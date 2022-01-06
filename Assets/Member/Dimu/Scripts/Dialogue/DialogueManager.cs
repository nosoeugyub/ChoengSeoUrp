using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace DM.Dialog
{

    public class DialogueManager : MonoBehaviour
    {
        DialogData nowDialogData;//현재담겨있는 데이터 
        public GameObject dialogUI;
        public Text dialogText;
        public Text nameText;


        void Start()
        {
            StartShowDialog("testJson");

            //DialogData DialogData = new DialogData(2);
            //DialogData.sentenceInfo[0] = new Sentence("Dimu", 1);
            //DialogData.sentenceInfo[1] = new Sentence("mumu", 2);
            //CreateJsonFile(Application.dataPath, "testJson", DialogData);
        }
        public void UpdateDialog()
        {
            //for (int i = 0; nowDialogData.sentenceInfo.Length < i; ++i)
            {
                dialogText.text = nowDialogData.sentenceInfo[0].sentence;
                nameText.text = nowDialogData.sentenceInfo[0].characterId + "";
            }
        }
        public void StartShowDialog(string eventname)
        {
            dialogUI.SetActive(true);
            LoadDialogData(eventname);
            UpdateDialog();
        }
        public void LoadDialogData(string eventname)
        {
            //Application.persistentDataPath
            string filePath = Application.dataPath + "/" + eventname + ".Json";
            if (File.Exists(filePath))
            {
                print(filePath);

                string FromJsonData = File.ReadAllText(filePath);
                nowDialogData = JsonUtility.FromJson<DialogData>(FromJsonData);
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
            string path = createPath + "/" + fileName + ".Json";
            File.WriteAllText(path, json);
        }
    }

    [System.Serializable]
    public class DialogData
    {
        public Sentence[] sentenceInfo;
        public DialogData(int slength) { sentenceInfo = new Sentence[slength]; }
    }
    [System.Serializable]
    public class Sentence
    {
        public string sentence;
        public int characterId;

        public Sentence(string sentence_, int characterId_)
        { sentence = sentence_; characterId = characterId_; }
    }

}