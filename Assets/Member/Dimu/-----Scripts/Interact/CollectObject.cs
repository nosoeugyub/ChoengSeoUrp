using NSY.Iven;
using NSY.Manager;
using UnityEngine;

public class CollectObject : ItemObject
{
    [SerializeField] protected string picksoundName = "item_pick";
    protected BoxCollider box;
    private new void Awake()
    {
        base.Awake();
        box = GetComponent<BoxCollider>();
    }
    public override int CanInteract()
    {
        return (int)CursorType.Pickup;
    }
    private new void OnEnable()
    {
        base.OnEnable();
    }
    public void Collect(Animator animator)
    {
        animator.GetComponent<PlayerAnimator>().PickUp = UpdateCollect;
        animator.SetBool("isPickingUp", true);
    }
    public virtual void UpdateCollect()
    {
        inventoryNSY = FindObjectOfType<InventoryNSY>();
        Item itemCopy = item.GetCopy();
        if (inventoryNSY.AddItem(itemCopy))
            SuperManager.Instance.soundManager.PlaySFX(picksoundName);
        else
            itemCopy.Destroy();
        Interact();
        DeactiveDelay();
    }
    void DeactiveDelay() => gameObject.SetActive(false);


}
