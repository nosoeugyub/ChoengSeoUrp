using NSY.Manager;
using System.Collections;
using UnityEngine;

public class DropCollectObject : CollectObject
{
    [SerializeField] float powerInit = 0.3f;
    [SerializeField] float power=0.3f;
    [SerializeField] bool canMove;

    [SerializeField] string startsoundName = "item_drop2";

    private new void OnEnable()
    {
        base.OnEnable();

        Init();
    }

    private void Init()
    {
        box.enabled = false;
        StartCoroutine(CanInteractDelay());
        canMove = true;
        StartCoroutine(SpawnUpdate());
        power = powerInit;
    }

    public void PlayStartSound()
    {
        SuperManager.Instance.soundManager.PlaySFX(startsoundName);
    }
    IEnumerator SpawnUpdate()
    {
        while (canMove)
        {
            Vector3 newVec = new Vector3(transform.position.x, transform.position.y + power, transform.position.z);
            transform.position = newVec;
            power -= Time.deltaTime * 0.5f;
            yield return null;
        }
    }
    IEnumerator CanInteractDelay()
    {
        yield return new WaitForSeconds(0.5f);
        MoveTrue();
    }
    public void MoveTrue()
    {
        box.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
            canMove = false;
    }
    void OnDisable()
    {
        Debug.LogWarning("ReturnToPool");
        ObjectPooler.ReturnToPool(gameObject);  // 한 객체에 한번만
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면
    }
}
