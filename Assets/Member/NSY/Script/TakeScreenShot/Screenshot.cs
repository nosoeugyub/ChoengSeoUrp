﻿using NSY.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Screenshot : MonoBehaviour
{


    public event Action OnSceenShot;


    [Header("플래쉬 애니")]
    private Animator FleshAnim;




    [Header("모두의 페이지")]
    [SerializeField]
    private List<Material> EveryMat;



    private Camera SceenShotCam;//ui카메라가찍히기 전에 게임 카메라에 있는 랜더텍스쳐를 활용해 ui제외한 부분들을 스크린샷처럼 찍음 
    public int Width; //가로
    public int Hight; //세로

  
    public string folderName = "ScreenShot";
    public string publicfolderName = "ScreenShots";
    public string fileName = "MyScreenShot";
    public string fileNames = "MyScreenShots";
    public string extName = "png";

    public bool _WillFakeScreenShot;

    private EnvironmentManager manager;
    private void Awake()
    {

        SceenShotCam = GetComponent<Camera>();
      //  SceenShotCam.GetComponent<Camera>().enabled = false;

        manager = FindObjectOfType<EnvironmentManager>();
    }


    private string RootPath
    {
        get
        {
            return Application.dataPath;
        }
    }

    [SerializeField]
    private Light lighting;
    public Light _Lighting
    {
        get
        {
            return lighting;
        }
        set
        {
            lighting = value;
            if (texture1 == null)
            {
                return;
            }
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
    //온전한
    [SerializeField]
    private Texture2D texture3;
    public Texture2D Texutre3
    {
        get
        {
            return texture3;
        }
        set
        {
            texture3 = value;
            if (texture3 == null)
            {
                return;
            }
        }
    }


    [SerializeField]
    private Material material1;
    public Material Material1
    {
        get
        {
            return material1;
        }
        set
        {
            material1 = value;
            if (Texutre1 != null)
            {
                material1.SetTexture("_MainTex", Texutre1);
            }

        }
    }

    [SerializeField]
    private Material material2;
    public Material Material2
    {
        get
        {
            return material2;
        }
        set
        {
            material2 = value;
            if (Texutre2 != null)
            {
                material2.SetTexture("_MainTex", Texutre2);
            }
          
        }
    }
    [SerializeField]
    private Material material3;
    public Material Material3
    {
        get
        {
            return material3;
        }
        set
        {
            material3 = value;
            if (Texutre3 != null)
            {
                material3.SetTexture("_MainTex", Texutre3);
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
            SceenShotCam.transform.position = CamPos.transform.position;
        }
    }

    private void OnValidate()
    {
        Texutre1 = texture1;
        Texutre2 = texture2;
        Texutre3 = texture3;
        Material1 = material1;
        Material2 = material2;
        Material3 = material3;
        _Page = Page;
        _CamPos = CamPos;
        _Lighting = lighting;

    }
    public Camera OringinCAm;


   
    private string FolderPath => $"{RootPath}/{folderName}";
    private string TotalPath => $"{FolderPath}/{fileName}_{DateTime.Now.ToString("MMdd_HHmmss")}.{extName}";
    private string publicFolder => $"{publicfolderName}"; //밖에 생성됨 
    private string publicTotalPath => $"{publicFolder}/{fileNames}_{DateTime.Now.ToString("MMdd_HHmmss")}.{extName}";


  


    private void OnPostRender()
    {
       
        if (_WillFakeScreenShot)
        {

            if (Directory.Exists(FolderPath) == false)
            {
                Debug.Log("폴더없으니 만들겠음");
                Directory.CreateDirectory(FolderPath);
            }
            if (Directory.Exists(publicFolder) == false)
            {
                Debug.Log("밖에폴더없으니 만들겠음");
                Directory.CreateDirectory(publicFolder);
            }


            _WillFakeScreenShot = false;


            Width = Screen.width;
            Hight = Screen.height;

            SceenShotCam.transform.position = _CamPos.transform.position;

            RenderTexture RenderTexture = new RenderTexture(Width, Hight, 24);


            SceenShotCam.targetTexture = RenderTexture;

            Texture2D screenshotTexture1 = new Texture2D(Width, Hight, TextureFormat.RGB24, false); //처음 끝 화면 크기의 텍스쳐를 생성
            Texture2D screenshotTexture2 = new Texture2D(Width, Hight, TextureFormat.RGB24, false); //중앙부터 끝 화면 크기의 텍스쳐를 생성
            //온전한 한장
            Texture2D screenshotTexture3 = new Texture2D(Width, Hight, TextureFormat.RGB24, false);

            Texutre1 = screenshotTexture1;//처음부터끝
            Texutre2 = screenshotTexture2; //중앙부터끝
            Texutre3 = screenshotTexture3; //전체 다~

            Texutre1.wrapMode = TextureWrapMode.Clamp;//노노해
            Texutre2.wrapMode = TextureWrapMode.Clamp; //반복 노노해
            Texutre3.wrapMode = TextureWrapMode.Clamp; //반복 노노해!

            Rect halfrect1 = new Rect(0, 0, Width / 2, Hight); //처음부터 반영역
            Rect halfrect2 = new Rect(Width / 2, 0, Width / 2, Hight);//캡쳐영역을 중간부터 끝
            Rect halfrect3 = new Rect( 0, 0, Width , Hight);//전체 


            SceenShotCam.Render();
            RenderTexture.active = RenderTexture;

            screenshotTexture1.ReadPixels(halfrect2, 0, 0); //텍스쳐 픽셀에 저장
            screenshotTexture2.ReadPixels(halfrect1, 0, 0);
            screenshotTexture3.ReadPixels(halfrect3, 0, 0); //전체



            screenshotTexture1.Apply();
            screenshotTexture2.Apply();
            screenshotTexture3.Apply();

            //pc 저장
            byte[] byteArray = screenshotTexture1.EncodeToPNG(); // 이미지 저장
            byte[] byteArray2 = screenshotTexture2.EncodeToPNG(); // 이미지 저장
            byte[] byteArray3 = screenshotTexture3.EncodeToPNG(); // 전체 이미지 저장
                                                                  //
                                                                  //Ver 2  . 무지성 저장 카메라 영역에대한
            #region
            //if (Page == 1 &&
            //     EveryMat[0].GetTexture("_MainTex") == null)
            //{
            //    SceenShotCam.enabled = true;
            //    EveryMat[0].SetTexture("_MainTex", Texutre2);
            //    EveryMat[1].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 2 &&
            //   EveryMat[2].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[2].SetTexture("_MainTex", Texutre2);
            //    EveryMat[3].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 3 &&
            //   EveryMat[4].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[4].SetTexture("_MainTex", Texutre2);
            //    EveryMat[5].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 4 &&
            //   EveryMat[6].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[6].SetTexture("_MainTex", Texutre2);
            //    EveryMat[7].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 5 &&
            //   EveryMat[8].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[8].SetTexture("_MainTex", Texutre2);
            //    EveryMat[9].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 6 &&
            //   EveryMat[10].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[10].SetTexture("_MainTex", Texutre2);
            //    EveryMat[11].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 7 &&
            //   EveryMat[12].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[12].SetTexture("_MainTex", Texutre2);
            //    EveryMat[13].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 8 &&
            // EveryMat[14].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[14].SetTexture("_MainTex", Texutre2);
            //    EveryMat[15].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 9 &&
            // EveryMat[16].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[16].SetTexture("_MainTex", Texutre2);
            //    EveryMat[17].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 10 &&
            // EveryMat[18].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[18].SetTexture("_MainTex", Texutre2);
            //    EveryMat[19].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 11 &&
            // EveryMat[20].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[20].SetTexture("_MainTex", Texutre2);
            //    EveryMat[21].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 12 &&
            // EveryMat[22].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[22].SetTexture("_MainTex", Texutre2);
            //    EveryMat[23].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 13 &&
            // EveryMat[24].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[24].SetTexture("_MainTex", Texutre2);
            //    EveryMat[25].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 14 &&
            // EveryMat[26].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[26].SetTexture("_MainTex", Texutre2);
            //    EveryMat[27].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 15 &&
            // EveryMat[28].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[28].SetTexture("_MainTex", Texutre2);
            //    EveryMat[29].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 16 &&
            // EveryMat[30].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[30].SetTexture("_MainTex", Texutre2);
            //    EveryMat[31].SetTexture("_MainTex", Texutre1);
            //}
            //if (Page == 17 &&
            // EveryMat[32].GetTexture("_MainTex") == null)
            //{
            //    EveryMat[32].SetTexture("_MainTex", Texutre2);
            //    EveryMat[33].SetTexture("_MainTex", Texutre1);
            //     GetComponent<Camera>().targetTexture = null;
            //    SceenShotCam.enabled = false;
            //}
            #endregion
            //Ver 1 . NPC만                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     c                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
            #region
            Debug.Log("홀로롤로 할머니22");
            Material1.SetTexture("_MainTex", Texutre2);
            Material2.SetTexture("_MainTex", Texutre1);
            Material3.SetTexture("_MainTex", Texutre3);
            for (int i = 0; i < SuperManager.Instance.buildingManager.buildings.Count; i++)//빌딩스를 체크하여
            {
                Debug.Log("호잉호잉");
                for (int j = 0; i < 14; i++)
                {
                    if (Page == i &&
                    SuperManager.Instance.buildingManager.buildings[j]._livingCharacter != null)// 
                    {
                        Debug.Log(i + "번들어감");
                        Material1.SetTexture("_MainTex", Texutre2);
                        Material2.SetTexture("_MainTex", Texutre1);
                        Material3.SetTexture("_MainTex", Texutre3);
                    }
                    else
                    {
                        Debug.Log("임소정 애미");
                        Material1.SetTexture("_MainTex", Texutre2);
                        Material2.SetTexture("_MainTex", Texutre1);
                        Material3.SetTexture("_MainTex", Texutre3);
                    }
                }
                

            }


            #endregion
            System.IO.File.WriteAllBytes(TotalPath, screenshotTexture1.EncodeToPNG());
            System.IO.File.WriteAllBytes(TotalPath, screenshotTexture2.EncodeToPNG());
            System.IO.File.WriteAllBytes(publicTotalPath, screenshotTexture3.EncodeToPNG()); //PublicTotalPath

            SceenShotCam.targetTexture = null;
            SceenShotCam.GetComponent<Camera>().enabled = false;
        }



    }


    // Update is called once per frame
    public void OnSceenShotEvent()
    {


        
        if (_WillFakeScreenShot == false)
        {
            _WillFakeScreenShot = true;
            SceenShotCam.GetComponent<Camera>().enabled = true;

        }
        Debug.Log("임소정씨발");
        OnPostRender();
        _WillFakeScreenShot = false;
    }



}
