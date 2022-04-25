using DM.Dialog;
using NSY.Manager;
using NSY.Player;
using System.Collections.Generic;
using TT.BuildSystem;
using UnityEngine;

public enum BuildingLike { Like, Unlike, Cant, None, }

namespace DM.NPC
{
    public class MainNpc : NPC, ITalkable
    {
        //[SerializeField] PreferItem[] preferBuildItemObjList;
        [SerializeField] BuildingBlock myHouse;
        public Condition[] wantToBuildCondition;
        BuildingManager buildingManager;
        DialogueManager dialogueManager;
        public PlayerInteract player;
        [SerializeField] float speed;
        [SerializeField] bool isFollowPlayer;
        BuildingLike like = BuildingLike.None;
        private void Awake()
        {
            buildingManager = FindObjectOfType<BuildingManager>();
            dialogueManager = FindObjectOfType<DialogueManager>();
            player = FindObjectOfType<PlayerInteract>();
        }
        private void Start()
        {
            EventManager.EventActions[2] += MoveToMyHome;
            EventManager.EventActions[3] += MoveToHisHome;
            EventManager.EventActions[4] += OnFollowPlayer;

            //BuildingBlock.UpdateBuildingInfos += FindLikeHouse;
        }
        private void Update()
        {
            if (isFollowPlayer)
                FollowPlayer();
        }
        public void OnFollowPlayer()
        {
            //현재 대화 상대와 같다면
            if (dialogueManager.GetNowNpc() == this)
            {
                isFollowPlayer = true;
                player.SetNpc(this);
                EventManager.EventAction -= EventManager.EventActions[4];
            }
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
        public void FindLikeHouse(BuildingBlock buildAreaObject)
        {
            if (buildAreaObject.HaveLivingChar())
            {
                like = BuildingLike.Cant;
                SettingBuildingTalk();
                return;
            }
            float highScore = GetBuildingLikeable(buildAreaObject);

            if (50 < highScore)
                SetMyHouse(buildAreaObject);
            else
                SetMyHouse(null);
        }
        public float GetMyHouseScore()
        {
            return GetBuildingLikeable(myHouse);
        }
        public void SetMyHouse(BuildingBlock block)
        {
            if (!block)
            {
                like = BuildingLike.Unlike;
            }
            else
            {
                like = BuildingLike.Like;
                myHouse = block;
                myHouse.SetLivingChar(this);
                print("Find My House");
                MoveToMyHome();
            }
            SettingBuildingTalk();
        }
        public float GetBuildingLikeable(BuildingBlock buildingBlock)
        {
            float score = 0;
            List<BuildingItemObj> buildItemList = buildingBlock.GetBuildItemList();

            foreach (BuildingItemObj buildItem in buildItemList)//건축자재 하나씩
            {
                foreach (Condition preferCondition in wantToBuildCondition)
                {
                    bool canContinue = false;

                    foreach (var kind in preferCondition.buildItemKind)
                    {
                        if (kind == buildItem.GetAttribute().buildItemKind)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                    }

                    if (!canContinue) break;

                    foreach (var kind in preferCondition.buildHPos)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildHPos)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                    }

                    if (!canContinue) break;

                    foreach (var kind in preferCondition.buildVPos)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildVPos)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                    }

                    if (!canContinue) break;

                    foreach (var kind in preferCondition.buildSize)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildSize)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                    }

                    if (!canContinue) break;

                    foreach (var kind in preferCondition.buildColor)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildColor)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                    }

                    if (!canContinue) break;

                    foreach (var kind in preferCondition.buildShape)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildShape)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                    }

                    if (!canContinue) break;

                    foreach (var kind in preferCondition.buildThema)
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
                    }

                    if (!canContinue) break;

                    //해당 건축자재가 설치된 개수 가져오기
                    foreach (var kind in preferCondition.buildCount)
                    {
                        canContinue = false;
                        //개수와 비교하기
                    }

                    if (!canContinue) break;

                    score += preferCondition.likeable;
                }
            }
            print(score);
            return score;
        }

        public string CanInteract()
        {
            return "말걸기";
        }
        public Transform ReturnTF()
        {
            return transform;
        }
        public void SettingBuildingTalk()
        {
            print("SettingBuildingTalk");
            PlayDialog(null);
            if (isFollowPlayer)
            {
                player.SetNpc(null);
                isFollowPlayer = false;
            }

        }
        public void Talk(Item handitem)
        {
            print("Talk");
            if (isFollowPlayer)
            {
                player.SetNpc(null);
                isFollowPlayer = false;
            }
            else
                PlayDialog(handitem);
        }
        public void PlayDialog(Item handitem)
        {
            SuperManager.Instance.dialogueManager.FirstShowDialog(this, handitem, isFollowPlayer, (int)like);
        }
        public void MoveTo(Vector3 pos)
        {
            transform.position = pos;
        }
        public void MoveToMyHome()
        {
            if (myHouse)
            {
                Vector3 vec = new Vector3(myHouse.transform.position.x-5, transform.transform.position.y, myHouse.transform.position.z + myHouse.transform.forward.z * -7);
                //vec = new Vector3(myHouse.transform.position.x, myHouse.transform.position.y, myHouse.transform.position.z);// myHouse.transform.forward * -7;//집 앞
                MoveTo(vec);
            }
            EventManager.EventAction -= EventManager.EventActions[2];
        }
        public void MoveToHisHome()
        {
            if (GetCharacterType() != Character.Walrus) return;
            BuildingBlock buildingBlock = buildingManager.GetNPCsHouse((int)Character.Ejang);
            Vector3 vec = new Vector3(buildingBlock.transform.position.x, buildingBlock.transform.position.y, buildingBlock.transform.position.z);
            vec += buildingBlock.transform.forward * -9;//집 앞
            MoveTo(vec);
            EventManager.EventAction -= EventManager.EventActions[3];
        }
    }
    [System.Serializable]
    public class PreferItem
    {
        public Item item;
        public float likeable;
    }


}
