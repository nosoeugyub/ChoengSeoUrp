﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    [SerializeField] GameObject note;
    [SerializeField] GameObject boomParticle;
    [SerializeField] Sprite[] textboxFabImgs;

    RectTransform rect;
    Image textboxFabImg;
    [SerializeField]
    TextMeshProUGUI textboxFabText;
    [SerializeField]
    Button textboxFabNextButton;

    private void Awake()
    {
        textboxFabImg = transform.Find("Image").GetComponent<Image>();
      
       
        rect = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        textboxFabImg.gameObject.SetActive(true);
        textboxFabText.gameObject.SetActive(true);
        textboxFabNextButton.gameObject.SetActive(true);
        boomParticle.SetActive(false);
    }
    public Button GetNextButton => textboxFabNextButton;
    public void SetTextbox(string sentence, Transform tf, TextboxType textboxType)
    {
        textboxFabText.text = sentence;
        rect.anchoredPosition3D = Vector3.zero;
        textboxFabImg.sprite = textboxFabImgs[(int)textboxType];
    }
    public void DestroyTextBox()
    {
        if (Random.Range(0, 100) < 50)
            InstactiateNote();
        textboxFabImg.gameObject.SetActive(false);
        textboxFabText.gameObject.SetActive(false);
        textboxFabNextButton.gameObject.SetActive(false);
        Invoke("Destroy", 0.5f);
        boomParticle.SetActive(true);
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
