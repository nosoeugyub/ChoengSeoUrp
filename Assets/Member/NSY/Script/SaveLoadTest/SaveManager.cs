using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SaveManager : MonoBehaviour 
{
  public static SaveManager instance { get; private set; }

    private SaveData saveData;

    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("싱글톤 없음");
        }
        instance = this;
    }

    private void Start()
    {
        LoadGame();
    }
    public void NewGame()
    {
        this.saveData = new SaveData();
    }

    public void LoadGame()
    {
        //저장된 데이터들 불러오기
        if (this.saveData == null)
        {
            Debug.Log("없엉");
            NewGame();
        }
    }

    public void SaveGame()
    {

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
