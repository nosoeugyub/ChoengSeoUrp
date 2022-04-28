using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouse : MonoBehaviour
{
    public float rotSpeed;
    


    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }
}
