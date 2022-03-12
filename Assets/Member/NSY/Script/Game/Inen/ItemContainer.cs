using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using System;
using UnityEngine.Serialization;

namespace NSY.Iven
{
    public abstract class ItemContainer : MonoBehaviour, IItemContainer
    {
        public ItemSlot[] itemSlots;




        public virtual bool AddItem(Item item)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {//itemSlots[i].item == null || (itemSlots[i].item.ItemName == item.ItemName && itemSlots[i].Amount < item.MaximumStacks
                if (itemSlots[i].item == null || itemSlots[i].CanAddStack(item))
                {
                    itemSlots[i].item = item;
                    itemSlots[i].Amount++;
                    return true;
                }


            }
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == null)
                {
                    itemSlots[i].item = item;
                    itemSlots[i].Amount++;
                    return true;
                }


            }
            return false;
        }

        //
        public virtual bool RemoveItem(Item item)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == item)
                {
                    itemSlots[i].Amount--;
                   //  if (itemSlots[i].Amount == 0)
                    //  {
                   //       itemSlots[i].item = null;
                   //  }

                    return true;
                }


            }
            return false;
        }


        public virtual Item RemoveItem(string itemID)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                Item item = itemSlots[i].item;
                if (item != null && item.ItemName == itemID)
                {
                    itemSlots[i].Amount--;
                     // if (itemSlots[i].Amount == 0)
                     //{
                     // itemSlots[i].item = null;
                     //  }
                    return item;
                }


            }
            return null;
        }

        public virtual bool isFull()//아이템 리스트가 슬롯칸보다 같거나 많을경우
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == null)
                {

                    return false;
                }


            }
            return true;
        }


        public virtual int ItemCount(string itemID)
        {

            int number = 0;
            for (int i = 0; i < itemSlots.Length; i++)
            {
                Item item = itemSlots[i].item;
                if (itemSlots[i].item.ItemName == itemID)
                {
                    number += itemSlots[i].Amount;
                   // number++;
                }


            }
            return number;
        }
    }

}

