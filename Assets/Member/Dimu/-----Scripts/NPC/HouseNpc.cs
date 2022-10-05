using DM.Building;
using NSY.Manager;
using NSY.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingLike { Like, Unlike_Shape, Unlike_Count, Unlike_Empty, Cant, None, }
public enum DialogMarkType { CanStart, CanClear, None, }

namespace DM.NPC
{
    public class HouseNpc : NPC
    {
        [SerializeField] private BuildingBlock myHouse;
        [SerializeField] private Condition[] wantToBuildCondition;
        [SerializeField] private PlayerInteract player;
        [SerializeField] private Transform[] dialogMarks;
        [SerializeField] private DialogMarkType nowDialogMarkType = DialogMarkType.None;
        [SerializeField] private Vector3 dialogMarkScale;
        [SerializeField] private float speed;
        [SerializeField] private bool isFollowPlayer;
        [SerializeField] private string talkSound;

        private BuildingLike like = BuildingLike.None;
        float dist;

        public Action GoHomeEvent;
        public Action<int> UIOnEvent;
        public Action<int, DialogMarkType> UIUpdateEvent;

        public BuildingBlock MyHouse { get { return myHouse; } set { myHouse = value; } }

        private void Awake()
        {
            player = FindObjectOfType<PlayerInteract>();
            if (dialogMarks.Length > 0)
                dialogMarkScale = dialogMarks[0].localScale;
        }
        private void Start()
        {
            GoHomeEvent += MoveToMyHome;
            DIalogEventManager.EventActions[((int)EventEnum.MoveToMyHome)] += MoveToMyHome;
            //DIalogEventManager.EventActions[(int)EventEnum.OnFollowPlayer] += OnFollowPlayer;
        }

        internal void PlayDialogSound()
        {
            if (talkSound != "")
            {
                SuperManager.Instance.soundManager.StopSFX(talkSound);
                SuperManager.Instance.soundManager.PlaySFX(talkSound);
            }
        }

        private void FixedUpdate()
        {
            if (isFollowPlayer)
                FollowPlayer();
            if (nowDialogMarkType != DialogMarkType.None)
            {
                dist = Vector3.Distance(player.transform.position, dialogMarks[(int)nowDialogMarkType].position) * 0.02f;
                if (dist < 0.5f)
                    dist = 0.5f;
                dialogMarks[(int)nowDialogMarkType].localScale = dialogMarkScale * dist;
            }
        }
        public void SetQuestMark(DialogMarkType dialogMarkType)//, bool ison)
        {
            if (dialogMarkType == nowDialogMarkType) return;

            if (nowDialogMarkType != DialogMarkType.None)
                dialogMarks[(int)nowDialogMarkType].gameObject.SetActive(false);

            nowDialogMarkType = dialogMarkType;

            if (dialogMarkType != DialogMarkType.None)
                dialogMarks[(int)nowDialogMarkType].gameObject.SetActive(true);

            NPCStateUIUptate(dialogMarkType);
        }

        public void NPCStateUIUptate(DialogMarkType dialogMarkType)
        {
            UIUpdateEvent((int)GetCharacterType(), dialogMarkType);
        }

