using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NSY.Iven;

public class UnLockManager : MonoBehaviour
{
    [Header("조건을 채울 컴포넌트들")]

  //  [SerializeField] UnLockTable unlocktable;
    [SerializeField] InventoryNSY inventory;

    //조건함수 이벤트
    public delegate bool getitemUnlock(UnLockTable item);
    public static event getitemUnlock GetItemUnlock; //아이템 흭득시 아이템 넘버를 받아와서 조건탐색

    public delegate bool getRecipeUnlock(UnLockTable item);
    public static event getRecipeUnlock GetRecipeUnlock; //레시피 흭득시 레시피 넘버를 받아와서 조건탐색


    //조건 갯수 ex) 일정 아이템을 몇개이상 얻었을때 해금
    public bool GetAmountLock(UnLockTable unlocktalbeitem )
    {
        for (int i = 0; i < inventory.ItemSlots.Count; i++)
        {
            for (int j = 0; j < unlocktalbeitem.FindLockItem.Count; j++)
            {
                if (true)
                {

                    return true;
                }


              
            }
        }
     

        return false;
    }


   
}
