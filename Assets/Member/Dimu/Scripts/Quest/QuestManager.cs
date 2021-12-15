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
        private void Awake()
        {
            acceptQuests = new Dictionary<QuestData, GameObject>();
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
}