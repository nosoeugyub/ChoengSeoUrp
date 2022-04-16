using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    [SerializeField] GameObject note;
    RectTransform rect;
    Image textboxFabImg;//대화창 프리펩 //쪽지로 변하는것임!!
    TextMeshProUGUI textboxFabText;//대화창 프리펩 //쪽지로 변하는것임!!
    Button textboxFabNextButton;//대화창 프리펩 //쪽지로 변하는것임!!
    private void Awake()
    {
        textboxFabImg = transform.Find("Image").GetComponent<Image>();
        textboxFabText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        textboxFabNextButton = transform.Find("Button").GetComponent<Button>();
        rect = GetComponent<RectTransform>();
    }
    public Button GetNextButton => textboxFabNextButton;
    public void OnEnable()
    {
    }
    public void SetTextandPosition(string sentence, Transform tf)
    {
        textboxFabText.text = sentence;
        rect.anchoredPosition3D = new Vector3(2, 7, 0);
    }
    public void DestroyTextBox()
    {
        GameObject newNote = Instantiate(note, transform.parent);
        newNote.transform.position = transform.position;//아이템오브젝트 부모로 설정해야함
        Destroy(this.gameObject);//풀링
    }
}
