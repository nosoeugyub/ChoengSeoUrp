//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//public class TrashCan : ItemObject//, IEventable
//{
//    bool isInvenOn = false;
//    //NSY 추가 코드
    
//    GameObject popupmanager;
//    private void Start()
//    {
//        popupmanager = GameObject.Find("PopUpUi Contain");
//    }
//    public new int CanInteract()
//    {
//        return (int)CursorType.Normal;
//    }
//    public void EtcEvent(Item _handItem) //상호작용 시 실행
//    {
//        TrashInventoryOn();
//        Interact();
//    }
//    public void TrashInventoryOn()
//    {
//        if (isInvenOn)
//        {
//            print("인벤닫기");
//            //nsy
//            popupmanager.GetComponent<PopUpManager>().ClosePopup(popupmanager.GetComponent<PopUpManager>()._ivenPopup);
//            isInvenOn = false;
//        }
//        else
//        {

//            print("인벤열기");
//            //nsy
//            popupmanager.GetComponent<PopUpManager>().OpenPopup(popupmanager.GetComponent<PopUpManager>()._ivenPopup);
//            isInvenOn = true;
//        }
//    }

//    //메서드 자유 추가
//}
