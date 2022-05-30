using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.Rendering;

public class Screenshot : MonoBehaviour
{

    public Camera camera;//ui카메라가찍히기 전에 게임 카메라에 있는 랜더텍스쳐를 활용해 ui제외한 부분들을 스크린샷처럼 찍음 
    public int Width; //가로
    public int Hight; //세로
   
    public string folderName = "ScreenShot";
    public string fileName = "MyScreenShot";
    public string extName = "png";

    public bool _WillFakeScreenShot;

    //저장될 경로 변수
    private string RootPath
    {
        get
        {
            return Application.dataPath;
        }
    }

    private string FolderPath => $"{RootPath}/{folderName}";
    private string TotalPath => $"{FolderPath}/{fileName}_{DateTime.Now.ToString("MMdd_HHmmss")}.{extName}";





    private void OnPostRender()
    {
        if (_WillFakeScreenShot)
        {
            if (Directory.Exists(FolderPath) == false)
            {
                Debug.Log("폴더없으니 만들겠음 ㅋ");
                Directory.CreateDirectory(FolderPath);
            }

            _WillFakeScreenShot = false;
            Width = Screen.width;
            Hight = Screen.height;
            RenderTexture RenderTexture = new RenderTexture(Width, Hight ,24);
            camera.targetTexture = RenderTexture;
            Texture2D screenshotTexture = new Texture2D(Width, Hight, TextureFormat.RGB24, false); // 화면 크기의 텍스쳐를 생성
            Rect rect = new Rect(0, 0, Width, Hight); //캡쳐 영역을지정
            camera.Render();RenderTexture.active = RenderTexture;


            screenshotTexture.ReadPixels(rect, 0, 0); //텍스쳐 픽셀에 저장
           screenshotTexture.Apply();
          //pc 저장
         byte[] byteArray = screenshotTexture.EncodeToPNG(); // 이미지 저장
         System.IO.File.WriteAllBytes(TotalPath, screenshotTexture.EncodeToPNG());

            Destroy(screenshotTexture);
            camera.targetTexture = null;
        }
           
    }
  



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _WillFakeScreenShot = true;
            OnPostRender();
            Debug.Log("김취!~");
        }
        else
        {
            _WillFakeScreenShot = false;
        }
    }


}
