﻿using UnityEngine;

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
        public Requirements requirements;


        [System.Serializable]
        public class Task
        {
            [SerializeField] public QuestTask[] builds; //건물 짓기, 철거하기 등
            [SerializeField] public QuestTask[] items; //아이템 얻기 버리기 등
            [SerializeField] public QuestTask[] npcs; //npc 상호작용 등
            [SerializeField] public QuestTask[] locations; //npc 상호작용 등
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

        public void InitData() //퀘스트에 필요한 항목을 현재 플레이어 데이터 값으로 초기화
        {
            foreach (QuestTask building in tasks.builds)
            {
                if (!PlayerData.BuildBuildingData.ContainsKey(building.objType))
                {
                    PlayerData.BuildBuildingData.Add(building.objType, new int());
                }
                building.initData = PlayerData.BuildBuildingData[building.objType];
            }

            foreach (QuestTask item in tasks.items)
            {
                if (!PlayerData.ItemData.ContainsKey(item.objType))
                {
                    PlayerData.ItemData.Add(item.objType, new ItemBehavior());
                }
                item.initData = PlayerData.ItemData[item.objType].amounts[item.behaviorType];
            }
        }
        public bool CanClear()
        {
            if (tasks.builds.Length > 0)
            {
                foreach (QuestTask building in tasks.builds)
                {
                    if (!PlayerData.BuildBuildingData.ContainsKey(building.objType)) PlayerData.BuildBuildingData.Add(building.objType, new int());
                    Debug.Log(string.Format("fin: {0}, now: {1}", building.finishData, PlayerData.BuildBuildingData[building.objType] - building.initData));

                    if (building.finishData > PlayerData.BuildBuildingData[building.objType] - building.initData)
                    {
                        return false;
                    }
                }
            }
            if (tasks.items.Length > 0)
            {
                foreach (QuestTask item in tasks.items)
                {
                    if (!PlayerData.ItemData.ContainsKey(item.objType)) PlayerData.ItemData.Add(item.objType, new ItemBehavior());
                    Debug.Log(string.Format("fin: {0}, now: {1}", item.finishData, PlayerData.ItemData[item.objType].amounts[item.behaviorType] - item.initData));

                    if (item.finishData > PlayerData.ItemData[item.objType].amounts[item.behaviorType] - item.initData)
                    {
                        return false;
                    }
                }
            }
            if (tasks.npcs.Length > 0)
            {
                foreach (QuestTask npc in tasks.npcs)
                {
                    if (!PlayerData.ItemData.ContainsKey(npc.objType)) PlayerData.ItemData.Add(npc.objType, new ItemBehavior());
                    Debug.Log(string.Format("fin: {0}, now: {1}", npc.finishData, PlayerData.npcData[npc.objType] - npc.initData));

                    if (npc.finishData > PlayerData.npcData[npc.objType] - npc.initData)
                    {
                        return false;
                    }
                }
            }
            if (tasks.locations.Length > 0)
            {
                foreach (QuestTask location in tasks.locations)
                {
                    if (!PlayerData.ItemData.ContainsKey(location.objType)) PlayerData.ItemData.Add(location.objType, new ItemBehavior());
                    Debug.Log(string.Format("fin: {0}, now: {1}", location.finishData, PlayerData.loactionData[location.objType] - location.initData));

                    if (location.finishData > PlayerData.loactionData[location.objType] - location.initData)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool CanAccept()
        {
            //레벨이 충족되었는가?
            //if(requirements.requirementLevel > 현재 레벨)
            //{
            //   return false;
            //}
            //선행퀘스트가 있는가?
            foreach (QuestData requireQuest in requirements.requireQuests)
            {
                //선행퀘스트가 클리어 퀘스트 목록에 하나라도 없으면
                if (!FindObjectOfType<QuestManager>().IsQuestCleared(requireQuest.questID, requireQuest.npcID))
                {
                    Debug.Log("선행퀘스트를 클리어해야 합니다.");
                    return false;
                }
            }
            Debug.Log("퀘스트를 받을 수 있습니다.");

            return true;
        }

        public int QuestID => questID;
    }
}
