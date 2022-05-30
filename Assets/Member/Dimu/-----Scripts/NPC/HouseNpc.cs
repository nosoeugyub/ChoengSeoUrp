using DM.Building;
using DM.Dialog;
using NSY.Manager;
using NSY.Player;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingLike { Like, Unlike_Shape, Unlike_Count, Unlike_Empty, Cant, None, }

namespace DM.NPC
{
    public class HouseNpc : NPC
    {
        [SerializeField] private BuildingBlock myHouse;
        [SerializeField] private Condition[] wantToBuildCondition;
        [SerializeField] private PlayerInteract player;
        [SerializeField] private Transform questMark;
        [SerializeField] private Vector3 questMarkScale;
        [SerializeField] private float speed;
        [SerializeField] private bool isFollowPlayer;
        private DialogueManager dialogueManager;
        private BuildingLike like = BuildingLike.None;

        public BuildingBlock MyHouse { get { return myHouse; } set { myHouse = value; } }

        private void Awake()
        {
            questMarkScale = questMark.localScale;
            dialogueManager = FindObjectOfType<DialogueManager>();
            player = FindObjectOfType<PlayerInteract>();
        }
        private void Start()
        {
            EventManager.EventActions[2] += MoveToMyHome;
            EventManager.EventActions[4] += OnFollowPlayer;
        }
        private void FixedUpdate()
        {
            if (isFollowPlayer)
                FollowPlayer();
            float dist = Vector3.Distance(player.transform.position, questMark.position) * 0.03f;
            if(dist < 0.5f)
                dist = 0.5f;
            questMark.localScale = questMarkScale * dist; 
        }
        public void SetQuestMark(bool ison)
        {
            questMark.gameObject.SetActive(ison);
        }
        public void OnFollowPlayer()
        {
            //현재 대화 상대와 같다면
            if (dialogueManager.GetNowNpc() == this)
            {
                if (player.SetNpc(this))
                {
                    isFollowPlayer = true;
                }
                    EventManager.EventAction -= EventManager.EventActions[4];
            }
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
            if (buildAreaObject.HaveLivingChar())
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
                myHouse.SetLivingChar(this);
                print("Find My House");
                MoveToMyHome();
            }
        }
        public BuildingLike GetBuildingLikeable(BuildingBlock buildingBlock) //bool형
        {
            if (!buildingBlock) return BuildingLike.None;
            if (buildingBlock.GetBuildItemList().Count == 0) return BuildingLike.Unlike_Empty;

            List<BuildingItemObj> buildItemList = buildingBlock.GetBuildItemList();

            int[] ints = new int[wantToBuildCondition.Length];

            foreach (BuildingItemObj buildItem in buildItemList)//건축자재 하나씩
            {
                int count = 0;
                for (int i = 0; i < wantToBuildCondition.Length; i++)
                {
                    bool canContinue = false;

                    foreach (var preferkind in wantToBuildCondition[i].buildItemKind)
                    {
                        //희망 조건에 있는 종류와 설치된 자재와 같은지
                        if (preferkind == buildItem.GetAttribute().buildItemKind)
                        {
                            print(preferkind.ToString());
                            canContinue = true;
                            break;
                        }
                        count++;
                    }
                    if (wantToBuildCondition[i].buildItemKind.Length != 0 && count == wantToBuildCondition[i].buildItemKind.Length) return BuildingLike.Unlike_Shape;

                    if (!canContinue) break;

                    count = 0;
                    foreach (var kind in wantToBuildCondition[i].buildHPos)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildHPos)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                        count++;
                    }
                    if (wantToBuildCondition[i].buildHPos.Length != 0 && count == wantToBuildCondition[i].buildHPos.Length) return BuildingLike.Unlike_Shape;

                    if (!canContinue) break;

                    count = 0;
                    foreach (var kind in wantToBuildCondition[i].buildVPos)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildVPos)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                        count++;
                    }
                    if (wantToBuildCondition[i].buildVPos.Length != 0 && count == wantToBuildCondition[i].buildVPos.Length) return BuildingLike.Unlike_Shape;

                    if (!canContinue) break;

                    count = 0;
                    foreach (var kind in wantToBuildCondition[i].buildSize)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildSize)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                        count++;
                    }
                    if (wantToBuildCondition[i].buildSize.Length != 0 && count == wantToBuildCondition[i].buildSize.Length) return BuildingLike.Unlike_Shape;

                    if (!canContinue) break;

                    count = 0;
                    foreach (var kind in wantToBuildCondition[i].buildColor)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildColor)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                        count++;
                    }
                    if (wantToBuildCondition[i].buildColor.Length != 0 && count == wantToBuildCondition[i].buildColor.Length) return BuildingLike.Unlike_Shape;

                    if (!canContinue) break;

                    count = 0;
                    foreach (var kind in wantToBuildCondition[i].buildShape)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildShape)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                        count++;
                    }
                    if (wantToBuildCondition[i].buildShape.Length != 0 && count == wantToBuildCondition[i].buildShape.Length) return BuildingLike.Unlike_Shape;

                    if (!canContinue) break;

                    count = 0;
                    foreach (var kind in wantToBuildCondition[i].buildThema)
                    {
                        canContinue = false;
                        foreach (var thema in buildItem.GetAttribute().buildThema)
                        {
                            print(kind.ToString());
                            if (kind == thema)
                            {
                                canContinue = true;
                                break;
                            }
                        }
                        if (canContinue) break;
                        count++;
                    }
                    if (wantToBuildCondition[i].buildThema.Length != 0 && count == wantToBuildCondition[i].buildThema.Length) return BuildingLike.Unlike_Shape;

                    if (!canContinue) break;

                    ints[i]++;
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
            return BuildingLike.Like;
        }

        public override int CanInteract()
        {
            return (int)CursorType.Talk;
        }
        public Transform ReturnTF()
        {
            return transform;
        }
        public bool SettingBuildingTalk()
        {
            if (!PlayDialog(null)) return false;
            if (isFollowPlayer)
            {
                player.SetNpc(null);
                isFollowPlayer = false;
            }
            return true;
        }
        public override void Talk(Item handitem)
        {
            if (isFollowPlayer)
            {
                player.SetNpc(null);
                isFollowPlayer = false;
            }
            else
                PlayDialog(handitem);
        }
        public bool PlayDialog(Item handitem)
        {
            return SuperManager.Instance.dialogueManager.FirstShowDialog(this, handitem, isFollowPlayer, (int)like);
        }
        public void MoveTo(Vector3 pos)
        {
            transform.position = pos;
        }
        public void MoveToMyHome()
        {
            if (myHouse)
            {
                Vector3 vec = myHouse.HouseOwnerTransform.position;
                MoveTo(vec);
            }
            EventManager.EventAction -= EventManager.EventActions[2];
        }
    }
}
