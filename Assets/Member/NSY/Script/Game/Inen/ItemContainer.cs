using DM.Building;
using NSY.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace NSY.Iven
{
    public abstract class ItemContainer : MonoBehaviour, IItemContainer
    {
        //  public CraftManager craftslots;

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

        protected virtual void OnValidate()
        {
            GetComponentsInChildren(includeInactive: true, result: ItemSlots);
        }
        private void EventHelper(BaseItemSlot itemSlot, Action<BaseItemSlot> action)
        {
            if (action != null)
                action(itemSlot);
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

        public virtual void CheckCanBuildItem(BuildingBlock buildingBlock)//당장 건축 가능한 자재인지 아닌지 판단.
        {

            if (!buildingBlock) //임시 처리 정확한 기획 없음
            {
                foreach (ItemSlot itemSlot in ItemSlots)
                {
                    if (itemSlot.item == null) continue;
                    itemSlot.Interactble(true); //채원이 착한고 이쁜 빨갱이
                }
                return;
            }

            foreach (ItemSlot itemSlot in ItemSlots)
            {

                if (itemSlot.item == null)
                {


                    continue;
                }
                else
                {
                    if (itemSlot.item.InItemType == InItemType.BuildSign || itemSlot.item.InItemType != InItemType.BuildWall && itemSlot.item.InItemType != InItemType.BuildNormal)//벽이면
                    {
                        itemSlot.Interactble(false);
                    }
                    else
                    {
                        itemSlot.Interactble(true);
                    }
                }



                if (itemSlot.item.OutItemType == OutItemType.BuildingItemObj && itemSlot.item != null)// 건축가능한 친구
                {
                    itemSlot.isCheckBulid = false;
                    itemSlot.Interactble(true);
                }
                else// 건축 불가능한친구면
                {

                    itemSlot.isCheckBulid = true;
                    itemSlot.Interactble(false);

                }

            }
        }

        public virtual bool AddItem(Item item)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].CanAddStack(item))
                {
                    ItemSlots[i].item = item;
                    ItemSlots[i].Amount++;
                    ItemSlots[i].item.GetCountItems++;
                    if (OnAddItemEvent != null)
                        OnAddItemEvent();

                    SuperManager.Instance.unlockmanager.GetInterectItemUnLocking();// 해금
                    PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.GetItem, PlayerData.ItemData, ((int)ItemBehaviorEnum.length));

                    return true;
                }
            }
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].item == null)
                {
                    ItemSlots[i].item = item;
                    ItemSlots[i].Amount++;
                    ItemSlots[i].item.GetCountItems++;
                    if (OnAddItemEvent != null)
                        OnAddItemEvent();

                    SuperManager.Instance.unlockmanager.GetInterectItemUnLocking();
                    PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.GetItem, PlayerData.ItemData, ((int)ItemBehaviorEnum.length));

                    return true;
                }
            }
            return false;
        }

        public bool AddItem(Item item, int removeCount)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].CanAddStack(item, removeCount))
                {
                    ItemSlots[i].item = item;
                    ItemSlots[i].Amount++;
                    ItemSlots[i].item.GetCountItems++;
                    if (OnAddItemEvent != null)
                        OnAddItemEvent();

                    SuperManager.Instance.unlockmanager.GetInterectItemUnLocking();// 해금
                    PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.GetItem, PlayerData.ItemData, ((int)ItemBehaviorEnum.length));

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

            ItemSlot minSlot;// = itemSlots[itemSlots.Count - 1];

            do
            {
                //print(itemSlots[i].Count - 1);
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

            return true;
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

    }
}

