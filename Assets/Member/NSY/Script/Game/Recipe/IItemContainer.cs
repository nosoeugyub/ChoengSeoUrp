﻿
using DM.Inven;

namespace NSY.Iven
{
    public interface IItemContainer 
    {
        int ItemCount(string itemID);
        Item RemoveItem(string itemID);
        bool RemoveItem(Item item);
        bool RemoveItem(Item item, int removeCount);
        bool AddItem(Item item);
        bool AddItem(Item item, int removeCount);
        bool CanAddItem(Item item , int amount = 1);
        void Clear();

     
    }

}
