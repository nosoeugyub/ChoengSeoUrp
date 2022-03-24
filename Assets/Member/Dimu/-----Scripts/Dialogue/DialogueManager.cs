﻿using DM.Quest;
using Game.NPC;
using NSY.Manager;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public enum Character
{ CheongSeo, Ejang, Hen, Walrus, Bee, Rabbit, Deer, Milkcow, Sheep, Length }
//청서 곰 닭 바코 벌 토끼 사슴 젖소 양

namespace DM.Dialog
{

    public class DialogueManager : MonoBehaviour
    {
        DialogData nowDialogData;//현재담겨있는 대화 데이터 
        int dialogLength;
        int nowSentenceIdx;
        public Transform partnerTf;

        [Header("UI")]
        public GameObject dialogUI;//대화창 조상
        public Button nextButton; //다음 버튼
        public Text dialogText;
        public Text nameText;

        [Header("DialogInfos")]
        public DialogList[] questDialogLists; //퀘스트 있는 대화
        public DialogList[] dailydialogLists; //퀘스트 없는 대화
        //ublic int[] dialogIdxs = new int[(int)Character.Length];//0부터 캐릭터 인덱스 값은 대화인덱스

        public int nowPartner = -1; //일단 이장 고정
        MainNpc nowNpc;
        bool isTalking = false;
        float times = 0;
        QuestManager questManager;

        private void Awake()
        {
            questManager = SuperManager.Instance.questmanager;
        }
        void Start()
        {
            EventManager.EventActions[1] = Test;
            //StartShowDialog("testJson");

            //DialogData DialogData = new DialogData(2, 1,1,2);
            //DialogData.questId = 0;
            //DialogData.subjectCharacterID = 1;
            //DialogData.acceptSentenceInfo[0] = new Sentence("퀘스트를 받아주게나", 1);
            //DialogData.acceptSentenceInfo[1] = new Sentence("알겠습니다", 0);
            //DialogData.proceedingSentenceInfo[0] = new Sentence("퀘스트 진행중...", 1);
            //DialogData.clearSentenceInfo[0] = new Sentence("클리어~~", 1);
            //DialogData.clearSentenceInfo[1] = new Sentence("와~", 0);
            //CreateJsonFile(Application.dataPath, "FirstTalkWithHim", DialogData);
        }
        public void Test()
        {
            print("event test");
            times += Time.deltaTime;
            if (times > 3) { EventManager.EventAction -= EventManager.EventActions[1]; }
        }

        public void FirstShowDialog(MainNpc npc) //첫 상호작용 시 호출. 어떤 대화를 호출할지 결정
        {
            if (isTalking) return;

            isTalking = true;
            nowPartner = npc.GetCharacterType();  //대화하는 대상을 현재 파트너로 지정
            nowNpc = npc;
            partnerTf = npc.transform;

            PlayerData.AddValue(npc.GetCharacterType(), (int)NpcBehaviorEnum.Interact, PlayerData.npcData, (int)NpcBehaviorEnum.length);

            StartShowDialog(); //파트너와 진행해야 하는 순서의 대화를 진행
        }

