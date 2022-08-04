using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
   private void Save()
    {
        SaveDataInfo SaveDatainfo = new SaveDataInfo
        {
        //    PlayerPostion = 
        };
        string _saveJson = JsonUtility.ToJson(SaveDatainfo);

        File.WriteAllText(Application.dataPath + "/save.txt", _saveJson);

    }
    private void Load()
    {

    }
}

public class SaveDataInfo
{
    public Vector3 PlayerPostion;

   
   public class ItemslotSaveData
    {
        public string ItemID;
        public int Amount;

        public ItemslotSaveData(string id, int amount)
        {
            ItemID = id;
            Amount = amount;
        }

    }
    public class itemContainerSaveData
    {
        public ItemslotSaveData[] SaveSlot;

        public itemContainerSaveData(int numItems)
        {
            SaveSlot = new ItemslotSaveData[numItems];
        }
    }

}
