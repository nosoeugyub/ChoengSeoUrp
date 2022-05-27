using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HappyUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI happytext;
    [SerializeField] Image cleanGageImage;
    [SerializeField] RectTransform heartImg;
    [SerializeField] RectTransform cleanGageBottomPos;
    [SerializeField] RectTransform cleanGayTopPos;
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIChange(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        UIChange(false);
    }

    public void UIChange(bool isicon)
    {
        happytext.gameObject.SetActive(isicon);
    }
    public void HappyUISetting(float happyAmount)
    {
        heartImg.anchoredPosition = ((cleanGayTopPos.anchoredPosition - cleanGageBottomPos.anchoredPosition) * happyAmount * 0.01f) + cleanGageBottomPos.anchoredPosition;
        cleanGageImage.fillAmount = happyAmount / 100;
        happytext.text = happyAmount.ToString();
    }
}
