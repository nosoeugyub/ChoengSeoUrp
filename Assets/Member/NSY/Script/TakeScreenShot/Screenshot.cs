using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    [Header("플래쉬 애니")]
    private Animator FleshAnim;




    [Header("모두의 페이지")]
    [SerializeField]
    private List<Material> EveryMat;

    [Header("1페이지 머테리얼")]
    [SerializeField]
    private List<Material> PageMat1;

    [Header("2페이지 머테리얼")]
    [SerializeField]
    private List<Material> PageMat2;

    [Header("3페이지 머테리얼")]
    [SerializeField]
    private List<Material> PageMat3;

    [Header("4페이지 머테리얼")]
    [SerializeField]
    private List<Material> PageMat4;

    [Header("5페이지 머테리얼")]
    [SerializeField]
    private List<Material> PageMat5;

    [Header("6페이지 머테리얼")]
    [SerializeField]
    private List<Material> PageMat6;

    [Header("7페이지 머테리얼")]
    [SerializeField]
    private List<Material> PageMat7;

    public Camera SceenShotCam;//ui카메라가찍히기 전에 게임 카메라에 있는 랜더텍스쳐를 활용해 ui제외한 부분들을 스크린샷처럼 찍음 
    public int Width; //가로
    public int Hight; //세로

    public string folderName = "ScreenShot";
    public string fileName = "MyScreenShot";
    public string extName = "png";

    public bool _WillFakeScreenShot;


    private void Awake()
    {
      //  FleshAnim.GetComponent<Animator>();
    }

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
            if (texture1 == null)
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
    [SerializeField]
    private int Page;
    public int _Page
    {
        get
        {
            return Page;
        }
        set
        {
            Page = value;
        }
    }


    [SerializeField]
    private Transform CamPos;
    public Transform _CamPos
    {
        get
        {
            return CamPos;
        }
        set
        {
            CamPos = value;
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

            SceenShotCam.transform.position = _CamPos.transform.position;

            RenderTexture RenderTexture = new RenderTexture(Width, Hight, 24);
            

            SceenShotCam.targetTexture = RenderTexture;

            Texture2D screenshotTexture1 = new Texture2D(Width, Hight, TextureFormat.RGB24, false); //처음 끝 화면 크기의 텍스쳐를 생성
            Texture2D screenshotTexture2 = new Texture2D(Width, Hight, TextureFormat.RGB24, false); //중앙부터 끝 화면 크기의 텍스쳐를 생성

            Texutre1 = screenshotTexture1;//처음부터끝
            Texutre2 = screenshotTexture2; //중앙부터끝

            Texutre1.wrapMode = TextureWrapMode.Clamp;//노노해
            Texutre2.wrapMode = TextureWrapMode.Clamp; //반복 노노해

            Rect halfrect1 = new Rect(0, 0, Width / 2, Hight); //처음부터 반영역
            Rect halfrect2 = new Rect(Width / 2, 0, Width / 2, Hight);//캡쳐영역을 중간부터 끝

            SceenShotCam.Render();
            RenderTexture.active = RenderTexture;

            screenshotTexture1.ReadPixels(halfrect2, 0, 0); //텍스쳐 픽셀에 저장
            screenshotTexture2.ReadPixels(halfrect1, 0, 0);




            screenshotTexture1.Apply();
            screenshotTexture2.Apply();
            //pc 저장
            byte[] byteArray = screenshotTexture1.EncodeToPNG(); // 이미지 저장
            byte[] byteArray2 = screenshotTexture2.EncodeToPNG(); // 이미지 저장

            //Ver 2  . 무지성 저장 카메라 영역에대한
            #region
            if (Page == 1 &&
                 EveryMat[0].GetTexture("_MainTex") == null)
            {
                EveryMat[0].SetTexture("_MainTex", Texutre2);
                EveryMat[1].SetTexture("_MainTex", Texutre1);
            }
            if (Page == 2 &&
               EveryMat[2].GetTexture("_MainTex") == null)
            {
                EveryMat[2].SetTexture("_MainTex", Texutre2);
                EveryMat[3].SetTexture("_MainTex", Texutre1);
            }
            if (Page == 3 &&
               EveryMat[4].GetTexture("_MainTex") == null)
            {
                EveryMat[4].SetTexture("_MainTex", Texutre2);
                EveryMat[5].SetTexture("_MainTex", Texutre1);
            }
            if (Page == 4 &&
               EveryMat[6].GetTexture("_MainTex") == null)
            {
                EveryMat[6].SetTexture("_MainTex", Texutre2);
                EveryMat[7].SetTexture("_MainTex", Texutre1);
            }
            if (Page == 5 &&
               EveryMat[8].GetTexture("_MainTex") == null)
            {
                EveryMat[8].SetTexture("_MainTex", Texutre2);
                EveryMat[9].SetTexture("_MainTex", Texutre1);
            }
            if (Page == 6 &&
               EveryMat[10].GetTexture("_MainTex") == null)
            {
                EveryMat[10].SetTexture("_MainTex", Texutre2);
                EveryMat[11].SetTexture("_MainTex", Texutre1);
            }
            if (Page == 7 &&
               EveryMat[12].GetTexture("_MainTex") == null)
            {
                EveryMat[12].SetTexture("_MainTex", Texutre2);
                EveryMat[13].SetTexture("_MainTex", Texutre1);
            }
            #endregion
            //Ver 1 . NPC만                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     c                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
            #region
            //for (int i = 0; i < SuperManager.Instance.buildingManager.buildings.Count; i++)
            //{
            //    Debug.Log(SuperManager.Instance.buildingManager.buildings[i]._livingCharacter);
            //    if (PageMat1[i].GetTexture("_MainTex") == null)// 사진도 비어있고
            //    {
            //        if (SuperManager.Instance.buildingManager.buildings[i] != null) // 빌딩스에 값이 할당됐다면
            //        {
            //            if (SuperManager.Instance.buildingManager.buildings[i]._livingCharacter != null)
            //            {
            //                Debug.Log("테리우스 김채원 닮음");
            //                PageMat1[i].SetTexture("_MainTex", Texutre2);
            //                PageMat1[i + 1].SetTexture("_MainTex", Texutre1);
            //                camera.targetTexture = null;
            //                Destroy(RenderTexture);
            //                return;
            //            }
            //            else
            //            {
            //                camera.targetTexture = null;
            //                Destroy(RenderTexture);
            //                continue;
            //            }
            //        }
            //    }
            //}
            #endregion


            System.IO.File.WriteAllBytes(TotalPath, screenshotTexture1.EncodeToPNG());
            System.IO.File.WriteAllBytes(TotalPath, screenshotTexture2.EncodeToPNG());
            SceenShotCam.targetTexture = null;

        }
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
          
            if (_WillFakeScreenShot == false)
            {
                _WillFakeScreenShot = true;
            }
                SceenShotCam.enabled = true;
         //   FleshAnim.Play("Crossfade_wait");
                OnPostRender();
                Debug.Log("김취!~");
            
            _WillFakeScreenShot = false;
        }
         
        
    }


 
}
