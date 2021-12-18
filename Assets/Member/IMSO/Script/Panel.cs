using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public GameObject panel;

    void OnCollisionEnter(Collision collison)
    {
        if(Input.GetKeyDown(KeyCode.R) && panel != null)
        {
            bool isActive = panel.activeSelf;
            panel.SetActive(true);
        }
    }
}
