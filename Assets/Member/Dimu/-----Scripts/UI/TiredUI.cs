using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TiredUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image PlayerHealth_image;
    [SerializeField] Image PlayerHealth_icon;
    [SerializeField] TextMeshProUGUI PlayerHealth_textParent;
    [SerializeField] TextMeshProUGUI PlayerHealth_textText;
    [SerializeField] Sprite[] playerHealth_sprites;
    public void SetTiredUI(float tired, float maxHealth)
    {
        PlayerHealth_image.fillAmount = tired/ maxHealth;
        PlayerHealth_textText.text = ((int)tired).ToString();
        SpriteUpdate();

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIChange(false);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        UIChange(true);
    }
    public void UIChange(bool isicon)
    {
        PlayerHealth_icon.gameObject.SetActive(isicon);
        PlayerHealth_textParent.gameObject.SetActive(!isicon);
    }
    void SpriteUpdate()
    {
        if (PlayerHealth_image.fillAmount > 0.7f)
            PlayerHealth_icon.sprite = playerHealth_sprites[0];
        else if (PlayerHealth_image.fillAmount > 0.3f)
            PlayerHealth_icon.sprite = playerHealth_sprites[1];
        else
            PlayerHealth_icon.sprite = playerHealth_sprites[2];
    }
}
