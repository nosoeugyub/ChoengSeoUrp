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

        [Header("SubCameraList")]
        [SerializeField]
        private GameObject[] virtualCamera = null;
        //[SerializeField]
        //private CinemachineVirtualCamera [] virtualCamera = null;
        
        void Start()
        {
            DeactiveAllSubCam();  
        }
        void Update()
        {
            
        }

        void DeactiveAllSubCam()
        {
            for(int i=0;i<virtualCamera.Length;i++)
            {
                virtualCamera[i].SetActive(false);
            }     
        }
        
       public void ActiveCamera(int camNum) //다른 카메라 뷰 바꿈
        {
            virtualCamera[camNum].SetActive(true);
        }

        public void DeactiveCamera(int camNum)//메인 카메라에 돌아감
        {
            virtualCamera[camNum].SetActive(false);
        }

        public void ChangeFollowTarger(Transform newTarget,int camNum)//배열있는 SubCamera의 Followtarget가 바꿈
        {
            CinemachineVirtualCamera virtualCam = virtualCamera[camNum].GetComponent<CinemachineVirtualCamera>();
            virtualCam.Follow = newTarget;
        }

        public IEnumerator AutoFocusObjectLocation(Transform focusObject,float stayTime,int camNum)
        {
            //카메라의 Followtarget가 원하는 Object로 바꿈
            ChangeFollowTarger(focusObject, camNum);// camNum = 이 script에서 vísualcamera 배열의 있는 SubCamera 위치
            //카메라 뷰 바꿈
            ActiveCamera(camNum);
            //바꾼 후에 Object 뷰가 유지
            yield return new WaitForSeconds(stayTime);//stayTime = 메안 뷰 -> Object 뷰 바꾸는 시간  +뷰 유지 시간
            //메인 카메라에 다시 돌아간다
            DeactiveCamera(camNum);
        }   
    }
}

