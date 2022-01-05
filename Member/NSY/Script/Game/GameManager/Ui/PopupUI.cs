using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopupUI : MonoBehaviour, IPointerDownHandler
{
    public Button _closeButton;
    public event Action OnFocus;

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}
