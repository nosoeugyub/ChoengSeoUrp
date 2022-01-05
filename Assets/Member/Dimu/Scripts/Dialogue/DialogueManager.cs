using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace DM.Dialog {
    public class DialogData
    {
        public Sentence[] sentence;
    }
    public class Sentence
    {
        public string sentence;
        public int characterId;
    }

    public class DialogueManager : MonoBehaviour
    {
        DialogData nowDialogData;//현재담겨있는 데이터 

        void Start()
        {
            LoadGameData();
            DialogData DialogData = new DialogData(22, "dimu");
            CreateJsonFile(Application.dataPath, "testJson", DialogData);
        }

        public void LoadDialogData(string eventname)
        {
            string filePath = Application.persistentDataPath + "/GameJson.json";

        }

        void CreateJsonFile(string createPath, string fileName, object obj)
        {
            string json = JsonUtility.ToJson(obj);
            print(json);
            string path = createPath + "/" + fileName + ".Json";
            File.WriteAllText(path, json);
        }
        public void LoadGameData()                                                                                                                  //게임 데이터 불러오기
        {
            string filePath = Application.persistentDataPath + "/GameJson.json";


            if (File.Exists(filePath))
            {
                print(filePath);

                string FromJsonData = File.ReadAllText(filePath);
                DialogData _gameData = JsonUtility.FromJson<DialogData>(FromJsonData);
            }
            else
            {
                // UnityEngine.Debug.Log("새 파일 생성!");
                DialogData _gameData = new DialogData();
                ToGameJson();

            }
        }
        [ContextMenu("To Game Json")]
        public void ToGameJson()
        {
            Debug.Log("togamejason");
            //File.WriteAllText(Application.persistentDataPath + "/GameJson.json", JsonUtility.ToJson(gameData, true));
        }
    }
}