using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.Manager;

public class FirstTree : MonoBehaviour
{


    public GameObject FirstApple;
    public GameObject FirstUi;
    Rigidbody Rid;
    float WaitTime=0.8f;

    private void Start()
    {
        Rid = FirstApple.GetComponent<Rigidbody>();
        EventManager.FristTreeCollder += DropApple;
        EventManager.UnFristTreeCollder += ExitDropApple;
    }



    private void DropApple()
    {
        
       
        StartCoroutine(Dropapple()); 
      
       
    }

    IEnumerator Dropapple()
    {
        Debug.Log("사과 떨어져라");
        Rid.useGravity = true;
        yield return new WaitForSeconds(1f);
        Debug.Log("사과 떨어져라");
        Rid.useGravity = true;

            FirstUi.SetActive(true);
            Debug.Log("켜져라");
      

    }


    private void ExitDropApple()
    {
        FirstUi.SetActive(false);
        StopCoroutine(Dropapple());
    }

    private void OnDestroy()
    {
        EventManager.FristTreeCollder -= DropApple;
        EventManager.UnFristTreeCollder -= ExitDropApple;
    }
}
