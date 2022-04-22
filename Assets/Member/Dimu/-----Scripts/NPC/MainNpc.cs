using NSY.Manager;
using System.Collections.Generic;
using TT.BuildSystem;
using UnityEngine;


namespace DM.NPC
{
    public class MainNpc : NPC, ITalkable
    {
        [SerializeField] PreferItem[] preferBuildItemObjList;
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
        private void Update()
        {
            if (!myHouse)
            {
                FindLikeHouse();
            }
        }
        public void FindLikeHouse()
        {
            BuildingBlock buildingBlock = null;
            float highScore = 0;
            foreach (var item in buildingManager.GetCompleteBuildings())
            {
                if (item.HaveLivingChar()) continue;
                float nowScore = GetBuildingLikeable(item);
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
            List<Item> buildItemList = buildingBlock.GetBuildItemList();

            foreach (var item in buildItemList)
            {
                foreach (var preferObj in preferBuildItemObjList)
                {
                    if (preferObj.item == item)
                    {
                        score += preferObj.likeable;
                    }
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
