using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPosition : MonoBehaviour
{
    [SerializeField] public Transform Playertransform;
    [SerializeField] Vector3 Uioffset;
    public GameObject PopUpBox;


    // Update is called once per frame
    void Update()
    {
        PopUpBox.transform.position = Camera.main.WorldToScreenPoint(Playertransform.position + Uioffset);
    }
}
