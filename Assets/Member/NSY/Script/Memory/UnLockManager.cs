using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NSY.Iven;

public class UnLockManager : MonoBehaviour
{
    public static UnLockManager Unlockmanager;

    

    //조건함수 이벤트
    private void Awake()
    {
        Unlockmanager = this;
    }

    public  event Action   GetItemUnlocks; //아이템 흭득시 아이템 넘버를 받아와서 조건탐색
    //조건 전달 함수
    public void  GetInterectItemUnLocking()
    {
            if (GetItemUnlocks != null)
            {
                GetItemUnlocks();
            }
 
    }
    public event Action GetCreateUnlocks;
    public void GetCreateItemUnLocking()
    {
        if (GetCreateUnlocks != null)
        {
            GetCreateUnlocks();
        }

    }



    public event Action GetUnlockUnlocks;
    public void GetUnlockItemUnLocking()
    {
        if (GetUnlockUnlocks != null)
        {
            GetUnlockUnlocks();
        }

    }

}
