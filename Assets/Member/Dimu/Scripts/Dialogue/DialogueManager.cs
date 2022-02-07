﻿using DM.Quest;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public enum Character
{ CheongSeo, Ejang, Length }

namespace DM.Dialog
{

    public class DialogueManager : MonoBehaviour
    {
        DialogData nowDialogData;//현재담겨있는 대화 데이터 
        int dialogLength;
        int dialogIndex;

        [Header("UI")]
        public GameObject dialogUI;//대화창 조상
        public Button nextButton; //다음 버튼
        public Text dialogText;
        public Text nameText;

        [Header("DialogInfos")]
        public DialogList[] dialogLists;
        public int[] dialogIdxs = new int[(int)Character.Length];//0부터 캐릭터 인덱스 값은 대화인덱스

        public int nowPartner = 1; //일단 이장 고정

        QuestManager questManager;

        private void Awake()
        {
            questManager = FindObjectOfType<QuestManager>();
        }
        void Start()
        {
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
        public void FirstShowDialog(int charId) //첫 상호작용 시 호출
        {
            nowPartner = charId;  //대화하는 대상을 현재 파트너로 지정

            PlayerData.npcData[charId]++; //charId npc와 1번 상호작용 했다.

            StartShowDialog(dialogIdxs[nowPartner]); //파트너와 진행해야 하는 순서의 대화를 진행
        }
        public void StartShowDialog(int diaIdx)
        {
            LoadDialogData(nowPartner, diaIdx); //해당 대화 데이터 불러오기.
            Sentence[] ss = null;//대화뭉치를 담을 변수

            //현재 진행중인 퀘스트에서 자신과 상호작용 하는 내용의 퀘스트가 있는지?
            //있다면 해당 퀘스트의 CanClear 검사
            //클리어할 수 있다면 해당 퀘스트 클리어 처리 후 완료 대사를 ss에 넣는다.
            dialogUI.SetActive(true);

            QuestData qd = questManager.ReturnQuestRequireNpc(nowPartner);
            //완료자가 nowPartner(현재 대화 상대)인 퀘스트 받아옴. 제공자는 같을 수도,  다를 수 있음.
            if (qd != null && questManager.ClearQuest(qd.questID, qd.npcID))//없거나 클리어할 수 없다면
            {
                LoadDialogData(qd.npcID, dialogIdxs[qd.npcID]); //제공자의 퀘스트의 해당 대화 데이터 불러오기.
                ss = nowDialogData.clearSentenceInfo;
                dialogLength = nowDialogData.clearSentenceInfo.Length;

                dialogIdxs[nowPartner]++;
            }

            else if (questManager.IsQuestAccepted(nowDialogData.questId, nowPartner))//진행해야 하는 퀘 수락중인지?
            {
                ss = nowDialogData.proceedingSentenceInfo;
                dialogLength = nowDialogData.proceedingSentenceInfo.Length;
            }

            else if (questManager.CanAcceptQuest(nowDialogData.questId, nowPartner))//클리어X수락X, 수락가능한지?
            {
                ss = nowDialogData.acceptSentenceInfo;
                dialogLength = nowDialogData.acceptSentenceInfo.Length;
            }
            else //아무것도 없을 때
            { 
                Debug.LogError("StartShowDialog :: Sentence null");
                dialogUI.SetActive(false);
                return; 
            }



            //if (questManager.CanClear(nowDialogData.questId, nowPartner) //클리어 가능한지?
            //    || questManager.IsQuestAccepted(nowDialogData.questId, nowPartner)//진행중인지?
            //    || questManager.CanAcceptQuest(nowDialogData.questId, nowPartner))//수락가능한지?
            //{
            //    LoadDialogData(nowPartner, 0);//0번을 기본대화로 할까 생각중.
            //}

            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() =>
            {
                UpdateDialog(ss);
            });
            UpdateDialog(ss);
        }
        public void UpdateDialog(Sentence[] sentences)
        {
            UpdateDialogText(sentences);
            if (dialogLength == dialogIndex)
                LastDialog();
        }

        private void UpdateDialogText(Sentence[] sentences)
        {
            dialogText.text = sentences[dialogIndex].sentence;
            nameText.text = dialogLists[sentences[dialogIndex++].characterId].charName;
        }

        //마지막 대사일 때 작동
        private void LastDialog()
        {
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() =>
            {
                CloseDialog();
            });
            //만약 대화데이터에 퀘스트가 있다면
            if (nowDialogData.questId > -1)
            {
                //완료 상태 아니라면 강제수락
                if (!questManager.IsQuestCleared(nowDialogData.questId, nowPartner))
                    questManager.AcceptQuest(nowDialogData.questId, nowPartner);
            }
        }

        private void CloseDialog()
        {
            dialogUI.SetActive(false);
        }

        #region Data
        public void LoadDialogData(int charId, int diaIdx)//string eventname)
        {
            //Application.persistentDataPath
            string filePath = Application.dataPath + "/JsonData/" + dialogLists[charId].dialogList[diaIdx] + ".Json";
            if (File.Exists(filePath))
            {
                //print(filePath);

                string FromJsonData = File.ReadAllText(filePath);
                nowDialogData = JsonUtility.FromJson<DialogData>(FromJsonData);
                //dialogLength = nowDialogData.acceptSentenceInfo.Length;
                dialogIndex = 0;
                Debug.Log(dialogIndex + " dialogIndex 초기화");
            }
            else
            {
                Debug.Log("경로에 파일이 없음");
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
    public class DialogProgress
    {
        public int dialogCount;
    }
}