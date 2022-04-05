﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using System;
using UnityEngine.Serialization;

namespace NSY.Iven
{
    public abstract class ItemContainer : MonoBehaviour, IItemContainer
    {
        public CraftManager craftslots;
        public List<ItemSlot> ItemSlots;
        
       

        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnRightClickEvent;
        public event Action<BaseItemSlot> OnLeftClickEvent;
        public event Action<BaseItemSlot> OnDubleClickEvent;
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;




        protected virtual void Awake()
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                ItemSlots[i].OnPointerEnterEvent += slot => EventHelper(slot,OnPointerEnterEvent);
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


        public virtual bool CanAddItem(Item item , int amount = 1)
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

        public bool entireItem(Item item)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].CanAddStack(item))
                {
                    ItemSlots[i].item = item;
                    for (int j = 0; j < craftslots.CratfingSlots.Count; j++)
                    {
                      
                        if (ItemSlots[i].item == craftslots.CratfingSlots[j].item)
                        {
                            ItemSlots[i].Amount += craftslots.CratfingSlots[j].Amount;
                           
                        }
                    }
                   
                    
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
                    PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.GetItem, PlayerData.ItemData, ((int)ItemBehaviorEnum.length));

                    return true;
                }


            }
            return false;
        }

        public virtual bool AddItem(Item item)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].CanAddStack(item))
                {
                    ItemSlots[i].item = item;
                    ItemSlots[i].Amount++;
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
                if (item != null &&item.ItemName == itemID)
                {
                    number += ItemSlots[i].Amount;
                   // number++;
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

