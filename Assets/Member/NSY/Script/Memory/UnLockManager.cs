using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class UnLockManager : MonoBehaviour
{
    [SerializeField] UnLockTable unlocktable;


    //조건함수 이벤트
    public delegate bool getitemUnlock(int itemAmount);
    public static event getitemUnlock GetItemUnlock; //아이템 흭득시 아이템 넘버를 받아와서 조건탐색

    public delegate bool getRecipeUnlock(Item item);
    public static event getRecipeUnlock GetRecipeUnlock; //레시피 흭득시 레시피 넘버를 받아와서 조건탐색





   
}
