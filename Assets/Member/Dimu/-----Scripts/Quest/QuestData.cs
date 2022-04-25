using NSY.Manager;
using UnityEngine;

namespace DM.Quest
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "QuestData/new QuestData", order = 0)]
    public class QuestData : ScriptableObject
    {
        public int questID; //id 양식을 정할 것00 00 01 이라던지... // 다이얼로그 때문에 012로 해야할듯.
        public int npcID; //퀘스트 제공자
        public int interactNpcID; //퀘스트 완료자
        public string questName;
        [TextArea]
        public string description;
        public Sprite[] TaskImg;
        public Task tasks;
        //public Requirements requirements;
        public Rewards[] rewards;


        [System.Serializable]
        public class Task
        {
            [SerializeField] public QuestTask_Build[] builds; //건물 짓기, 철거하기 등
            [SerializeField] public QuestTask[] items; //아이템 얻기 버리기 등
            [SerializeField] public QuestTask[] npcs; //npc 상호작용 등
            [SerializeField] public QuestTask[] locations; //지역 상호작용 등
            [SerializeField] public QuestTask[] gotItems; //인벤에 보유중인 친구들
            //재료 줍기
            //뭐 행동하기 (해당 행동을 했을 때 퀘스트로 들어오게 추가해야함.) 
        }
        [System.Serializable]
        public class QuestTask
        {
            public int objType;
            public int behaviorType;
            public int finishData;
            public int initData;
        }
        [System.Serializable]
        public class QuestTask_Build
        {
            //public int npcType; //특정 NPC의 집
            public Condition buildCondition; //특정 NPC의 집
            public BuildingBehaviorEnum behaviorType;//
        }
        public class QuestInfo
        {
            public int questId;
            public string charId;
        }
        [System.Serializable]
        public class Requirements
        {
            public QuestData[] requireQuests;
            public QuestInfo[] requireQuests2;
            public int requireLevel;
        }
        [System.Serializable]
        public class Rewards
        {
            public RewardType rewardType;
            public Item itemType;
            public int requireCount;
        }

        public void InitData() //퀘스트에 필요한 항목을 현재 플레이어 데이터 값으로 초기화
        {

            foreach (QuestTask_Build building in tasks.builds)
            {
                //PlayerData.AddDictionary(building.objType, PlayerData.BuildBuildingData, (int)BuildingBehaviorEnum.length);
                //building.initData = PlayerData.BuildBuildingData[building.objType].amounts[building.behaviorType];
            }

            foreach (QuestTask item in tasks.items)
            {
                PlayerData.AddDictionary(item.objType, PlayerData.ItemData, (int)ItemBehaviorEnum.length);
                item.initData = PlayerData.ItemData[item.objType].amounts[item.behaviorType];
            }
        }
        public bool CanClear()
        {
            if (tasks.builds.Length > 0)
            {
                foreach (QuestTask_Build building in tasks.builds)
                {
                    //building.buildCondition
                }
            }
            if (tasks.items.Length > 0)
            {
                foreach (QuestTask item in tasks.items)
                {
                    PlayerData.AddDictionary(item.objType, PlayerData.ItemData, (int)ItemBehaviorEnum.length);
                    Debug.Log(string.Format("fin: {0}, now: {1}", item.finishData, PlayerData.ItemData[item.objType].amounts[item.behaviorType] - item.initData));
                    int questdata = PlayerData.ItemData[item.objType].amounts[item.behaviorType] - item.initData;
                    if (item.finishData > questdata)
                    {
                        return false;
                    }
                }
            }
            if (tasks.npcs.Length > 0)
            {
                foreach (QuestTask npc in tasks.npcs)
                {
                    PlayerData.AddDictionary(npc.objType, PlayerData.npcData, (int)NpcBehaviorEnum.length);
                    Debug.Log(string.Format("fin: {0}, now: {1}", npc.finishData, PlayerData.npcData[npc.objType].amounts[npc.behaviorType] - npc.initData));

                    if (npc.finishData > PlayerData.npcData[npc.objType].amounts[0] - npc.initData)
                    {
                        return false;
                    }
                }
            }
            if (tasks.locations.Length > 0)
            {
                foreach (QuestTask location in tasks.locations)
                {
                    PlayerData.AddDictionary(location.objType, PlayerData.locationData, (int)LocationBehaviorEnum.length);
                    Debug.Log(string.Format("fin: {0}, now: {1}", location.finishData, PlayerData.locationData[location.objType].amounts[location.behaviorType] - location.initData));

                    if (location.finishData > PlayerData.locationData[location.objType].amounts[0] - location.initData)
                    {
                        return false;
                    }
                }
            }
            if (tasks.gotItems.Length > 0)
            {
                foreach (QuestTask gotItem in tasks.gotItems)
                {
                    //if (gotItem.finishData > PlayerData.gotItemData[gotItem.objType].amounts[0] - gotItem.initData)
                    //if(gotItem .finishData > SuperManager.Instance.inventoryManager.ItemCount(gotItem.objType.ToString()))
                    //{
                    //    return false;
                    //}
                }
            }
            Debug.Log("해당 사항 없음");
            return true;
        }

        //public bool CanAccept()
        //{
        //    //레벨이 충족되었는가?
        //    //if(requirements.requirementLevel > 현재 레벨)
        //    //{
        //    //   return false;
        //    //}
        //    //선행퀘스트가 있는가?
        //    foreach (QuestData requireQuest in requirements.requireQuests)
        //    {
        //        //선행퀘스트가 클리어 퀘스트 목록에 하나라도 없으면
        //        if (SuperManager.Instance.questmanager.IsQuestCleared(requireQuest))
        //        {
        //            Debug.Log("선행퀘스트를 클리어해야 합니다.");
        //            return false;
        //        }
        //    }
        //    Debug.Log("퀘스트를 받을 수 있습니다.");

        //    return true;
        //}

        public int QuestID => questID;
    }
}
