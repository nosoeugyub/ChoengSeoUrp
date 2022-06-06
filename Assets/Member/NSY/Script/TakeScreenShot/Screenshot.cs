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
    private Texture2D texture;
    public Texture2D Texutre
    {
        get
        {
            return texture;
        }
        set
        {
            texture = value;
            if (texture ==null)
            {
                return;
            }
        }
    }

    private void OnValidate()
    {
        
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

            Texture2D screenshotTexture = new Texture2D(Width, Hight, TextureFormat.RGB24, false); // 화면 크기의 텍스쳐를 생성
            Texutre = screenshotTexture;
         
            Rect rect = new Rect(0, 0, Width, Hight); //캡쳐 영역을지정
            camera.Render(); 
            RenderTexture.active = RenderTexture;
           

            screenshotTexture.ReadPixels(rect, 0, 0); //텍스쳐 픽셀에 저장

            screenshotTexture.Apply();
            
            //pc 저장
            byte[] byteArray = screenshotTexture.EncodeToPNG(); // 이미지 저장
            System.IO.File.WriteAllBytes(TotalPath, screenshotTexture.EncodeToPNG());
            for (int i = 0; i < PageMat.Count; i++)
            {
                if (PageMat[i].GetTexture("_MainTex") == null)
                {
                    PageMat[i].SetTexture("_MainTex", Texutre);
                    camera.targetTexture = null;
                    return;
                }
            }

             
            //  Destroy(screenshotTexture);

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
    #region
    Mesh CreateQuad(Vector3 normal, Vector3 right, Vector2 size, Vector2 uvFrom, Vector2 uvTo, Vector3 positionOffset, Color color)
    {
        Vector3 up = -Vector3.Cross(normal, right);
        right = right * size.x * 0.5f;
        up = up * size.y * 0.5f;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = -right - up + positionOffset;
        vertices[1] = right - up + positionOffset;
        vertices[2] = right + up + positionOffset;
        vertices[3] = -right + up + positionOffset;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 1;
        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;

        Vector2[] uvs = new Vector2[4];
        uvs[0] = uvFrom;
        uvs[1] = new Vector2(uvTo.x, uvFrom.y);
        uvs[2] = uvTo;
        uvs[3] = new Vector2(uvFrom.x, uvTo.y);

        Color[] colors = new Color[4];
        for (int i = 0; i < 4; ++i)
        {
            colors[i] = color;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.colors = colors;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }

    #endregion
}
