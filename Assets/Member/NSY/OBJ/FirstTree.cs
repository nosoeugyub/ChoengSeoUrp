using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.Manager;

public class FirstTree : MonoBehaviour
{
    public GameObject FirstApple;
    Rigidbody Rid;

    private void Start()
    {
        Rid = FirstApple.GetComponent<Rigidbody>();
        EventManager.FristTreeCollder += DropApple;
    }



    private void DropApple()
    {
        Debug.Log("사과 떨어져라");
        Rid.useGravity = true;

    }

    private void OnDestroy()
    {
        EventManager.FristTreeCollder -= DropApple;
    }
}
