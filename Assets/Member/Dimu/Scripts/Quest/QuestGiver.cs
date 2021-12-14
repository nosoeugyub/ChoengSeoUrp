using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DM.Quest
{
    public class QuestGiver : MonoBehaviour
    {
        public QuestData[] gotQuests;
        public int questProgress = 0;

        public void GiveQuest()
        {
           FindObjectOfType<QuestManager>().AcceptQuest(gotQuests[questProgress], 0);
        }
        public void ClearQuest()
        {
            FindObjectOfType<QuestManager>().ClearQuest(gotQuests[questProgress]);
            ++questProgress;
        }
    }
}
