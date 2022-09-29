using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField] Ease _ease;
     int rotateAmount = 10;
    float _iconrotatespeed = 1;

    Coroutine coroutine;
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
        {
            _image.color = _onColor;
        }
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

    public void StartAnimation()
    {
        if (isHaveNewDialog() && coroutine == null && gameObject.activeSelf)
            coroutine = StartCoroutine(IconRotation());
    }

    public IEnumerator IconRotation()
    {
        float time = _iconrotatespeed;
        bool isRIght = false;
        while (isHaveNewDialog())
        {
            time += Time.deltaTime;
            if (time >= _iconrotatespeed)
            {
                isRIght = !isRIght;

                if (isRIght)
                    transform.DOLocalRotate(new Vector3(0, 0, -rotateAmount), _iconrotatespeed, RotateMode.Fast).SetEase(_ease);
                else
                    transform.DOLocalRotate(new Vector3(0, 0, rotateAmount), _iconrotatespeed, RotateMode.Fast).SetEase(_ease);

                time = 0;
            }
            yield return null;
        }
        transform.DOLocalRotate(new Vector3(0, 0, 0), _iconrotatespeed, RotateMode.Fast).SetEase(_ease);
        coroutine = null;
        yield return null;
    }

    bool isHaveNewDialog()
    {
        return _onColor == _image.color;
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
}
