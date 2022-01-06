using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPosition : MonoBehaviour
{
    [SerializeField] public Transform Playertransform;
    [SerializeField] public Transform Box;
    [SerializeField] Vector3 Uioffset;
    [SerializeField] Vector3 PlayTransUioffset;
    public GameObject PopUpBox;
    public GameObject PopUpTuto001;

    // Update is called once per frame
    void Update()
    {
        PopUpBox.transform.position = Camera.main.WorldToScreenPoint(Box.position + Uioffset);
        PopUpTuto001.transform.position = Camera.main.WorldToScreenPoint(Playertransform.position + PlayTransUioffset);

    }
}
