using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ScreenshotManager : MonoBehaviour
{
    public List<Screenshot> screenshots;

    public void screenshosEvent()
    {
        for (int i = 0; i < screenshots.Count; i++)
        {
            Debug.Log("씨발아 김취라");
            screenshots[i].OnSceenShotEvent();
        }
    }

    //private void onable()
    //{
    //    for (int i = 0; i < screenshot.Count; i++)
    //    {
    //        screenshot[i].OnSceenShot += screenshot[i].OnSceenShotEvent;
    //    }
    //}
    //private void OnDisable()
    //{
    //    for (int i = 0; i < screenshot.Count; i++)
    //    {
    //        screenshot[i].OnSceenShot -= screenshot[i].OnSceenShotEvent;
    //    }
    //}
}
