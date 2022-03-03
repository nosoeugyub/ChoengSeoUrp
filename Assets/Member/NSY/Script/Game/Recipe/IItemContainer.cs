
using DM.Inven;

namespace NSY.Iven
{
    public interface IItemContainer 
    {
        int ItemCount(string itemID);
        Item RemoveItem(string itemID);
        bool RemoveItem(Item item);
        bool AddItem(Item item);
        bool isFull();
    }

}
