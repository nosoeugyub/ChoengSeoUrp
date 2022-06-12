﻿using NSY.Manager;
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
        box = GetComponent<BoxCollider>();

        box.enabled = false;
        StartCoroutine(CanInteractDelay());
        StartCoroutine(SpawnUpdate());
        canMove = true;
        power = powerInit;
    }
    public void PlayStartSound()
    {
        SuperManager.Instance.soundManager.PlaySFX(startsoundName);
    }
    //private void Update()
    //{
    //    if (!canMove)
    //        return;
        
    //    Vector3 newVec = new Vector3(transform.position.x, transform.position.y + power, transform.position.z);
    //    transform.position = newVec;
    //    power -= Time.deltaTime * 0.5f;
    //}
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
}
