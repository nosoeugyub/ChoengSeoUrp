using NSY.Iven;
using NSY.Manager;
using UnityEngine;

public class CollectObject : ItemObject, ICollectable
{
    public int amount = 1;
    public float power = 0.3f;
    public bool canMove;
    [SerializeField] string soundName = "item_pick";
    BoxCollider box;
    public string CanInteract()
    {
        return "줍기";
    }
    private void OnEnable()
    {
        box = GetComponent<BoxCollider>();
        box.enabled = false;
            SuperManager.Instance.soundManager.PlaySFX("item_drop");
        canMove = true;
        Invoke("MoveTrue", 0.5f);
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
            SuperManager.Instance.soundManager.PlaySFX("item_pick");
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
        if (other.CompareTag("Ground"))
        {
            //print("ground");
            canMove = false;
        }
    }

    public void EndInteract()
    {
    }
    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);  // 한 객체에 한번만
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면
    }
}
