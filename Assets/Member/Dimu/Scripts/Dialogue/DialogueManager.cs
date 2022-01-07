using NSY.Manager;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace DM.Dialog
{

    public class DialogueManager : MonoBehaviour
    {
        DialogData nowDialogData;//현재담겨있는 데이터 
        public GameObject dialogUI;//대화창 조상
        public Button nextButton;
        public Button acceptButton;
        public Button rejectButton;
        public Text dialogText;
        public Text nameText;
        public int dialogLength;
        public int dialogIndex;
        public DialogList[] dialogLists;


        void Start()
        {
            //StartShowDialog("testJson");

            DialogData DialogData = new DialogData(2, 1);
            DialogData.questId = 0;
            DialogData.subjectCharacterID = 1;
            DialogData.sentenceInfo[0] = new Sentence("디무", 1);
            DialogData.sentenceInfo[1] = new Sentence("mumu", 2);
            CreateJsonFile(Application.dataPath, "testJson3", DialogData);
        }
        public void UpdateDialog()
        {
            dialogText.text = nowDialogData.sentenceInfo[dialogIndex].sentence;
            nameText.text = nowDialogData.sentenceInfo[dialogIndex++].characterId + "";//NPC 이름으로 변경해줘야함
            IfLastDialog();
        }

        private void IfLastDialog()
        {
            if (dialogLength <= dialogIndex)
            {
                //마지막 대사일 때 작동
                nextButton.gameObject.SetActive(false);

                //만약 대화데이터에 퀘스트가 있다면
                if (nowDialogData.questId > -1)
                {
                    //수락 거절 버튼 띄우고 
                    acceptButton.gameObject.SetActive(true);
                    rejectButton.gameObject.SetActive(true);
                    acceptButton.onClick.AddListener(() =>
                    {
                        SuperManager.Instance.questmanager.AcceptQuest(nowDialogData.questId, nowDialogData.subjectCharacterID);
                        //수락 대화 출력
                        AcceptDialog();
                        dialogUI.SetActive(false);
                    });
                    rejectButton.onClick.AddListener(() =>
                    {
                        //거절 대화 출력
                        RejectDialog();
                        dialogUI.SetActive(false);
                    });
                    return;
                }
            }
        }

        private void RejectDialog()
        {
            nextButton.gameObject.SetActive(true);
            dialogText.text = nowDialogData.rejectSentenceInfo[dialogIndex].sentence;
            nameText.text = nowDialogData.rejectSentenceInfo[dialogIndex++].characterId + "";
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() =>
            {
                RejectDialog();
            });
        }
        private void AcceptDialog()
        {
            nextButton.gameObject.SetActive(true);
            dialogText.text = nowDialogData.acceptSentenceInfo[dialogIndex].sentence;
            nameText.text = nowDialogData.acceptSentenceInfo[dialogIndex++].characterId + "";
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() =>
            {
                AcceptDialog();
            });
            //next 버튼에 UpdateDialog()말고 AcceptDialog() addlistener하기
        }

        public void TestEvent()
        {
            StartShowDialog(0, 0);
        }
        public void StartShowDialog(int charId, int diaIdx)//(string eventname)
        {
            dialogUI.SetActive(true);
            LoadDialogData(charId, diaIdx);
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() =>
            {
                UpdateDialog();
            });
                UpdateDialog();
        }
        public void LoadDialogData(int charId, int diaIdx)//string eventname)
        {
            //Application.persistentDataPath
            string filePath = Application.dataPath + "/JsonData/" + dialogLists[charId].dialogList[diaIdx] + ".Json";
            if (File.Exists(filePath))
            {
                //print(filePath);

                string FromJsonData = File.ReadAllText(filePath);
                nowDialogData = JsonUtility.FromJson<DialogData>(FromJsonData);
                dialogLength = nowDialogData.sentenceInfo.Length;
                dialogIndex = 0;
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
    }

    [System.Serializable]
    public class DialogData
    {
        public Sentence[] sentenceInfo;
        public int questId;
        public int subjectCharacterID;

        public Sentence[] acceptSentenceInfo;
        public Sentence[] rejectSentenceInfo;

        public DialogData(int slength, int charId)
        {
            sentenceInfo = new Sentence[slength];
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
}