        //public void FirstShowDialog(int charId, Transform transform) //첫 상호작용 시 호출
        //{
        //    if(isTalking) return;
        //    isTalking = true;
        //    nowPartner = charId;  //대화하는 대상을 현재 파트너로 지정
        //    partnerTf = transform;
        //
        //    PlayerData.AddValue(charId, (int)NpcBehaviorEnum.Interact, PlayerData.npcData,(int)NpcBehaviorEnum.length);
        //    //PlayerData.npcData[charId].amounts[0]++; //charId npc와 1번 상호작용 했다.
        //
        //    StartShowDialog(); //파트너와 진행해야 하는 순서의 대화를 진행
        //}
        public void StartShowDialog()
        {
            Sentence[] ss = null;//대화뭉치를 담을 변수
            int sentenceState = -1;//대화뭉치 타입(수락0, 진행중1, 완료2)

            //현재 진행중인 퀘스트에서 자신과 상호작용 하는 내용의 퀘스트가 있는지?
            //있다면 해당 퀘스트의 CanClear 검사
            //클리어할 수 있다면 해당 퀘스트 클리어 처리 후 완료 대사를 ss에 넣는다.
            dialogUI.SetActive(true);
            Vector3 uiPos = new Vector3(partnerTf.position.x, partnerTf.position.y + 6, partnerTf.position.z);
            dialogUI.transform.position = Camera.main.WorldToScreenPoint(uiPos);

            QuestData qd = questManager.ReturnQuestRequireNpc(nowPartner);

            List<QuestData> isAcceptedQuests = questManager.GetIsAcceptedQuestList(nowPartner);//진행중인 퀘스트
            List<QuestData> canAcceptQuests = questManager.GetCanAcceptQuestList(nowPartner);//수락가능 퀘스트
            //완료자가 nowPartner(현재 대화 상대)인 퀘스트 받아옴. 제공자는 같을 수도,  다를 수 있음.
            if (qd != null && questManager.ClearQuest(qd.questID, qd.npcID))//없거나 클리어할 수 없다면
            {
                LoadDialogData(qd.npcID, qd.questID, true); //제공자의 퀘스트의 해당 대화 데이터 불러오기.
                ss = nowDialogData.clearSentenceInfo;
                dialogLength = nowDialogData.clearSentenceInfo.Length;
                sentenceState = 2;//클리어
            }

            else if (isAcceptedQuests.Count > 0)
            {
                LoadDialogData(nowPartner, isAcceptedQuests[0].questID, true);

                ss = nowDialogData.proceedingSentenceInfo;
                dialogLength = nowDialogData.proceedingSentenceInfo.Length;
                sentenceState = 1;//진행중
            }
            else if (canAcceptQuests.Count > 0)
            {
                LoadDialogData(nowPartner, canAcceptQuests[0].questID, true);

                ss = nowDialogData.acceptSentenceInfo;
                dialogLength = nowDialogData.acceptSentenceInfo.Length;
                sentenceState = 0;//수락
            }
            else
            {
                LoadDialogData(nowPartner, 0, false);

                Debug.LogError("StartShowDialog :: nothing else");
                dialogUI.SetActive(false);
                isTalking = false;
                return;
            }


            //if (questDialogLists[nowPartner].dialogList.Length - dialogIdxs[nowPartner] < 1 ||
            //    !LoadDialogData(nowPartner, dialogIdxs[nowPartner], true)) //해당 대화 데이터 불러오기 실패 시
            //{
            //    Debug.LogError("StartShowDialog :: LoadDialogData fail, no data");
            //    dialogUI.SetActive(false);
            //    isTalking = false;
            //    return;
            //}
            //if (questManager.IsQuestAccepted(nowDialogData.questId, nowPartner))//진행해야 하는 퀘 수락중인지?
            //{
            //    ss = nowDialogData.proceedingSentenceInfo;
            //    dialogLength = nowDialogData.proceedingSentenceInfo.Length;
            //    sentenceState = 1;//진행중
            //
            //}
            //else if (questManager.CanAcceptQuest(nowDialogData.questId, nowPartner))//클리어X수락X, 수락가능한지?
            //{
            //    ss = nowDialogData.acceptSentenceInfo;
            //    dialogLength = nowDialogData.acceptSentenceInfo.Length;
            //    sentenceState = 0;//수락
            //}
            //else //아무것도 없을 때
            //{
            //    Debug.LogError("StartShowDialog :: nothing else");
            //    dialogUI.SetActive(false);
            //    isTalking = false;
            //    return;
            //}

            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() =>
            {
                UpdateDialog(ss, sentenceState);
            });
            UpdateDialog(ss, sentenceState);
        }
        //public void StartShowDialog()
        //{
        //    Sentence[] ss = null;//대화뭉치를 담을 변수
        //    int sentenceState = -1;//대화뭉치 타입(수락0, 진행중1, 완료2)

        //    //현재 진행중인 퀘스트에서 자신과 상호작용 하는 내용의 퀘스트가 있는지?
        //    //있다면 해당 퀘스트의 CanClear 검사
        //    //클리어할 수 있다면 해당 퀘스트 클리어 처리 후 완료 대사를 ss에 넣는다.
        //    dialogUI.SetActive(true);
        //    Vector3 uiPos = new Vector3(partnerTf.position.x, partnerTf.position.y+6, partnerTf.position.z);
        //    dialogUI.transform.position = Camera.main.WorldToScreenPoint(uiPos) ;

        //    QuestData qd = questManager.ReturnQuestRequireNpc(nowPartner);
        //    //완료자가 nowPartner(현재 대화 상대)인 퀘스트 받아옴. 제공자는 같을 수도,  다를 수 있음.
        //    if (qd != null && questManager.ClearQuest(qd.questID, qd.npcID))//없거나 클리어할 수 없다면
        //    {
        //        LoadDialogData(qd.npcID, dialogIdxs[qd.npcID]); //제공자의 퀘스트의 해당 대화 데이터 불러오기.
        //        ss = nowDialogData.clearSentenceInfo;
        //        dialogLength = nowDialogData.clearSentenceInfo.Length;
        //        sentenceState = 2;//클리어

        //        dialogIdxs[nowPartner]++;
        //    }

