using NSY.Manager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DM.Quest
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] bool istestmode;

        private List<QuestData> totalPlayQuests;//플레이하면서 만났던 퀘스트 모음 아직 안쓰임
        public Dictionary<QuestData, QuestInfoUI> acceptQuests;
        public Transform questInfoMom;
        public GameObject questInfoUI;
        public QuestData testSoData;
        public QuestList[] nowQuestLists;
        public QuestList[] questLists;
        public QuestList[] questLists_eng;
        public QuestList[] questLists_viet;
        public List<QuestData> clearQuestLists;
        public Sprite[] TaskImg;
        public string questClearSoundName;
        //[SerializeField] InventoryNSY inventoryNSY;
        [SerializeField] LanguageType nowLanguageType;

        private void Awake()
        {
            //inventoryNSY = FindObjectOfType<InventoryNSY>();
            acceptQuests = new Dictionary<QuestData, QuestInfoUI>();
            clearQuestLists = new List<QuestData>();
        }

        public void Start()
        {
            SetQuestSet(nowLanguageType);
        }//start update 등의 구문이 없다면 에디터에서 public QuestManager questmanager; 같은 구문에 넣을 수 없다.
        public void SetQuestSet(LanguageType languageType)
        {
            switch (languageType)
            {
                case LanguageType.Korean:
                    nowQuestLists = questLists;
                    break;
                case LanguageType.English:
                    nowQuestLists = questLists_eng;
                    break;
                case LanguageType.Vietnamese:
                    nowQuestLists = questLists_viet;
                    break;
                default:
                    break;
            }
        }
        public void AcceptQuest(int questId, int npcID)//퀘스트 수락하기
        {
            QuestData nowQuestData = nowQuestLists[npcID].questList[questId];
            //nowQuestData.CanAccept();
            if (acceptQuests.ContainsKey(nowQuestData)) return;

            nowQuestData.npcID = npcID;
            GameObject qui = Instantiate(questInfoUI, questInfoMom) as GameObject;
            qui.GetComponent<QuestInfoUI>().UpdateQuestInfoUI(nowQuestData, TaskImg[nowQuestData.interactNpcID]);

            nowQuestData.InitData();
            acceptQuests.Add(nowQuestData,qui.GetComponent<QuestInfoUI>());
        }
        public bool ClearQuest(int questId, int npcID) //퀘스트 클리어하기
        {
            if (istestmode) return true;
            if (CanClear(questId, npcID))//소모아이템이 존재한다면 추가
            {
                QuestData nowQuestData = nowQuestLists[npcID].questList[questId];

 

                //reward
                foreach (var reward in nowQuestData.rewards)
                {
                    if (reward.rewardType == RewardType.Item)
                    {
                        //아이템 추가
                        print(reward.itemType.ItemName);
                        if (SuperManager.Instance.inventoryManager.CanAddInven(reward.itemType))
                            SuperManager.Instance.inventoryManager.AddItem(reward.itemType,true);//, reward.requireCount);
                        else
                            return false;
                    }
                    else if (reward.rewardType == RewardType.Event)
                    {
                        DIalogEventManager.EventAction += DIalogEventManager.EventActions[reward.getCount];
                    }
                }
                foreach (QuestData.Rewards item in nowQuestData.returnRewards)
                {
                    SuperManager.Instance.inventoryManager.RemoveItem(item.itemType, item.getCount);//, reward.requireCount);
                }

                clearQuestLists.Add(nowQuestData);
                acceptQuests[nowQuestData].SetDisable();
                SuperManager.Instance.soundManager.StopSFX(questClearSoundName);
                SuperManager.Instance.soundManager.PlaySFX(questClearSoundName);
                acceptQuests.Remove(nowQuestData);


                return true;
            }
            return false;
        }
 

        public bool CanClear(int questId, int npcID)//퀘스트 클리어 가능한지?
        {
            return nowQuestLists[npcID].questList[questId].CanClear();
        }
        public bool IsQuestAccepted(QuestData questData)//특정 퀘스트 진행중인지?
        {
            if (acceptQuests.ContainsKey(questData)) return true;
            else return false;
        }
        //public bool CanAcceptQuest(int questId, int npcID)//퀘스트 수락 가능한지?
        //{
        //    return questLists[npcID].questList[questId].CanAccept();
        //}
        public bool IsQuestCleared(QuestData questData)//클리어한 퀘스트인지?
        {
            return clearQuestLists.Contains(questData);
        }
        //다른 Npc 와의 상호작용을 요구하는 퀘스트를 진행중인지
        public QuestData ReturnCanClearQuestRequireNpc(int npcID)//클리어 가능한 친구가 있다면 우선적으로 리턴해야 한다.
        {
            QuestData qd = null;
            foreach (var item in acceptQuests)
            {
                if (item.Key.interactNpcID == npcID) //현재 진행중인 퀘스트들 중에 완료자가 나랑 같은?
                {
                    if (CanClear(item.Key.questID, item.Key.npcID))
                    {
                        return item.Key;
                    }

                }
            }
            return qd;
        }
        public List<QuestData> GetIsAcceptedQuestList(int npcID)
        {
            List<QuestData> canAcceptQuests = new List<QuestData>();

            for (int i = 0; i < nowQuestLists[npcID].questList.Length; i++)
            {
                if (IsQuestAccepted(nowQuestLists[npcID].questList[i]))
                {
                    canAcceptQuests.Add(nowQuestLists[npcID].questList[i]);
                }
            }

            return canAcceptQuests;
        }

        public Dictionary<QuestData, QuestInfoUI> GetAcceptQuests()
        {
            return acceptQuests;
        }
        //public List<QuestData> GetCanAcceptQuestList(int npcID)
        //{
        //    List<QuestData> canAcceptQuests = new List<QuestData>();

        //    for (int i = 0; i < questLists[npcID].questList.Length; i++)
        //    {
        //        if (CanAcceptQuest(i, npcID))
        //        {
        //            canAcceptQuests.Add(questLists[npcID].questList[i]);
        //        }
        //    }

        //    return canAcceptQuests;
        //}
    }
    [System.Serializable]
    public class QuestList
    {
        public string charName;
        public QuestData[] questList;
    }
}
public enum RewardType
{ Item, Event, }