using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NpcNoticeEntity : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string npcNameText;
    Image _image;
    Image _season;
    Color _onColor;
    Color _offColor;

    bool isPoint;

    public Action<bool, bool, Vector3, string> SetinformUIOnOffEvent;
    public Action<bool, string> UpdateText;

    private void Awake()
    {
        isPoint = false;
        _image = GetComponent<Image>();
        _season = transform.GetChild(0).GetComponent<Image>();
        gameObject.SetActive(false);
    }
    internal void SetInitColor(Color onColor, Color offColor)
    {
        _onColor = onColor;
        _offColor = offColor;
    }
    public void FirstUIOnMethod()
    {
        isPoint = true;
        SetinformUIOnOffEvent(true, isHaveNewDialog(), transform.position, npcNameText);
    }
    internal void UpdateNoticeColor(DialogMarkType dialogMarkType)
    {
        if (dialogMarkType != DialogMarkType.None)
            _image.color = _onColor;
        else
            _image.color = _offColor;

        if (isPoint)
        {
            UpdateText(isHaveNewDialog(), npcNameText);
        }
    }

    internal void SetSeasonSprite(Sprite sprite)
    {
        _season.sprite = sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPoint = true;
        SetinformUIOnOffEvent(true, isHaveNewDialog(), transform.position, npcNameText);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        isPoint = false;
        SetinformUIOnOffEvent(false, isHaveNewDialog(), transform.position, npcNameText);
    }
    bool isHaveNewDialog()
    {
        return _onColor == _image.color;
    }
}
