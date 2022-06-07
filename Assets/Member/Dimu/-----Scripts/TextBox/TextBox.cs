﻿using DM.Building;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    [SerializeField] GameObject note;
    [SerializeField] GameObject boomParticle;
    [SerializeField] Sprite[] textboxFabImgs_Right;
    [SerializeField] Sprite[] textboxFabImgs_Left;

    RectTransform rect;
    Image textboxFabImg;
    [SerializeField]
    TextMeshProUGUI textboxFabText;
    [SerializeField]
    Button textboxFabNextButton;

    [SerializeField]
    Image RecImg;

    [SerializeField]
    Vector2 rectpos;

    // [SerializeField]
    BoxCollider boxCollider;
    //랜더모드 카메라 생성
    //Canvas UiCamCanvas;
    Camera UiCam;

    [SerializeField] private RectTransform baseCanvas;
    private Vector2 screenPoint;
    private Vector2 worldToScreenPoint;
    Transform bubbleTf;
    Vector3  bubbleposition;
    private void Awake()
    {
        textboxFabImg = transform.Find("Image").GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(RecImg.rectTransform);
        UiCam = GameObject.Find("UICamera").GetComponent<Camera>();
        //UiCamCanvas = this.gameObject.GetComponent<Canvas>();
        baseCanvas = GameObject.Find("GameCanvas").GetComponent<RectTransform>();

    }
    private void OnEnable()
    {
        textboxFabImg.gameObject.SetActive(true);
        textboxFabText.gameObject.SetActive(true);
        textboxFabNextButton.gameObject.SetActive(true);
        boomParticle.SetActive(false);

    }
    public Button GetNextButton => textboxFabNextButton;
    public void SetTextbox(string sentence, Transform tf, TextboxType textboxType, bool isLeft)//말풍선 생산
    {
        BuildingBlock.SetTextBox(this);
        transform.SetParent(baseCanvas);
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        bubbleTf = tf;
        textboxFabText.text = sentence;
        rect.anchoredPosition3D = Vector3.zero;
        LayoutRebuilder.ForceRebuildLayoutImmediate(RecImg.rectTransform);
        //StartCoroutine(PosChange());
        //UiCamCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        //UiCamCanvas.worldCamera = UiCam;
        if (!isLeft)
        {
            bubbleposition = bubbleTf.position;
            textboxFabImg.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        textboxFabImg.sprite = textboxFabImgs_Right[(int)textboxType];
        }
        else
        {
            bubbleposition = bubbleTf.position;
            bubbleposition.x = -bubbleTf.position.x;
            textboxFabImg.GetComponent<RectTransform>().pivot = new Vector2(1, 0);
        textboxFabImg.sprite = textboxFabImgs_Left[(int)textboxType];
        }
        worldToScreenPoint = Camera.main.WorldToScreenPoint(bubbleposition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(baseCanvas, worldToScreenPoint, UiCam, out screenPoint);
        transform.GetComponent<RectTransform>().localPosition = screenPoint;
    }
    public void FixedUpdate()
    {
        worldToScreenPoint = Camera.main.WorldToScreenPoint(bubbleTf.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(baseCanvas, worldToScreenPoint, UiCam, out screenPoint);
        transform.GetComponent<RectTransform>().localPosition = screenPoint;
    }
    IEnumerator PosChange()
    {
        yield return new WaitForSeconds(0.01f);
        rectpos.x = textboxFabImg.rectTransform.rect.width;
        rectpos.y = textboxFabImg.rectTransform.rect.height;
        boxCollider.size = rectpos;
        boxCollider.center = rectpos / 2;
    }
    public void DestroyTextBox()
    {
        if (Random.Range(0, 100) < 1)
            InstactiateNote();
        textboxFabImg.gameObject.SetActive(false);
        textboxFabText.gameObject.SetActive(false);
        BuildingBlock.SetTextBox(null);

        textboxFabNextButton.gameObject.SetActive(false);
        Invoke("Destroy", 0.5f);

        //  Rectoffset.

        boomParticle.transform.localPosition = new Vector3(RecImg.GetComponent<RectTransform>().rect.width / 2,
                                                           RecImg.GetComponent<RectTransform>().rect.height / 2, 0);

        boomParticle.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(RecImg.rectTransform);

    }

    private void InstactiateNote()
    {
        GameObject newNote = Instantiate(note, transform.parent.parent.parent.parent);
        newNote.transform.position = new Vector3(transform.position.x - Random.Range(0.5f, -0.5f), transform.position.y, transform.position.z - Random.Range(0.5f, -0.5f));//아이템오브젝트 부모로 설정해야함
    }
    void DeactiveDelay() => gameObject.SetActive(false);

    public void Destroy()
    {
        DeactiveDelay();
    }

    public void Init()
    {
    }
    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);  // 한 객체에 한번만
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면
    }
}
