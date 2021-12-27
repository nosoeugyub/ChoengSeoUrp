﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public PopupUI _inventroyPopup;
    public PopupUI _dialoguePopup;

    //실시간 팝업관리 링크드 리스트ㅓ
    private LinkedList<PopupUI> _activePopupList;
    //전체 팝업목록
    private List<PopupUI> _allPopupList;

    [Space]
    public KeyCode EscKey = KeyCode.Escape;
    public KeyCode inventoryKey = KeyCode.I;

    private void Awake()
    {
        _activePopupList = new LinkedList<PopupUI>();
        Init();
        InitCloseAll();
        
    }


    private void Update()
    {
        //ESC를 누를경우 링크드리스트의 FIrst에 팝업창이들어감
        if (Input.GetKeyDown(EscKey))
        {
            if (_activePopupList.Count > 0)
            {
                ClosePopup(_activePopupList.First.Value);
            }
            
        }
        //단축키를 눌렀을때
        ToggleKeyDownAction(inventoryKey, _inventroyPopup);
    }
    /// <summary>
    /// Prive 함수들====================================
    /// </summary>
    //리스트 초기화 및 팝업창에 이벤트 등록
    private void Init()
    {  //list초기화+
        _allPopupList = new List<PopupUI>
        {
            _inventroyPopup
        };
        foreach (var popup in _allPopupList)//팝업 이벤트 등록
        {
            popup.OnFocus += () =>
            {
                _activePopupList.Remove(popup);
                _activePopupList.AddFirst(popup);
                RefreshAllPopupDepth();
            };
            //닫기 버튼
            popup._closeButton.onClick.AddListener(() => ClosePopup(popup));
        }

    }
    //시작시 모든 팝업 닫기
    private void InitCloseAll()
    {
        foreach (var popup in _allPopupList)
        {
            ClosePopup(popup);
        }
    }


    //단축키 팝업 의 입력에따라
    private void ToggleKeyDownAction(in KeyCode key, PopupUI popup)
    {
        if (Input.GetKeyDown(key))
        {
            ToggleOpenClosePopup(popup);
        }


    }
    //팝업의 상태에따라 열거다나 닫기
    private void ToggleOpenClosePopup(PopupUI popup)
    {
        if (!popup.gameObject.activeSelf)
        {
            OpenPopup(popup);
        }
        else
            ClosePopup(popup);
    }
    //팝업을 열고 리스트의 상단에추가
    private void OpenPopup(PopupUI popup)
    {
        _activePopupList.AddFirst(popup);
        popup.gameObject.SetActive(true);
        RefreshAllPopupDepth();
    }

    //팝업창 닫기 링크드리스트에서 제거
    private void ClosePopup(PopupUI popup)
    {
        _activePopupList.AddFirst(popup);
        popup.gameObject.SetActive(false);
        RefreshAllPopupDepth();
    }
    //링크드리스트 내 모든 팝업의 자식순서 재배치
    private void RefreshAllPopupDepth()
    {
        foreach (var popup in _activePopupList)
        {
            popup.transform.SetAsFirstSibling();
        }
    }










}
