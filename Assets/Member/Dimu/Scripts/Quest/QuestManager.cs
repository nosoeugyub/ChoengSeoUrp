using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DM.Quest
{
    public class QuestManager : MonoBehaviour
    {
        private Dictionary<QuestData, GameObject> acceptQuests;
        //private Dictionary<int , QuestData> acceptQuests;
        //private List<QuestData> acceptQuests;
        public Transform questInfoMom;
        public GameObject questInfoUI;
        public QuestData testSOdata;
        private void Awake()
        {
            //acceptQuests = new List<QuestData>();
            acceptQuests = new Dictionary<QuestData, GameObject>();
        }
        public void AcceptQuest(int npcID)
        {
            if (acceptQuests.ContainsKey(testSOdata)) return;

            testSOdata.npcID = npcID;
            GameObject qui = Instantiate(questInfoUI, questInfoMom) as GameObject;
            UpdateQuestInfoUI(qui, testSOdata);

            acceptQuests.Add(testSOdata, qui);
        }
        public void AcceptQuest(QuestData questData, int npcID)
        {
            if (acceptQuests.ContainsKey(questData)) return;

            questData.npcID = npcID;
            GameObject qui = Instantiate(questInfoUI, questInfoMom) as GameObject;
            UpdateQuestInfoUI(qui, questData);

            questData.InitData();
            acceptQuests.Add(questData, qui);
        }
        public bool ClearQuest(QuestData questData)
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
        public void ClearQuest()
        {
            IsClear(testSOdata);
        }
        public bool IsClear(QuestData questData)
        {
            //퀘스트의 현재 수치가 목표치와 같거나 크다면 클리어
            //초기화 수치일 수도 있음
            if (questData.IsClear())
            {
                acceptQuests[questData].SetActive(false);
                acceptQuests.Remove(questData);
            }

            return false;
        }
        public void UpdateQuestInfoUI(GameObject qui, QuestData questData)
        {
            qui.transform.Find("QuestNameText").GetComponent<Text>().text
                = string.Format("{0}", questData.questName);
            qui.transform.Find("ProgressText").GetComponent<Text>().text
                = string.Format(testSOdata.description);

            qui.transform.Find("BuildingImg").GetComponent<Image>().sprite
                = questData.TaskImg[0];
        }
    }
}