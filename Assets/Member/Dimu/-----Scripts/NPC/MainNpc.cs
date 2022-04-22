using NSY.Manager;
using System.Collections.Generic;
using TT.BuildSystem;
using UnityEngine;


namespace DM.NPC
{
    public class MainNpc : NPC, ITalkable
    {
        //[SerializeField] PreferItem[] preferBuildItemObjList;
        [SerializeField] BuildingBlock myHouse;
        public Condition[] wantToBuildCondition;
        BuildingManager buildingManager;

        private void Awake()
        {
            buildingManager = FindObjectOfType<BuildingManager>();
        }
        private void Start()
        {
            EventManager.EventActions[2] += MoveToMyHome;
            EventManager.EventActions[3] += MoveToHisHome;

            BuildingBlock.UpdateBuildingInfos += FindLikeHouse;

        }
        public void FindLikeHouse()
        {
            BuildingBlock buildingBlock = null;
            float highScore = 0;
            foreach (BuildingBlock item in buildingManager.GetCompleteBuildings())
            {
                if (item.HaveLivingChar()) continue;

                float nowScore = GetBuildingLikeable(item);
                print(nowScore);

                if (50 < nowScore)
                {
                    if (highScore < nowScore)
                    {
                        buildingBlock = item;
                        highScore = nowScore;
                    }
                }
            }
            SetMyHouse(buildingBlock);
        }
        public void SetMyHouse(BuildingBlock block)
        {
            if (!block) return;
            myHouse = block;
            myHouse.SetLivingChar(this);
            print("Find My House");
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
        public void Talk(Item handitem)
        {
            PlayDialog(handitem);
        }
        public void PlayDialog(Item handitem)
        {
            SuperManager.Instance.dialogueManager.FirstShowDialog(this, handitem);
        }
        public void MoveTo(Vector3 pos, Character character)
        {
            transform.position = pos;
        }
        public void MoveToMyHome()
        {
            if (myHouse)
            {
                Vector3 vec = new Vector3(myHouse.transform.position.x, myHouse.transform.position.y, myHouse.transform.position.z);
                vec += myHouse.transform.forward * -7;//집 앞
                MoveTo(vec, GetCharacterType());
            }
            EventManager.EventAction -= EventManager.EventActions[2];
        }
        public void MoveToHisHome()
        {
            if (GetCharacterType() != Character.Walrus) return;
            BuildingBlock buildingBlock = buildingManager.GetNPCsHouse(Character.Ejang);
            Vector3 vec = new Vector3(buildingBlock.transform.position.x, buildingBlock.transform.position.y, buildingBlock.transform.position.z);
            vec += buildingBlock.transform.forward * -9;//집 앞
            MoveTo(vec, GetCharacterType());
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