        public void SetIsFollowPlayer(bool ison)
        {
            isFollowPlayer = ison;
        }
        public bool IsHaveHouse()
        {
            return myHouse;
        }
        public void FollowPlayer()
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 4)
            {
                Vector3 dir = player.transform.position - transform.position;
                Vector3 vector3 = new Vector3(transform.position.x + dir.x * Time.deltaTime * speed, transform.position.y, transform.position.z + dir.z * Time.deltaTime * speed);
                MoveTo(vector3);
            }
        }
        public void FindLikeHouse(BuildingBlock buildAreaObject) //해당 건축물에 입주 가능한지.
        {
            if (buildAreaObject.SpecialHouse)
            {
                DebugText.Instance.SetText("특수 건물은 소개할 수 없어요!");
                return;
            }
            if (buildAreaObject._livingCharacter)
            {
                like = BuildingLike.Cant;
                SettingBuildingTalk();
                return;
            }
            BuildingLike canGet = GetBuildingLikeable(buildAreaObject); //점수 

            switch (canGet)
            {
                case BuildingLike.Like:
                    SetMyHouse(buildAreaObject, BuildingLike.Like);
                    break;
                default:
                    SetMyHouse(null, canGet);
                    break;
            }
        }
        public BuildingLike CanGetMyHouse()
        {
            return GetBuildingLikeable(myHouse);
        }
        public void SetMyHouse(BuildingBlock block, BuildingLike l)
        {
            like = l;
            if (!SettingBuildingTalk()) return;
            if (block)
            {
                myHouse = block;
                myHouse._livingCharacter = this;
                print("Find My House");
                PlayerData.AddValue((int)GetCharacterType(), (int)NpcBehaviorEnum.gethouse, PlayerData.npcData, (int)NpcBehaviorEnum.length);
                SuperManager.Instance.questmanager.ClearQuest(SuperManager.Instance.questmanager.questLists[(int)GetCharacterType()].questList.Length - 1, (int)GetCharacterType());
                GoHomeEvent();
                UIOnEvent((int)GetCharacterType());
            }
        }

        internal bool IsFollowPlayer()
        {
            return isFollowPlayer;
        }

        public BuildingLike GetBuildingLikeable(BuildingBlock buildingBlock) //bool형
        {
            if (!buildingBlock) return BuildingLike.None;
            if (buildingBlock.GetBuildItemList().Count == 0) return BuildingLike.Unlike_Empty;

            List<BuildingItemObj> buildItemList = buildingBlock.GetBuildItemList();

            int[] ints = new int[wantToBuildCondition.Length];

            int failBuildItemCount = 0;

            foreach (BuildingItemObj buildItem in buildItemList)//건축자재 하나씩
            {
                int count = 0;
                for (int i = 0; i < wantToBuildCondition.Length; i++)
                {
                    bool canContinue = true;
                    ints[i]++;

                    Test(wantToBuildCondition[i].buildItemKind, buildItem.GetAttribute().buildItemKind, ref canContinue, ref count, ref failBuildItemCount);
                    if (!canContinue) break;

                    Test(wantToBuildCondition[i].buildHPos, buildItem.GetAttribute().buildHPos, ref canContinue, ref count, ref failBuildItemCount);
                    if (!canContinue) break;

                    Test(wantToBuildCondition[i].buildVPos, buildItem.GetAttribute().buildVPos, ref canContinue, ref count, ref failBuildItemCount);
                    if (!canContinue) break;

                    Test(wantToBuildCondition[i].buildSize, buildItem.GetAttribute().buildSize, ref canContinue, ref count, ref failBuildItemCount);
                    if (!canContinue) break;

                    Test(wantToBuildCondition[i].buildColor, buildItem.GetAttribute().buildColor, ref canContinue, ref count, ref failBuildItemCount);
                    if (!canContinue) break;

                    Test(wantToBuildCondition[i].buildShape, buildItem.GetAttribute().buildShape, ref canContinue, ref count, ref failBuildItemCount);
                    if (!canContinue) break;

                    Test(wantToBuildCondition[i].buildThema, buildItem.GetAttribute().buildThema, ref canContinue, ref count, ref failBuildItemCount);
                    if (!canContinue) break;
                }
            }

            for (int i = 0; i < wantToBuildCondition.Length; i++)
            {
                if (wantToBuildCondition[i].buildCount > ints[i])
                {
                    return BuildingLike.Unlike_Count; // false
                }
            }

            for (int i = 0; i < wantToBuildCondition.Length; i++)
            {
                if (wantToBuildCondition[i].buildPerfactCount > ints[i])
                {
                    return BuildingLike.Unlike_Count; // false
                }
            }

            print(failBuildItemCount);
            int halfbuildcount = buildItemList.Count / 2;
            if (halfbuildcount >= failBuildItemCount)
                return BuildingLike.Like;
            else
                return BuildingLike.Unlike_Shape;
        }
        public void Test<T>(T[] d, dynamic attr, ref bool canContinue, ref int count, ref int failBuildItemCount)
        {
            count = 0;
            foreach (var kind in d)
            {
                canContinue = false;
                if (kind == attr)
                {
                    print(kind.ToString());
                    canContinue = true;
                    break;
                }
                count++;
            }
            if (d.Length != 0 && count == d.Length)
                ++failBuildItemCount;
        }
        public override int CanInteract()
        {
            return (int)CursorType.Talk;
        }
        public bool SettingBuildingTalk()
        {
            if (!PlayDialog()) return false;
            SetIsFollowPlayer(false);
            return true;
        }
        public override void Talk()
        {
            PlayDialog();
        }
        public bool PlayDialog()
        {
            if (IsFollowPlayer())
            {
                Debug.Log("ResetDelay()");
                SuperManager.Instance.dialogueManager.ResetDelay();
            }
            return SuperManager.Instance.dialogueManager.FirstShowDialog(this, isFollowPlayer, (int)like);
        }
        public void MoveTo(Vector3 pos)
        {
            transform.position = pos;
        }
        public void TeleportToPlayer(Vector3 pos)
        {
            if (isFollowPlayer)
                MoveTo(pos);
        }
        public void MoveToMyHome()
        {
            if (myHouse)
            {
                Vector3 vec = myHouse.HouseOwnerTransform.position;
                MoveTo(vec);
            }
            DIalogEventManager.EventAction -= DIalogEventManager.EventActions[2];
        }
    }
}
