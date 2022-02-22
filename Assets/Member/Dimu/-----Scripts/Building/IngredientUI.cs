using UnityEngine;
using UnityEngine.UI;
using DM.Inven;

namespace DM.Building
{
    public class IngredientUI : MonoBehaviour
    {
        [SerializeField]
        private ItemType ingredientUItype; //디자이너가 할당
        [SerializeField]
        private Button getButton;
        [SerializeField]
        private Image ingredientIcon;
        [SerializeField]
        private Sprite uiIconSprite;
        [SerializeField]
        private Text gotText;

        public void InitUIs(GetIGDdelegate getIngredient, int i)
        {
            ingredientIcon.sprite = uiIconSprite;
            getButton.onClick.AddListener(() =>
            {
                getIngredient(ingredientUItype, i);
            });
        }
        public void UpdateNowSlashFinishText(string text)
        {
            gotText.text = text;
        }
    }
}