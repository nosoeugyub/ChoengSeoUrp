using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DM.Quest
{
    public class QuestManager : MonoBehaviour
    {
        private List<QuestData> totalPlayQuests;//플레이하면서 만났던 퀘스트 모음 아직 안쓰임
        private Dictionary<QuestData, GameObject> acceptQuests;
        public Transform questInfoMom;
        public GameObject questInfoUI;
        public QuestData testSOdata;
        public QuestList[] questLists;
        public List<QuestData> clearQuestLists;
        private void Awake()
        {
            acceptQuests = new Dictionary<QuestData, GameObject>();
            clearQuestLists = new List<QuestData>();
        }
        
        public void Start() { print(""); }//start update 등의 구문이 없다면 에디터에서 public QuestManager questmanager; 같은 구문에 넣을 수 없다.
        
        public void AcceptQuest(int questId, int npcID)//(QuestData questData, int npcID)
        {
            QuestData nowQuestData = questLists[npcID].questList[questId];
            nowQuestData.CanAccept();
            if (acceptQuests.ContainsKey(nowQuestData)) return;
        
            nowQuestData.npcID = npcID;
            GameObject qui = Instantiate(questInfoUI, questInfoMom) as GameObject;
            UpdateQuestInfoUI(qui, nowQuestData);
        
            nowQuestData.InitData();
            acceptQuests.Add(nowQuestData, qui);
        }public bool ClearQuest(QuestData questData)
        {
            if (questData.IsClear())
            {
                acceptQuests[questData].SetActive(false);
                acceptQuests.Remove(questData);
                Debug.Log("Clear");
                return true;
            }
                return false;
        }
        public void UpdateQuestInfoUI(GameObject qui, QuestData questData)
        {
            qui.transform.Find("QuestNameText").GetComponent<Text>().text
                = string.Format("{0}", questData.questName);
            qui.transform.Find("DescriptionText").GetComponent<Text>().text
                = string.Format(questData.description);
            qui.transform.Find("ProgressText").GetComponent<Text>().text
                = string.Format(questData.description);
            qui.transform.Find("BuildingImg").GetComponent<Image>().sprite
                = questData.TaskImg[0];
        }

        public bool IsQuestAccepted(QuestData questData)
        {
            if (acceptQuests.ContainsKey(questData)) return true;
            else return false;
        }
    }
    [System.Serializable]
    public class QuestList
    {
        public string charName;
        public QuestData[] questList;
    }
}