using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.Rendering;

public class Screenshot : MonoBehaviour
{
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



    private void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_EndCameraRendering;
    }
    private void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_EndCameraRendering;
    }

    private void RenderPipelineManager_EndCameraRendering(ScriptableRenderContext arg1, Camera arg2)
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
            Texture2D screenshotTexture = new Texture2D(Width, Hight, TextureFormat.RGB24, false); // 화면 크기의 텍스쳐를 생성
            Rect rect = new Rect(0, 0, Width, Hight); //캡쳐 영역을지정
            screenshotTexture.ReadPixels(rect, 0, 0); //텍스쳐 픽셀에 저장
         screenshotTexture.Apply();
         //pc 저장
         byte[] byteArray = screenshotTexture.EncodeToPNG(); // 이미지 저장
         System.IO.File.WriteAllBytes(TotalPath, screenshotTexture.EncodeToPNG());

            Destroy(screenshotTexture);
        
        }
        RefreshAndroidGallery(TotalPath);
    }
  
    private void RefreshAndroidGallery(string imageFilePath)
    {
#if !UNITY_EDITOR
        AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2]
        { "android.intent.action.MEDIA_SCANNER_SCAN_FILE", classUri.CallStatic<AndroidJavaObject>("parse", "file://" + imageFilePath) });
        objActivity.Call("sendBroadcast", objIntent);
#endif
    }




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _WillFakeScreenShot = true;
            Debug.Log("김취!~");
        }
        else
        {
            _WillFakeScreenShot = false;
        }
    }


}
