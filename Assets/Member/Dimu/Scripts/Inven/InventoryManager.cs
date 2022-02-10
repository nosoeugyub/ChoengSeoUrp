using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DM.Inven
{
    public class InventoryManager : MonoBehaviour
    {
        public Transform slotMom;
        public Transform inventoryMom;

        public Dictionary<Item, Slot> itemslots = new Dictionary<Item, Slot>();
        public GameObject slotfab;

        public int maxStorageSpace;
        public int storageSpace;

        public Text storageSpaceText;

        public Item[] items;//테스트용
        private void Awake()
        {
            UpdateStorageText();
        }
        private void UpdateStorageText() //저장공간 텍스트 업데이트
        {
            storageSpaceText.text = string.Format("{0}/{1}", storageSpace, maxStorageSpace);
        }

        private void Update()
        {
          //  if (Input.GetKeyDown(KeyCode.I))
            //    OnOffInventoryUI();
        }
        public void OnOffInventoryUI()
        {
            
                inventoryMom.gameObject.SetActive(!inventoryMom.gameObject.activeSelf);
        }
        public bool CanAddItem() //아이템 추가가 가능한지 bool 퀘스트 보상 수령 시에도 호출 가능
        {
            return storageSpace < maxStorageSpace;
        }
        public bool AddItem(Item item, int howmuch) //add 성공 여부를 리턴.
        {
            //if (!PlayerData.ItemData.ContainsKey((int)item.ItemType))
            //{
            //    PlayerData.ItemData.Add((int)item.ItemType, new ItemBehavior());
            //}
            //PlayerData.AddDictionary((int)item.ItemType, PlayerData.ItemData);

            if (!CanAddItem())
            {
                print("용량이 가득 차 더이상 수납할 수 없습니다.");
                return false;
            }

            if (itemslots.ContainsKey(item))
            {
                AddItemValue(item, howmuch);
            }
            else
            {
                InstantiateItem(item);
                AddItemValue(item, howmuch);
            }
            return true;
        }

        private void AddItemValue(Item item, int howmuch)
        {
            itemslots[item].AddValue(howmuch);
            storageSpace += howmuch;
            UpdateStorageText();
        }

        public void DeleteItem(Item item, int howmuch) //템삭제
        {
            if (itemslots.ContainsKey(item))//키가 있다면
            {
                int slotValue = itemslots[item].DeleteValue(howmuch); //뺀 양을 받는다.
                storageSpace -= slotValue;
                UpdateStorageText();

                if (itemslots[item].GetValue() <= 0)
                {
                    Destroy(itemslots[item].gameObject);
                    itemslots.Remove(item);
                }
            }
            else
                return;
        }

        private void InstantiateItem(Item item) //슬롯 인스턴스 생성
        {
            GameObject slotObj = Instantiate(slotfab) as GameObject; //슬롯 inst 생성
            slotObj.transform.SetParent(slotMom);//부모 설정
            Slot slot = slotObj.GetComponent<Slot>(); //슬롯 설정
            slot.SetItemSlot(item); //슬롯초기화

            itemslots.Add(item, slot);
            print("Instance");
        }

        public int GetItemValue(Item item) //값 받아오기
        {
            if (itemslots.ContainsKey(item))
            {
                return itemslots[item].GetValue();
            }
            return 0;
        }

        #region test methods
        public void AddItemButton(int idx) //버튼에 연결함. 테스트
        {
            AddItem(items[idx], 2);
        }
        public void DeleteItemButton(int idx) //버튼에 연결함. 테스트
        {
            DeleteItem(items[idx], 1);
        }
        public void PrintAllSlot()
        {
            foreach (var item in itemslots)
            {
                print(GetItemValue(item.Key));
                print(item.Value.itemInfo.ItemName);
            }
        }
        #endregion
    }

}