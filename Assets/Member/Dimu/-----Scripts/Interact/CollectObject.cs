using NSY.Iven;
using NSY.Manager;
using UnityEngine;

public class CollectObject : ItemObject
{
    [SerializeField] bool isGround;
    [SerializeField] int amount = 1;
    [SerializeField] float powerInit = 0.3f;
    [SerializeField] float power;
    [SerializeField] bool canMove;
    [SerializeField] string picktsoundName = "item_pick";
    [SerializeField] string startsoundName = "item_drop2";
    [SerializeField] string downsoundName = "item_drop1";
    BoxCollider box;
    public override int CanInteract()
    {
        return (int)CursorType.Pickup;
    }
    private void OnEnable()
    {
        base.OnEnable();
        box = GetComponent<BoxCollider>();
        if (isGround)
        {

        }
        else
        {
            box.enabled = false;
            Invoke("MoveTrue", 0.5f);
            canMove = true;
            amount = 0;
            power = powerInit;

        }
    }

    public void PlayStartSound()
    {
        SuperManager.Instance.soundManager.PlaySFX(startsoundName);
    }
    public void PlayDropSound()
    {
        SuperManager.Instance.soundManager.PlaySFX(downsoundName);
    }
    void DeactiveDelay() => gameObject.SetActive(false);
    public void MoveTrue()
    {
        box.enabled = true;
    }
    private void Update()
    {
        if (!canMove)
        {
            return;
        }
        Vector3 newVec = new Vector3(transform.position.x, transform.position.y + power, transform.position.z);
        transform.position = newVec;
        power -= Time.deltaTime * 0.5f;
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
        {
            SuperManager.Instance.soundManager.PlaySFX(picktsoundName);
            amount--;
        }
        else
        {
            itemCopy.Destroy();
        }
        Interact();
        DeactiveDelay();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isGround) return;
        if (other.CompareTag("Ground"))
        {
            canMove = false;
            //PlayDropSound();
        }
    }
    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);  // 한 객체에 한번만
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면
    }
}
