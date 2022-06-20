using NSY.Manager;
using NSY.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NSY.Iven
{
    public abstract class ItemContainer : MonoBehaviour, IItemContainer
    {
        public PlayerInteract playrinteract;
        //  public CraftManager craftslots;
        public GameObject NoPopUp;
        public List<ItemSlot> ItemSlots;
        public List<CraftSlot> Craftslot;

        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnRightClickEvent;
        public event Action<BaseItemSlot> OnLeftClickEvent;
        public event Action<BaseItemSlot> OnDubleClickEvent;
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;
        public event Action OnAddItemEvent;


        protected virtual void Awake()
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {

                ItemSlots[i].OnPointerEnterEvent += slot => EventHelper(slot, OnPointerEnterEvent);
                ItemSlots[i].OnPointerExitEvent += slot => EventHelper(slot, OnPointerExitEvent);
                ItemSlots[i].OnRightClickEvent += slot => EventHelper(slot, OnRightClickEvent);
                ItemSlots[i].OnLeftClickEvent += slot => EventHelper(slot, OnLeftClickEvent);
                ItemSlots[i].OnDubleClickEvent += slot => EventHelper(slot, OnDubleClickEvent);
                ItemSlots[i].OnBeginDragEvent += slot => EventHelper(slot, OnBeginDragEvent);
                ItemSlots[i].OnEndDragEvent += slot => EventHelper(slot, OnEndDragEvent);
                ItemSlots[i].OnDragEvent += slot => EventHelper(slot, OnDragEvent);
                ItemSlots[i].OnDropEvent += slot => EventHelper(slot, OnDropEvent);
            }

        }

        public void SetLockSpriteToCraftSlot(Sprite locksprite)
        {
            for (int i = 0; i < Craftslot.Count; i++)
            {
                Craftslot[i].SetSpriteLock(locksprite);
            }
        }

        protected virtual void OnValidate()
        {
            GetComponentsInChildren(includeInactive: true, result: ItemSlots);
        }
        private void EventHelper(BaseItemSlot itemSlot, Action<BaseItemSlot> action)
        {
            if (action != null)
                action(itemSlot);
            else
            {
                print("이벤트가없음");
            }
        }

        public virtual bool CanAddItem(Item item, int amount = 1)
        {
            int freeSpaces = 0;

            foreach (ItemSlot itemSlot in ItemSlots)
            {
                if (itemSlot.item == null || itemSlot.item.ItemName == item.ItemName)
                {
                    freeSpaces += item.MaximumStacks - itemSlot.Amount;
                }
            }
            return freeSpaces >= amount;
        }

        public virtual void InvenAllOnOff(bool isOn) //전체 상호작용 불가능, 가능 처리
        {
            foreach (ItemSlot itemSlot in ItemSlots)
            {
                if (itemSlot.item == null) continue;

                itemSlot.Interactble(isOn);
                //itemSlot.isRedbulid = !isOn;
            }
        }
        public virtual void CheckCanBuildItem()//건축자재만 켜기
        {
            foreach (ItemSlot itemSlot in ItemSlots)
            {
                if (itemSlot.item == null) continue;
                if (itemSlot.item.OutItemType != OutItemType.BuildingItemObj)//건축자재가 아니면 끄기
                    itemSlot.Interactble(false);
                else
                    itemSlot.Interactble(true);
            }
        }
        IEnumerator DelayUpdateAddValue(Item item)
        {
            yield return new WaitForEndOfFrame();
            PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.GetItem, PlayerData.ItemData, ((int)ItemBehaviorEnum.length));
            if (OnAddItemEvent != null)
                OnAddItemEvent();
        }

        public bool isGettingItem = true;
        public bool CanAddInven(Item item)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].CanAddStack(item))
                {
                    return true;
                }
            }
            //이미 있는 칸에 더할 수 없으면

            //빈 슬롯에 넣기
            if (IsFullInven())
            {
                NoPopupOn();
                return false;
            }
            else
                return true;
        }

        public bool IsFullInven()
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].item == null)
                {
                    return false;
                }
            }
            return true;
        }

        public void NoPopupOn()
        {
            StartCoroutine(NoPopUpgo());
        }

        public virtual bool AddItem(Item item)
        {
            if (!CanAddInven(item)) return false;
            //이미 있는 칸에 더할 수 있는지
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].CanAddStack(item))
                {
                    AddInven(item, i);
                    return true;
                }

            }
            //이미 있는 칸에 더할 수 없으면

            //빈 슬롯에 넣기
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].item == null)
                {
                    AddInven(item, i);
                    return true;
                }
            }
            //빈슬롯도 없으면...
            return false;

        }

        private void AddInven(Item item, int i)
        {
            isGettingItem = true;
            ItemSlots[i].item = item;
            ItemSlots[i].Amount++;
            ItemSlots[i].item.GetCountItems++;
            ItemSlots[i].item.GetnuCountItems++;

      

            SuperManager.Instance.unlockmanager.GetInterectItemUnLocking();
            StartCoroutine(DelayUpdateAddValue(item));
        }

        IEnumerator NoPopUpgo()
        {
            NoPopUp.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            NoPopUp.SetActive(false);
        }

        int resultadd;

        public bool AddItem(Item item, int AddCount)//많은 갯수를 먹을때 
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].CanAddStack(item, AddCount))// 최대 
                {
                    for (resultadd = 0; resultadd < AddCount; resultadd++)
                    {
                        ItemSlots[i].item = item;
                        ItemSlots[i].Amount++;
                        ItemSlots[i].item.GetCountItems++;
                        ItemSlots[i].item.GetnuCountItems++;
                        if (OnAddItemEvent != null)
                            OnAddItemEvent();

                        SuperManager.Instance.unlockmanager.GetInterectItemUnLocking();// 해금
                        StartCoroutine(DelayUpdateAddValue(item));
                    }
                    return true;
                }
                else if (ItemSlots[i].item && ItemSlots[i].item.name == item.name)
                {
                    int sub = ItemSlots[i].Amount + AddCount - ItemSlots[i].item.MaximumStacks;
                    ItemSlots[i].Amount = ItemSlots[i].item.MaximumStacks;
                    AddCount = sub;
                   
                }

            }
            for (int i = 0; i < ItemSlots.Count; i++)
            {

                if (ItemSlots[i].item == null)
                {
                    for (resultadd = 0; resultadd < AddCount; resultadd++)
                    {
                        ItemSlots[i].item = item;
                        ItemSlots[i].Amount++;
                        ItemSlots[i].item.GetCountItems++;
                        ItemSlots[i].item.GetnuCountItems++;
                        if (OnAddItemEvent != null)
                            OnAddItemEvent();

                        SuperManager.Instance.unlockmanager.GetInterectItemUnLocking();
                        StartCoroutine(DelayUpdateAddValue(item));

                    }
                    return true;
                }
            }
            return false;
        }

        public virtual bool RemoveItem(Item item, int removeCount)
        {
            if (item.GetCountItems < removeCount) return false; //삭제할 개수보다 보유 개수가 작으면 실패

            List<ItemSlot> itemSlots = GetItemSlotList(item);

            int ra = removeCount;

            ItemSlot minSlot;
            do
            {

                minSlot = itemSlots[itemSlots.Count - 1];

                foreach (var itemslot in itemSlots) //최소 슬롯 찾음
                {
                    if (itemslot.Amount < minSlot.Amount)
                    {
                        minSlot = itemslot;
                    }
                }

                if (minSlot.Amount < ra) //전체 6  민 칸 1개, 필요 2개일 경우 
                {
                    minSlot.item.GetCountItems -= minSlot.Amount;//6- 1
                    ra -= minSlot.Amount; //2 -= 1
                    minSlot.Amount = 0;
                    itemSlots.Remove(minSlot);
                }
                else
                    break;
            }
            while (ra > 0);// 그런데도

            minSlot.item.GetCountItems -= ra;
            minSlot.Amount -= ra;
            OnAddItemEvent();
            return true;
        }
        public void AddItemEvent()
        {
            OnAddItemEvent();
        }
        private List<ItemSlot> GetItemSlotList(Item item)
        {
            List<ItemSlot> itemSlot = new List<ItemSlot>();

            for (int i = 0; i < ItemSlots.Count; i++)//전체 인벤 돌기
            {
                if (item == ItemSlots[i].item) // 삭제할 재료와 인벤슬롯의 아이템 타입이 같다.
                {
                    itemSlot.Add(ItemSlots[i]);//해당 슬롯 리스트 추가
                }
            }
            return itemSlot;
        }

        //
        public virtual bool RemoveItem(Item item)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].item == item)
                {
                    ItemSlots[i].item.GetCountItems--;
                    ItemSlots[i].Amount--;
                    return true;
                }
            }
            return false;
        }

        public virtual Item RemoveItem(string itemID)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                Item item = ItemSlots[i].item;
                if (item != null && item.ItemName == itemID)
                {
                    ItemSlots[i].Amount--;
                    ItemSlots[i].item.GetCountItems--;
                    return item;
                }
            }
            return null;
        }

        public virtual int ItemCount(string itemID)
        {

            int number = 0;
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                Item item = ItemSlots[i].item;
                //string itemtype = ((int)item.InItemType).ToString();
                if (item != null && ((int)item.InItemType).ToString() == itemID)
                {
                    number += ItemSlots[i].Amount;
                }
            }
            return number;
        }

        public void Clear()
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].item != null && Application.isPlaying)
                {
                    ItemSlots[i].item.Destroy();
                }
                ItemSlots[i].item = null;
                ItemSlots[i].Amount = 0;
            }
        }
        public bool isFuuled()
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].item != null)  // 가득찼고
                {
                    if (isGettingItem == true)
                    {
                       

                        return true;
                    }
                    if (isGettingItem == false)
                    {
                       
                        return false;
                    }

                }

            }
            return true;
        }

        public bool Fulled()
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].item != null)  // 가득찼고
                {
                    return false;
                }

            }
            return true;
        }

    }

}

