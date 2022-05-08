﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]



//조건상세테이블
[CreateAssetMenu(fileName = "Lockitems", menuName = "LockItemDatatable")]

public class UnLockTable : ScriptableObject
{
   
   //언락에 필요한 아이템 + 초기화
   [Header("잠겨있는  아이템")]
    [SerializeField]
    public Item[] _LockItem;
    public Item this[int itemIndex]
    {
        get
        {
            if (itemIndex >= _LockItem.Length)
            {
                Debug.Log("아니 인덱스 개크다니깐");
                return _LockItem[0];
            }
            else
            {
                return _LockItem[itemIndex];
            }
          
        }

        set
        {
           
            if (itemIndex >= _LockItem.Length)
            {
                Debug.Log("인덱스 너무 큼!");
            }
            else
            {
                _LockItem[itemIndex] = value;
            }
           
        }
    }


    





}
