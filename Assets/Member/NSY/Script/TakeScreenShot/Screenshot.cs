using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    public List<Material> PageMat = new List<Material>();

    private string RootPath
    {
        get
        {
            return Application.dataPath;
        }
    }

   
   

    [SerializeField]
    private Texture2D texture1;
    public Texture2D Texutre1
    {
        get
        {
            return texture1;
        }
        set
        {
            texture1 = value;
            if (texture1 ==null)
            {
                return;
            }
        }
    }

    [SerializeField]
    private Texture2D texture2;
    public Texture2D Texutre2
    {
        get
        {
            return texture2;
        }
        set
        {
            texture2 = value;
            if (texture2 == null)
            {
                return;
            }
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
                Debug.Log("폴더없으니 만들겠음");
                Directory.CreateDirectory(FolderPath);
            }

            _WillFakeScreenShot = false;
            Width = Screen.width;
            Hight = Screen.height;

            RenderTexture RenderTexture = new RenderTexture(Width, Hight, 24);
           
            camera.targetTexture = RenderTexture;

            Texture2D screenshotTexture1 = new Texture2D(Width, Hight, TextureFormat.RGB24, false); //처음 끝 화면 크기의 텍스쳐를 생성
            Texture2D screenshotTexture2 = new Texture2D(Width, Hight, TextureFormat.RGB24, false); //중앙부터 끝 화면 크기의 텍스쳐를 생성

            Texutre1 = screenshotTexture1;//처음부터끝
            Texutre2 = screenshotTexture2; //중앙부터끝

            Texutre1.wrapMode = TextureWrapMode.Clamp;//노노해
            Texutre2.wrapMode = TextureWrapMode.Clamp; //반복 노노해
          
            Rect halfrect1 = new Rect(0, 0, Width/2 , Hight); //처음부터 반영역
            Rect halfrect2 = new Rect(Width/2, 0, Width/2, Hight);//캡쳐영역을 중간부터 끝
           
            camera.Render(); 
            RenderTexture.active = RenderTexture;

            screenshotTexture1.ReadPixels(halfrect2, 0, 0); //텍스쳐 픽셀에 저장
            screenshotTexture2.ReadPixels(halfrect1, 0, 0);
           
           


            screenshotTexture1.Apply();
            screenshotTexture2.Apply();
            //pc 저장
            byte[] byteArray = screenshotTexture1.EncodeToPNG(); // 이미지 저장
            byte[] byteArray2 = screenshotTexture2.EncodeToPNG(); // 이미지 저장

            System.IO.File.WriteAllBytes(TotalPath, screenshotTexture1.EncodeToPNG());
            System.IO.File.WriteAllBytes(TotalPath, screenshotTexture2.EncodeToPNG());
            for (int i = 0; i < PageMat.Count; i++)
            {
                if (PageMat[i].GetTexture("_MainTex") == null)
                {
                    PageMat[i].SetTexture("_MainTex", Texutre2);
                    PageMat[i+1].SetTexture("_MainTex", Texutre1);
                    camera.targetTexture = null;
                    return;
                }
                else
                {
                    continue;
                }
            }

             
           

        }

    }

    Texture2D Resize(Texture2D texture2D, int targetX, int targetY)//리사이즈
    {
        RenderTexture rt = new RenderTexture(targetX, targetY, 24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        return result;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("임소정 미래 인도 시금치장수");
            FindObjectOfType<SceneChangeManager>().slbal(2);
        }
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
    //메쉬를 잘라보실?
    
   
}
