using DM.Inven;

public class CollectObject : ItemObject, ICollectable
{
    public string CanInteract()
    {
        return "줍기";
    }

    public void Collect()
    {
        FindObjectOfType<InventoryManager>().AddItem(item, 1);
        Interact();
    }
}
