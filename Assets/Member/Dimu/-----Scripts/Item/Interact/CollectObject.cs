using NSY.Iven;
using NSY.Manager;

public class CollectObject : ItemObject, ICollectable
{
    public string CanInteract()
    {
        return "줍기";
    }

    public void Collect()
    {
        SuperManager.Instance.inventoryManager.AddItem(item);

        Interact();
    }
}
