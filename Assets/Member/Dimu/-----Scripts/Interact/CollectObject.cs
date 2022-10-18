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
    public bool Collect(Animator animator)
    {


        if (SuperManager.Instance.inventoryManager.CanAddInven(item))
        {
            animator.GetComponent<PlayerAnimator>().PickUp = UpdateCollect;
            animator.SetBool("isPickingUp", true);
            return true;
        }
        else
            return false;

    }
    public virtual void UpdateCollect()
    {
        //Item itemCopy = item.GetCopy();
        if (SuperManager.Instance.inventoryManager.AddItem(item, true))
            SuperManager.Instance.soundManager.PlaySFX(picksoundName);
        //else
        //    itemCopy.Destroy();
        Interact();
        DeactiveDelay();
    }
    void DeactiveDelay() => gameObject.SetActive(false);


}
