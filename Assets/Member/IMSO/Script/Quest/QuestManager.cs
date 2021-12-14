using Game.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int QuestId;
    public int questActionIndex;
    Dictionary<int, QuestData> QuestList;

    void Awake()
    {
        QuestList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData() 
    {
        QuestList.Add(10, new QuestData("마을 사람들과 대화하기",new int[] {1000,2000 }));
        QuestList.Add(20, new QuestData("청서의 잃어버린 도토리 찾아주기", new int[] { 1000, 2000 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return QuestId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if(id == QuestList[QuestId].NPCid[questActionIndex])
        questActionIndex++;

        if(questActionIndex == QuestList[QuestId].NPCid.Length)
            NextQuest();

        return QuestList[QuestId].questName;
    }

    void NextQuest()
    {
        QuestId += 10;
        questActionIndex = 0;
    }
}
