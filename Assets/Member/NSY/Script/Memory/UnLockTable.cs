using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//조건상세테이블
[CreateAssetMenu(fileName = "Lockitems", menuName = "LockItem")]

public class UnLockTable : ScriptableObject
{
    //언락에 성공한 아이템 식별자
   



    //언락에 필요한 아이템
    private Item _LockItem;
    public Item LockItem
    {
        get
        {
            return _LockItem;
        }

        set
        {
            _LockItem = value;
        }


        //언락 필요한 퀘스트 프로퍼티
    }
}
