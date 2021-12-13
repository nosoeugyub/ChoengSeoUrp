using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : MonoBehaviour
{
    //레이저를 맞았을 때 특정한 레이어만 맞도록 설정
    [SerializeField] private LayerMask layerMask;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;

        if(Physics.Raycast(this.transform.position,this.transform.transform.forward, out hitInfo, 10f, layerMask))
        {
            Debug.Log(hitInfo.transform.name);
        }
    }
}
