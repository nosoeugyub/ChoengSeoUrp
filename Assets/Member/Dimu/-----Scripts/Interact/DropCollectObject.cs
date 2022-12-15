using NSY.Manager;
using System.Collections;
using UnityEngine;

public class DropCollectObject : CollectObject
{
    float powerInit = 10f;
    float power;
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
       // StartCoroutine(SpawnUpdate());
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
            Vector3 newVec = new Vector3(transform.position.x, transform.position.y + power * Time.deltaTime, transform.position.z);
            transform.position = newVec;
            power -= 0.6f;
            yield return null;
        }
    }

    public void FixedUpdate()
    {
       if (canMove)
        {
            Vector3 newVec = new Vector3(transform.position.x, transform.position.y + power * Time.deltaTime, transform.position.z);
            transform.position = newVec;
            power -= 0.6f;
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
        ObjectPooler.ReturnToPool(gameObject);  // 한 객체에 한번만
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면
    }
}
