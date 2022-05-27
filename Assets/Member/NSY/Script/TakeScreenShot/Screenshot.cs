using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Screenshot : MonoBehaviour
{
    public int Width; //가로
    public int Hight; //세로

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(ScreenShotCoroutine());
            Debug.Log("김취!~");
        }
    }


    private IEnumerator ScreenShotCoroutine()
    {
        yield return new WaitForEndOfFrame();
        Width = Screen.width;
        Hight = Screen.height;
        Texture2D screenshotTexture = new Texture2D(Width, Hight, TextureFormat.ARGB32, false); // 화면 크기의 텍스쳐를 생성
        Rect rect = new Rect(0, 0, Width, Hight); //캡쳐향ㄹ 영역을지정
        screenshotTexture.ReadPixels(rect, 0, 0); //텍스쳐 픽셀에 저장
        screenshotTexture.Apply();

    }
}
