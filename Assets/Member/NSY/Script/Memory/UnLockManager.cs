using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NSY.Iven;

public class UnLockManager : MonoBehaviour
{
    [Header("조건을 채울 컴포넌트들")]

    [SerializeField] UnLockTable unlocktable;
    [SerializeField] InventoryNSY inventory;

    //조건함수 이벤트
    public delegate bool getitemUnlock(Item item);
    public static event getitemUnlock GetItemUnlock; //아이템 흭득시 아이템 넘버를 받아와서 조건탐색

    public delegate bool getRecipeUnlock(Item item);
    public static event getRecipeUnlock GetRecipeUnlock; //레시피 흭득시 레시피 넘버를 받아와서 조건탐색


    //조건 갯수
    public bool GetAmountLock()
    {

        foreach (ItemSlot item in inventory.ItemSlots)
        {

        }

        return false;
    }


   
}
