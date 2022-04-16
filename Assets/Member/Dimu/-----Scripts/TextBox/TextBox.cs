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
    [SerializeField] GameObject boomParticle;//대화창 프리펩 //쪽지로 변하는것임!!
    private void Awake()
    {
        textboxFabImg = transform.Find("Image").GetComponent<Image>();
        textboxFabText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        textboxFabNextButton = transform.Find("Button").GetComponent<Button>();
        //boomParticle = transform.Find("BOom").GetComponent<GameObject>();
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
        GameObject newNote = Instantiate(note, transform.parent);
        newNote.transform.position = new Vector3(transform.position.x - Random.Range(0.5f, -0.5f), transform.position.y, transform.position.z - Random.Range(0.5f, -0.5f));//아이템오브젝트 부모로 설정해야함
    }

    public void Init()
    {
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
