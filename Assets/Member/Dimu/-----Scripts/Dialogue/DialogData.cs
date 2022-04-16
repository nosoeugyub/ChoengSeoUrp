using DM.Quest;
using NSY.Manager;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "DialogData/new DialogData", order = 0)]
public class DialogData : ScriptableObject
{
    public Sentence[] acceptSentenceInfo;//시작 문장뭉치
    public Sentence[] proceedingSentenceInfo;//진행중 문장뭉치
    public Sentence[] clearSentenceInfo;//완료 문장뭉치

    public DialogTask dialogTasks;

    public int questId; //문장이 끝나면 주어질 퀘스트
    public int subjectCharacterID; //대화 주체(상대)
    public bool isTalkingOver; //대화가 끝났는지

    public bool CanStartTalk()
    {
        foreach (var item in dialogTasks.haveToClearQuest)
        {
            if (!SuperManager.Instance.questmanager.IsQuestCleared(item.questdata))
                return false;
        }
        foreach (var item in dialogTasks.DonthaveToClearQuest)
        {
            if (SuperManager.Instance.questmanager.IsQuestCleared(item.questdata))
                return false;
        }
        foreach (var item in dialogTasks.haveToDoingQuest)
        {
            if (!SuperManager.Instance.questmanager.IsQuestAccepted(item.questdata))
                return false;

        }
        foreach (var item in dialogTasks.buildBuildings)
        {
            //빌딩매니저에서 해당 건축물이 어느 집에 설치되어있을 때..
        }
        //if( dialogTasks.itemIndexToHand == 
        //{
        //    //플레이어의 hand 체크
        //}
        return true;
    }

}
public enum TextboxType { Normal, Cloud, Sharp }

[System.Serializable]
public class Sentence
{
    public string sentence;
    public int characterId;
    public int eventIdx;
    public TextboxType textboxType;

    public Sentence(string sentence_, int characterId_)
    { sentence = sentence_; characterId = characterId_; }
}

[System.Serializable]
public class DialogTask
{
    public QuestIndexSet[] haveToClearQuest; //클리어 해야 하는 퀘스트
    public QuestIndexSet[] DonthaveToClearQuest; //클리어 하지 말아야 하는 퀘스트
    public QuestIndexSet[] haveToDoingQuest; //진행중이어야 하는 퀘스트
    public BuildBuilding[] buildBuildings;
    public int itemIndexToHand;//손에 들고 있어야 하는 아이템 인덱스
}
[System.Serializable]
public class QuestIndexSet
{
    public QuestData questdata;
    public int npcid;
    public int questid;
}
[System.Serializable]
public class BuildBuilding
{
    public int[] itemIndexToBuild;//지어져야 하는 건축물 인덱스
    public int npcHouse;//지어져야 하는 건축물의 위치 인덱스
}