        //    else if (questDialogLists[nowPartner].dialogList.Length - dialogIdxs[nowPartner] < 1 || 
        //        !LoadDialogData(nowPartner, dialogIdxs[nowPartner])) //해당 대화 데이터 불러오기 실패 시
        //    {
        //        Debug.LogError("StartShowDialog :: LoadDialogData fail, no data");
        //        dialogUI.SetActive(false);
        //        isTalking = false;
        //        return;
        //    }
        //    else if (questManager.IsQuestAccepted(nowDialogData.questId, nowPartner))//진행해야 하는 퀘 수락중인지?
        //    {
        //        ss = nowDialogData.proceedingSentenceInfo;
        //        dialogLength = nowDialogData.proceedingSentenceInfo.Length;
        //        sentenceState = 1;//진행중

        //    }
        //    else if (questManager.CanAcceptQuest(nowDialogData.questId, nowPartner))//클리어X수락X, 수락가능한지?
        //    {
        //        ss = nowDialogData.acceptSentenceInfo;
        //        dialogLength = nowDialogData.acceptSentenceInfo.Length;
        //        sentenceState = 0;//수락
        //    }
        //    else //아무것도 없을 때
        //    {
        //        Debug.LogError("StartShowDialog :: nothing else");
        //        dialogUI.SetActive(false);
        //        isTalking = false;
        //        return;
        //    }

        //    nextButton.onClick.RemoveAllListeners();
        //    nextButton.onClick.AddListener(() =>
        //    {
        //        UpdateDialog(ss, sentenceState);
        //    });
        //    UpdateDialog(ss, sentenceState);
        //}

        public void UpdateDialog(Sentence[] sentences, int sentenceState)
        {
            UpdateDialogText(sentences);
            if (dialogLength == nowSentenceIdx)
                LastDialog(sentenceState);
        }

        private void UpdateDialogText(Sentence[] sentences)
        {
            if (sentences[nowSentenceIdx].eventIdx > 0)
                EventManager.EventAction += EventManager.EventActions[sentences[nowSentenceIdx].eventIdx];

            dialogText.text = sentences[nowSentenceIdx].sentence;
            nameText.text = questDialogLists[sentences[nowSentenceIdx++].characterId].charName;
        }

        //마지막 대사일 때 작동
        private void LastDialog(int sentenceState)
        {
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() =>
            {
                CloseDialog();
                isTalking = false;
            });
            //만약 대화데이터에 퀘스트가 있다면
            if (nowDialogData.questId > -1)
            {
                //완료 상태 아니라면 강제수락
                print(sentenceState);
                switch (sentenceState)
                {
                    case 0://수락 상태라면?
                        questManager.AcceptQuest(nowDialogData.questId, nowPartner);
                        break;
                    default:
                        break;
                }
            }
            //수락 시 이벤트가 있다면 진행
        }

        private void CloseDialog()
        {
            dialogUI.SetActive(false);
        }

        #region Data
        public bool LoadDialogData(int charId, int diaIdx, bool isQuestDialog)//string eventname)
        {
            string filePath = "dimu";
            if (isQuestDialog)
            {
                //Application.persistentDataPath
                filePath = Application.dataPath + "/JsonData/" + questDialogLists[charId].dialogList[diaIdx] + ".Json";
            }
            else
            {
                filePath = Application.dataPath + "/JsonData/" + dailydialogLists[charId].dialogList[diaIdx] + ".Json";

            }
            if (File.Exists(filePath))
            {
                //print(filePath);

                string FromJsonData = File.ReadAllText(filePath);
                nowDialogData = JsonUtility.FromJson<DialogData>(FromJsonData);
                nowSentenceIdx = 0;
                Debug.Log(nowSentenceIdx + " dialogIndex 초기화");
                return true;
            }
            else
            {
                Debug.Log("경로에 파일이 없음");
                return false;
            }
        }

        void CreateJsonFile(string createPath, string fileName, object obj)
        {
            string json = JsonUtility.ToJson(obj);
            print(json);
            string path = createPath + "/JsonData/" + fileName + ".Json";
            File.WriteAllText(path, json);
        }
        #endregion
    }

    [System.Serializable]
    public class DialogData
    {
        public Sentence[] acceptSentenceInfo;//시작 문장뭉치
        public Sentence[] proceedingSentenceInfo;//진행중 문장뭉치
        public Sentence[] clearSentenceInfo;//완료 문장뭉치

        public int questId; //문장이 끝나면 주어질 퀘스트
        public int subjectCharacterID; //대화 주체(상대)

        public DialogData(int slength, int charId, int proSLength, int clearSLength)
        {
            acceptSentenceInfo = new Sentence[slength];
            proceedingSentenceInfo = new Sentence[proSLength];
            clearSentenceInfo = new Sentence[clearSLength];
            subjectCharacterID = charId;
        }
    }
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
        QuestIndexSet haveToClear;
        QuestIndexSet haveToDoing;
        int itemIndexToBuild;
        int itemIndexToHand;
    }
    public class QuestIndexSet
    {
        public int npcid;
        public int questid;
    }
}