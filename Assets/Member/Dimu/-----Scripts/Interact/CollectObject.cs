using NSY.Iven;

public class CollectObject : ItemObject, ICollectable
{
    public int amount = 1;
    public string CanInteract()
    {
        return "줍기";
    }

    public void Collect()
    {
        inventoryNSY=FindObjectOfType<InventoryNSY>();
        Item itemCopy = item.GetCopy();
        if (inventoryNSY.AddItem(itemCopy))
        {
            amount--;
           
        }
        else
        {
            itemCopy.Destroy();

        }

        Interact();
    }
}
