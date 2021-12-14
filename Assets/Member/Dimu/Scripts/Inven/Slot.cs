using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DM.Inven
{
    public class Slot : MonoBehaviour
        ,IPointerClickHandler
        , IPointerEnterHandler
        , IPointerExitHandler
    {
        public Item itemInfo;
        public Image itemImage;
        public Text itemNameText;
        
        public GameObject itemInfoUI;
        public Button DeleteAllButton;

        public int value;

        public void SetItemSlot(Item item) //생성 시 셋팅
        {
            itemInfo = item;
            itemImage.sprite = itemInfo.itemSprite;
            itemNameText.text = value + "";
            value = 0;

            itemInfoUI.GetComponent<Text>().text = itemInfo.itemDescription;
            DeleteAllButton.onClick.AddListener(() => FindObjectOfType<InventoryManager>().DeleteItem(itemInfo, value));//+플레이어 위치에 아이템 생성

            itemInfoUI.SetActive(false);
            DeleteAllButton.gameObject.SetActive(false);
        }

        public void AddValue(int howmuch) //추가
        {
            value += howmuch;
            itemNameText.text = value + "";

        }
        public int DeleteValue(int howmuch) //삭제
        {
            int sub = 0; 
            if(value < howmuch) sub = value - howmuch;//5 - 6 = sub = -1
            value -= howmuch;
            itemNameText.text = value + "";
            return howmuch + sub;
        }
        public int GetValue() //값 리턴
        {
            return value;
        }

        //UI Event
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                print("clickbutton");
                DeleteAllButton.gameObject.SetActive(true);
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            itemInfoUI.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            itemInfoUI.SetActive(false);
            DeleteAllButton.gameObject.SetActive(false);
        }

    }
}