using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "QuestData/new QuestData", order = 0)]
public class DialogData : ScriptableObject
{
    DialogTask dialogTask;

    public Sentence[] acceptSentenceInfo;//시작 문장뭉치
    public Sentence[] proceedingSentenceInfo;//진행중 문장뭉치
    public Sentence[] clearSentenceInfo;//완료 문장뭉치

    public int questId; //문장이 끝나면 주어질 퀘스트
    public int subjectCharacterID; //대화 주체(상대)
}
\

[System.Serializable]
public class Sentence
{
    public string sentence;
    public int characterId;
    public int eventIdx;

    public Sentence(string sentence_, int characterId_)
    { sentence = sentence_; characterId = characterId_; }
}

[System.Serializable]
public class DialogList
{
    public string charName;
    public string[] dialogList;
}

[System.Serializable]
public class DialogTask
{
    QuestIndexSet[] haveToClear;
    QuestIndexSet[] haveToDoing;
    int[] itemIndexToBuild;
    int[] itemIndexToHand;
}
[System.Serializable]
public class QuestIndexSet
{
    public int npcid;
    public int questid;
}
