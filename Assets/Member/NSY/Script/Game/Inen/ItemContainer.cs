using DM.Building;
using System;
using System.Collections.Generic;
using UnityEngine;
using NSY.Manager;
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
                    itemSlot.Interactble(true);
                }
                return;
            }

            foreach (ItemSlot itemSlot in ItemSlots)
            {
                if (itemSlot.item.OutItemType == OutItemType.BuildingItemObj)// 건축가능한 친구
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
                }
                if (itemSlot.item.OutItemType != OutItemType.BuildingItemObj)// 건축 불가능한친구면
                {
                    Debug.Log("비활됐냐?");
                    itemSlot.StopAcitveSlot();
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
                    if(OnAddItemEvent !=null)
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
                    if(OnAddItemEvent !=null)
                    OnAddItemEvent();
                    SuperManager.Instance.unlockmanager.GetInterectItemUnLocking();
                    PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.GetItem, PlayerData.ItemData, ((int)ItemBehaviorEnum.length));
                    return true;
                }
            }
            return false;
        }

        //
        public virtual bool RemoveItem(Item item)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].item == item)
                {
                    ItemSlots[i].Amount--;
                    ItemSlots[i].item.GetCountItems--;
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

