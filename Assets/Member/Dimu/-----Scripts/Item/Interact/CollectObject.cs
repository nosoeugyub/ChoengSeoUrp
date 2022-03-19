using NSY.Iven;

public class CollectObject : ItemObject, ICollectable
{
    public string CanInteract()
    {
        return "줍기";
    }

    public void Collect()
    {
        FindObjectOfType<InventoryNSY>().AddItem(item);

        Interact();
    }
}
