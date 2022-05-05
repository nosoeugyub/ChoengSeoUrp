using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//조건상세테이블
[CreateAssetMenu(fileName = "Lockitems", menuName = "LockItemDatatable")]

public class UnLockTable : ScriptableObject
{
   
    //언락에 성공한 아이템 식별자
    [Header("item 스크립트의 numbering을 적으면 여기에나옴 \n" +
        "해금 레시피 아이템 식별 넘버임")]
    [Space(10)]
    public int itemIdNubering;
    [Space(10)]
    [Header("해제에 필요한 아이템들")]
    public List<UnlcokIteminfo>  FindLockItem;


    //언락에 필요한 아이템
    [Header("해제 될 레시피")]
    [SerializeField]
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
            itemIdNubering = _LockItem.ItemNubering; //아잉템 넘버
            if (_LockItem != null)
            {
                FindLockItem.AddRange(_LockItem.UnlcokIteminfos);



            }
           
        }
    }

        //언락 필요한 퀘스트 넘버 프로퍼티
        private int _QuestNubering;
        public int QuestNubering
        {
            get
            {
                return _QuestNubering;
            }
            set
            {
                _QuestNubering = value;
            }
        }

    private void OnValidate()
    {
        QuestNubering = _QuestNubering;
        LockItem = _LockItem;
    }

}
