using NSY.Manager;
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
        public Button spawnStartCanAcceptQuestButton;

        void Start()
        {
            //StartShowDialog("testJson");

            //DialogData DialogData = new DialogData(2, 1, 2, 1);
            //DialogData.questId = 0;
            //DialogData.subjectCharacterID = 1;
            //DialogData.acceptSentenceInfo[0] = new Sentence("그래그래 수락해야지", 1);
            //DialogData.acceptSentenceInfo[1] = new Sentence("어서가고", 1);
            //DialogData.rejectSentenceInfo[0] = new Sentence("이걸 거절해?", 1);
            //DialogData.sentenceInfo[0] = new Sentence("디무", 1);
            //DialogData.sentenceInfo[1] = new Sentence("mumu", 2);
            //CreateJsonFile(Application.dataPath, "testJson3", DialogData);
        }
        public void FirstShowDialog(int charId) //첫 상호작용 시 호출
        {
            dialogUI.SetActive(true);
            //기본 대화를 출력한다. < < 생략 LoadDialogData(charId, diaIdx);
            //charid에 해당하는 퀘스트를 다 뒤져서 수행 가능한 개수의 퀘스트 버튼을 생성한다.
            //수행 가능한 퀘스트가 아니라 대화로 해야할 것 같음.
            foreach (var questData in  SuperManager.Instance.questmanager.questLists[charId].questList)
            {
                if(questData.CanAccept())
                {
                    //퀘스트 버튼(spawnStartCanAcceptQuestButton) 생성 후 리스너 등록
                    spawnStartCanAcceptQuestButton.onClick.AddListener(() =>
                    {
                        StartShowDialog(charId, questData.QuestID);
                    });
                }

            }
           
        }
        public void StartShowDialog(int charId, int diaIdx)//(string eventname)
        {
            AcceptRejectButtonOnOff(false);
            LoadDialogData(charId, diaIdx);
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() =>
            {
                UpdateDialog();
            });
            UpdateDialog();
        }
        public void UpdateDialog() //
        {
            UpdateDialogText(nowDialogData.sentenceInfo);
            if (dialogLength == dialogIndex)
                LastDialog();
        }

        private void UpdateDialogText(Sentence[] sentences)
        {
            dialogText.text = sentences[dialogIndex].sentence;
            nameText.text = sentences[dialogIndex++].characterId + "";//NPC 이름으로 변경해줘야함
        }

        //마지막 대사일 때 작동
        private void LastDialog()
        {
            nextButton.gameObject.SetActive(false);
            //만약 대화데이터에 퀘스트가 있다면
            if (nowDialogData.questId > -1)
            {

                //수락 거절 버튼 띄우고 
                AcceptRejectButtonOnOff(true);
                acceptButton.onClick.AddListener(() =>
                {
                    //퀘수락
                    SuperManager.Instance.questmanager.AcceptQuest(nowDialogData.questId, nowDialogData.subjectCharacterID);
                    //수락 대화 출력
                    SetAnotherDialog(nowDialogData.acceptSentenceInfo);
                });
                rejectButton.onClick.AddListener(() =>
                {
                    //거절 대화 출력
                    SetAnotherDialog(nowDialogData.rejectSentenceInfo);
                });
            }
            else
                CloseDialog();
        }

        private void SetAnotherDialog(Sentence[] sentences)
        {
            dialogIndex = 0;

            nextButton.gameObject.SetActive(true);

            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() =>
            {
                AnotherDialog(sentences);
            });

            AnotherDialog(sentences);
            AcceptRejectButtonOnOff(false);
        }

        private void AnotherDialog(Sentence[] sentences)
        {
            if (sentences.Length == dialogIndex)
            {
                CloseDialog();
                return;
            }
            UpdateDialogText(sentences);
        }
        private void AcceptRejectButtonOnOff(bool isOn)
        {
            acceptButton.gameObject.SetActive(isOn);
            rejectButton.gameObject.SetActive(isOn);
        }

        private void CloseDialog()
        {
            dialogUI.SetActive(false);
        }

        public void TestEvent()
        {
            StartShowDialog(0, 0);
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
        #endregion
    }

    [System.Serializable]
    public class DialogData
    {
        public Sentence[] sentenceInfo;
        public int questId;
        public int subjectCharacterID;

        public Sentence[] acceptSentenceInfo;
        public Sentence[] rejectSentenceInfo;

        public DialogData(int slength, int charId, int accSLength, int rejSLength)
        {
            sentenceInfo = new Sentence[slength];
            acceptSentenceInfo = new Sentence[accSLength];
            rejectSentenceInfo = new Sentence[rejSLength];
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