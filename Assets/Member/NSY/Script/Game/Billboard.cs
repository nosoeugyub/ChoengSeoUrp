using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Billboard : MonoBehaviour
{

    private Camera MainCam;
    Vector3 cameraDir;
    public bool useStaticLookCam;

    private void Start()
    {
        MainCam = Camera.main;
    }
    void LateUpdate()
    {
       
        if (!useStaticLookCam)
        {
            cameraDir = MainCam.transform.forward;
            cameraDir.y = 0;
            transform.rotation = Quaternion.LookRotation(cameraDir);
        }
        else
        {
            transform.rotation = MainCam.transform.rotation;
        }
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }

}
