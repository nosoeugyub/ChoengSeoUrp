using UnityEngine;

namespace DM.Quest
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "QuestData/new QuestData", order = 0)]
    public class QuestData : ScriptableObject
    {
        public int questID; //id 양식을 정할 것00 00 01 이라던지...
        public int npcID; //퀘스트 제공자
        public string questName;
        public string description;
        public Sprite[] TaskImg;
        public Task tasks;

        [System.Serializable]
        public class Task
        {
            [SerializeField] public QuestTask[] builds; //건물 짓기
            [SerializeField] public QuestTask[] items; //아이템 얻기 버리기 등
            [SerializeField] public QuestTask[] kills; //뭐 때려잡기
            //건물 짓기(목표점 + 현재점)
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
        public void InitData()
        {
            foreach (QuestTask item in tasks.builds)
            {
                item.initData = PlayerData.BuildBuildingData[item.objType];
            }
            foreach (QuestTask item in tasks.items)
            {
                if(!PlayerData.ItemData.ContainsKey(item.objType))
                {
                    PlayerData.ItemData.Add(item.objType, new ItemBehavior());
                }
                item.initData = PlayerData.ItemData[item.objType].amounts[item.behaviorType];
            }
            foreach (QuestTask item in tasks.kills)
            {
                item.initData = 0;
            }
        }
        public bool IsClear()
        {
            if (tasks.builds.Length > 0)
            {
                foreach (QuestTask item in tasks.builds)
                {
                    Debug.Log(string.Format("f: {0}, now: {1}", item.finishData
                             , PlayerData.BuildBuildingData[item.objType] - item.initData));

                    if (item.finishData > PlayerData.BuildBuildingData[item.objType] - item.initData)
                    {
                        return false;
                    }
                }
            }
            if (tasks.items.Length > 0)
            {
                foreach (QuestTask item in tasks.items)
                {
                    Debug.Log(string.Format("f: {0}, now: {1}", item.finishData
                             , PlayerData.ItemData[item.objType].amounts[item.behaviorType] - item.initData));

                    if (item.finishData > PlayerData.ItemData[item.objType].amounts[item.behaviorType] - item.initData)
                    {
                        return false;
                    }
                }
            }
            if (tasks.kills.Length > 0)
            {

            }
            return true;
        }
    }
}
