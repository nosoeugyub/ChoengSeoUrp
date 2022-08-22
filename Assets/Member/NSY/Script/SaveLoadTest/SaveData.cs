using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//기본적인 게임 데이터 저장
//시작할때는 모두 초기화
[System.Serializable]
public class SaveData
{
    public Vector3 PlayerVector; 

    public SaveData()
    {
       
    }
}







/// <summary>
///  아이템 슬롯 저장 
/// </summary>

[System.Serializable]
public class ItemSloatSaveData
{
    public string ItemID;
    public int Amount;

    public ItemSloatSaveData(string id, int amount)
    {
        ItemID = id;
        Amount = amount;
    }
}

[System.Serializable]
public class ItemsContainerSaveData
{
    public ItemSloatSaveData[] SavedSlots;

    public ItemsContainerSaveData(int numItems)
    {
        SavedSlots = new ItemSloatSaveData[numItems];
    }
}