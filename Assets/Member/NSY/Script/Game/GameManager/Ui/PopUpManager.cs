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

    private void Awake()
    {
        _activePopupList = new LinkedList<PopupUI>();
        
    }
    private void Update()
    {
        //ESC를 누를경우 링크드리스트의 FIrst에 팝업창이들어감
        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

}
