using NSY.Iven;
using UnityEngine;

public class CollectObject : ItemObject, ICollectable
{
    public int amount = 1;
    public float power = 0.3f;
    public bool canMove;
    BoxCollider box;
    public string CanInteract()
    {
        return "줍기";
    }
    private void OnEnable()
    {
        box = GetComponent<BoxCollider>();
        box.enabled = false;
        canMove = true;
        Invoke("MoveTrue", 0.5f);
    }
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

    public void Collect()
    {
        inventoryNSY = FindObjectOfType<InventoryNSY>();
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
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            //print("ground");
            canMove = false;
        }
    }
}
