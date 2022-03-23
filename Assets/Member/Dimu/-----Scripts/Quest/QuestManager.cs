﻿using DM.Inven;
using NSY.Iven;
using NSY.Manager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DM.Quest
{
    public class QuestManager : MonoBehaviour
    {
        private List<QuestData> totalPlayQuests;//플레이하면서 만났던 퀘스트 모음 아직 안쓰임
        public Dictionary<QuestData, GameObject> acceptQuests;
        public Transform questInfoMom;
        public GameObject questInfoUI;
        public QuestData testSoData;
        public QuestList[] questLists;
        public List<QuestData> clearQuestLists;

        //[SerializeField] InventoryNSY inventoryNSY;

        private void Awake()
        {
            //inventoryNSY = FindObjectOfType<InventoryNSY>();
            acceptQuests = new Dictionary<QuestData, GameObject>();
            clearQuestLists = new List<QuestData>();
        }

        public void Start() { print(""); }//start update 등의 구문이 없다면 에디터에서 public QuestManager questmanager; 같은 구문에 넣을 수 없다.

        public void AcceptQuest(int questId, int npcID)//퀘스트 수락하기
        {
            QuestData nowQuestData = questLists[npcID].questList[questId];
            //nowQuestData.CanAccept();
            if (acceptQuests.ContainsKey(nowQuestData)) return;

            nowQuestData.npcID = npcID;
            GameObject qui = Instantiate(questInfoUI, questInfoMom) as GameObject;
            //UpdateQuestInfoUI(qui, nowQuestData);

            nowQuestData.InitData();
            acceptQuests.Add(nowQuestData, qui);

        }
        public bool ClearQuest(int questId, int npcID) //퀘스트 클리어하기
        {
            if (CanClear(questId, npcID))
            {
                
                QuestData nowQuestData = questLists[npcID].questList[questId];

                foreach (var reward in nowQuestData.rewards)
                {
                    if (reward.rewardType == RewardType.Gold)
                    {
                        //재화 증가
                        Debug.Log(string.Format( "Clear, {0}G 획득",reward.requireCount));
                    }
                    else if(reward.rewardType == RewardType.Item)
                    {
                        //아이템 추가
                        print(reward.itemType.ItemName);
                        SuperManager.Instance.inventoryManager.AddItem(reward.itemType);//, reward.requireCount);
                    }
                    else if (reward.rewardType == RewardType.Event)
                    {

                    }
                }
                clearQuestLists.Add(nowQuestData);
                acceptQuests[nowQuestData].SetActive(false);
                acceptQuests.Remove(nowQuestData);


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
            //qui.transform.Find("BuildingImg").GetComponent<Image>().sprite
            //    = questData.TaskImg[0];
        }

        public bool CanClear(int questId, int npcID)//퀘스트 클리어 가능한지?
        {
            QuestData nowQuestData = questLists[npcID].questList[questId];

            return nowQuestData.CanClear();
        }
        public bool IsQuestAccepted(int questId, int npcID)//특정 퀘스트 진행중인지?
        {
            if (acceptQuests.ContainsKey(questLists[npcID].questList[questId])) return true;
            else return false;
        }
        public bool CanAcceptQuest(int questId, int npcID)//퀘스트 수락 가능한지?
        {
            return questLists[npcID].questList[questId].CanAccept();
        }
        public bool IsQuestCleared(int questId, int npcID)//클리어한 퀘스트인지?
        {
            return clearQuestLists.Contains(questLists[npcID].questList[questId]);
        }
        //다른 Npc 와의 상호작용을 요구하는 퀘스트를 진행중인지
        public QuestData ReturnQuestRequireNpc(int npcID)
        {
            foreach (var item in acceptQuests)
            {
                if (item.Key.interactNpcID == npcID) //현재 진행중인 퀘스트들 중에 완료자가 나랑 같은?
                    return item.Key;
            }
            return null;
        }
    }
    [System.Serializable]
    public class QuestList
    {
        public string charName;
        public QuestData[] questList;
    }
}
public enum RewardType
{ Gold, Item, Event, }