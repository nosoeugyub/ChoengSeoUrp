using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Billboard : MonoBehaviour
{

    private Camera MainCam;
    public bool useStaticLookCam;

    private void Start()
    {
        MainCam = Camera.main;
    }
    void LateUpdate()
    {
       
        if (!useStaticLookCam)
        {
            transform.LookAt(MainCam.transform);
        }
        else
        {
            transform.rotation = MainCam.transform.rotation;
        }
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }

}
