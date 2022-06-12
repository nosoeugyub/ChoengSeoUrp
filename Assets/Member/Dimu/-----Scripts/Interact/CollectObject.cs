using NSY.Iven;
using NSY.Manager;
using UnityEngine;

public class CollectObject : ItemObject
{
    [SerializeField] string picksoundName = "item_pick";
    protected BoxCollider box;

    public override int CanInteract()
    {
        return (int)CursorType.Pickup;
    }
    private new void OnEnable()
    {
        base.OnEnable();
        box = GetComponent<BoxCollider>();
    }
    public void Collect(Animator animator)
    {
        animator.GetComponent<PlayerAnimator>().PickUp = UpdateCollect;
        animator.SetBool("isPickingUp", true);
    }
    public void UpdateCollect()
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
    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);  // 한 객체에 한번만
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면
    }
}
