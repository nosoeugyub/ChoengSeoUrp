using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Game.Cam

{ public class CameraManager : MonoBehaviour
    {
        //[Header("MainCameraList")]
        //[SerializeField]
        //private GameObject[] MainCamera = null;
        public bool LookIn;
        public bool IsZoom;
        [SerializeField]
        public GameObject MainCamera;
        [Header("SideCameraList")]
        [SerializeField]
        private GameObject[] SideCamera = null;
        [Header("CornerCameraList")]
        [SerializeField]
        private GameObject[] CornerCamera = null;
        [Header("SubCameraList")]
        [SerializeField]
        private GameObject[] virtualCamera = null;
        [SerializeField]
        bool IsMoveRight;
        //[SerializeField]
        //private CinemachineVirtualCamera [] virtualCamera = null;

        void Start()
        {
            LookIn = true;
            DeactiveAllSideCam();
            DeactiveAllCornerCam();
            DeaactiveAllSubCam();
        }
        void Update()
        {
            MoveDirectionCheck();

        }


    

        public void ChangeCamRotToArea(MapArea Area)
        {
            Debug.Log("TouchWestArea");
            List<CameraController> CornerCamControl = new List<CameraController>();
            foreach (GameObject cam in CornerCamera)
            {
                CornerCamControl.Add(cam.GetComponent<CameraController>());
                CameraController CornerCamControl1 = CornerCamera[0].GetComponent<CameraController>();
                CameraController CornerCamControl2 = CornerCamera[1].GetComponent<CameraController>();
                CameraController CornerCamControl3 = CornerCamera[2].GetComponent<CameraController>();
                CameraController CornerCamControl4 = CornerCamera[3].GetComponent<CameraController>();
                switch (Area)
                {
                    case MapArea.South:
                        {
                            CornerCamControl4.SetCamRot(CornerCamControl4.YRotMoveLeft);
                            CornerCamControl1.SetCamRot(CornerCamControl1.YRotMoveRight);
                            break;
                        }
                    case MapArea.East:
                        {
                            CornerCamControl1.SetCamRot(CornerCamControl1.YRotMoveLeft);
                            CornerCamControl2.SetCamRot(CornerCamControl2.YRotMoveRight);
                            break;
                        }
                    case MapArea.North:
                        {
                            CornerCamControl2.SetCamRot(CornerCamControl2.YRotMoveLeft);
                            CornerCamControl3.SetCamRot(CornerCamControl3.YRotMoveRight);
                            break;
                        }
                    case MapArea.West:
                        {
                            
                            CornerCamControl3.SetCamRot(CornerCamControl3.YRotMoveLeft);
                            CornerCamControl4.SetCamRot(CornerCamControl4.YRotMoveRight);
                           // Debug.Log("ChangeCamToWestArea");


                            break;
                        }
                }

            }

               

            
               
        }
        //void ChangeCornerCamRot()
        //{
        //    foreach (GameObject cam in CornerCamera)
        //    {
        //        CameraController camControl = cam.GetComponent<CameraController>();
        //        if (IsMoveRight)
        //        {


        //            camControl.SetCamRot(camControl.YRotMoveRight);

        //        }
        //        else
        //        {
        //            camControl.SetCamRot(camControl.YRotMoveLeft);
        //        }
        //    }
        //}
        void MoveDirectionCheck()
        {
            float XMovevalue = Input.GetAxisRaw("Horizontal");
            if(XMovevalue>0)
            {
                IsMoveRight = true;
            }

            if (XMovevalue < 0)
            {
                IsMoveRight = false;
            }
        }

        //public void MainCamRotate()
        //{
        //    if(IsMoveRight)
        //    {
        //        MainCamera.transform.rotation= Quaternion.Euler(10, 0, 0);
        //    }
        //    else
        //    {
        //        MainCamera.transform.rotation = Quaternion.Euler(10, 90, 0);
        //    }
        //}

        void DeactiveAllSideCam()
        {
            for(int i=0;i<SideCamera.Length;i++)
            {
                SideCamera[i].SetActive(false);
            }     
        }

        void DeactiveAllCornerCam()
        {
            for (int i = 0; i < CornerCamera.Length; i++)
            {
                CornerCamera[i].SetActive(false);
            }
        }

        void DeaactiveAllSubCam() {
            for (int i = 0; i < virtualCamera.Length; i++)
            {
             virtualCamera[i].SetActive(false);
            }
        }

        public void ActiveSubCamera(int camNum) //다른 카메라 뷰 바꿈
        {
            virtualCamera[camNum].SetActive(true);
        }

        public void DeactiveSubCamera(int camNum)//메인 카메라에 돌아감
        {
            virtualCamera[camNum].SetActive(false);
        }

        public void ChangeFollowTarger(Transform newTarget, int camNum)//배열있는 SubCamera의 Follow target가 바꿈
        {
            CinemachineVirtualCamera virtualCam = virtualCamera[camNum].GetComponent<CinemachineVirtualCamera>();
            virtualCam.Follow = newTarget;
            float TargetYRot = newTarget.rotation.eulerAngles.y;

            virtualCamera[camNum].transform.rotation = Quaternion.Euler(0, TargetYRot, 0);
        }
        public void ChangeLookTarger(Transform newTarget, int camNum)//배열있는 SubCamera의 LookAt target가 바꿈
        {
            CinemachineVirtualCamera virtualCam = virtualCamera[camNum].GetComponent<CinemachineVirtualCamera>();
            virtualCam.LookAt = newTarget;
          
        }
        public IEnumerator AutoFocusObjectLocation(Transform focusObject, float stayTime, int camNum)
        {
            //카메라의 Followtarget가 원하는 Object로 바꿈
            ChangeFollowTarger(focusObject, camNum);// camNum = 이 script에서 vísualcamera 배열의 있는 SubCamera 위치
            //카메라 뷰 바꿈
            ActiveSubCamera(camNum);
            IsZoom = true;
            //바꾼 후에 Object 뷰가 유지
            yield return new WaitForSeconds(stayTime);//stayTime = 메안 뷰 -> Object 뷰 바꾸는 시간  +뷰 유지 시간
            //메인 카메라에 다시 돌아간다
            DeactiveSubCamera(camNum);
            IsZoom = false;
        }

        
    }
}

