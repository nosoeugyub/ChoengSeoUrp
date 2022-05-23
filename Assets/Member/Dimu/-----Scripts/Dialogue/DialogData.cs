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
    public bool haveToHaveAndLikeHouse; //집을 가져야  하는지
    public bool dontHaveToHaveAndLikeHouse; //집을 가지지 말아야 하는지
    public int[] haveToHaveNPCHouse; //집 있어야 하는 npc
    public QuestRewards[] acceptQuestItems;//대화 종료 시 획득하는 아이템

    [System.Serializable]
    public class QuestRewards
    {
        public Item itemType;
        public int getCount;
    }


    private void OnEnable()
    {
        isTalkingOver = false;
    }
    
    //public bool CanStartTalk()
    //{
    ////입주 필수 인가?  haveToHaveAndLikeHouse
    ////그렇다면 집을 갖고 있는가?
    ////그렇다면 집의 점수가 50점 초과인가?

    //foreach (var item in dialogTasks.haveToClearQuest)
    //{
    //    if (!SuperManager.Instance.questmanager.IsQuestCleared(item.questdata))
    //        return false;
    //}
    //foreach (var item in dialogTasks.DonthaveToClearQuest)
    //{
    //    if (SuperManager.Instance.questmanager.IsQuestCleared(item.questdata))
    //        return false;
    //}
    //foreach (var item in dialogTasks.haveToDoingQuest)
    //{
    //    if (!SuperManager.Instance.questmanager.IsQuestAccepted(item.questdata))
    //        return false;

    //}
    //foreach (var item in dialogTasks.buildBuildings)
    //{
    //    //빌딩매니저에서 해당 건축물이 어느 집에 설치되어있을 때..
    //}
    //foreach (var item in dialogTasks.haveToEndDialog)
    //{
    //    if(!item.isTalkingOver)
    //    {
    //        return false;

    //    }
    //}
    //foreach (var item in dialogTasks.DonthaveToEndDialog)
    //{
    //    if (item.isTalkingOver)
    //    {
    //        return false;

    //    }
    //}
    ////if( dialogTasks.itemIndexToHand == 
    ////{
    ////    //플레이어의 hand 체크
    ////}
    //return true;
    //}

}
public enum TextboxType { Normal, Cloud, Sharp }

[System.Serializable]
public class Sentence
{
    [TextArea] public string sentence;
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
    public DialogData[] haveToEndDialog;
    public QuestIndexSet[] DonthaveToClearQuest; //클리어 하지 말아야 하는 퀘스트
    public DialogData[] DonthaveToEndDialog;
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
