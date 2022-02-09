using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Cam;


namespace TT.ObjINTERACT
{
    public class SignPost : MonoBehaviour
    {
       // public bool isinteracting;
        public float ShowSignPostDistant;//F키로 zoomin/zoomout Player와 표지판의 사용할 슈 있는 영역 
        public float AutoZoomTime;
        public bool FirstSign;// true이면 게임시작하고 나서 그 표지판에 자동 zoomin-->AutozoomTime 기다리고-->zoomout
        private GameObject ThePlayer;
        CameraManager CamManager;
        void Start()
        {
            ThePlayer = GameObject.FindGameObjectWithTag("Player");
            CamManager = FindObjectOfType<CameraManager>();

            if (FirstSign)
            //게임 시작하고 나서 표지판 가르키기
            {
                StartCoroutine(CamManager.AutoFocusObjectLocation(gameObject.transform, AutoZoomTime, 0));
            }
        }

        // Update is called once per frame
        void Update()
        {
            //
            float DistantFromPlayer = Vector3.Distance(transform.position, ThePlayer.transform.position);
            if (DistantFromPlayer <= ShowSignPostDistant)
            {
                CamManager.ChangeFollowTarger(gameObject.transform, 0);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ChangeView();
                }
            }

        }

        void ChangeView()
        {
            switch (CamManager.IsZoom)
            {
                case true:
                    CamManager.DeactiveSubCamera(0);
                    CamManager.IsZoom = false;
                    break;
                case false:
                    CamManager.ActiveSubCamera(0);
                    CamManager.IsZoom = true;
                    break;
            }
        }
    }
}

