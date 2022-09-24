using NSY.Manager;
using System.Collections;
using UnityEngine;

public class RespawnCollectObject : CollectObject
{
    [SerializeField] float respawnTime;
    [SerializeField] bool isStartOn;

    private new void OnEnable()
    {
        base.OnEnable();

        if (isStartOn)
            Init();
        else
        {
            PickAfter();
            StartCoroutine(RespawnWaitTime());
        }
    }
    public void Init()
    {
        quad.gameObject.SetActive(true);
        box.enabled = true;
    }

    IEnumerator RespawnWaitTime()
    {
        yield return new WaitForSeconds(respawnTime + Random.Range(-100,100));
        Init();
    }

    public override void UpdateCollect()
    {
        Item itemCopy = item.GetCopy();
        if (SuperManager.Instance.inventoryManager.AddItem(itemCopy, true))
            SuperManager.Instance.soundManager.PlaySFX(picksoundName);
        else
            itemCopy.Destroy();
        Interact();
        PickAfter();
            StartCoroutine(RespawnWaitTime());
    }
    public void PickAfter()
    {
        box.enabled = false;
        quad.gameObject.SetActive(false);
    }
}
