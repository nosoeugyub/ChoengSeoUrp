using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ScreenCapture.CaptureScreenshot("Game.png");
            Debug.Log("김취!~");
        }
    }
}
