using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


namespace Game.Cam
{
    [RequireComponent(typeof(Collider))]
    public class Camerazone : MonoBehaviour
    {
        [SerializeField]
        //private CinemachineVirtualCamera virtualCamera = null;
        private GameObject virtualCamera;
        //[SerializeField]
        //private GameObject virtualCamera2;
        [SerializeField]
        private GameObject MainCam;
        [SerializeField]
        bool CornerZone;
        CameraManager CamManager;
        void Start()
        {
            CamManager = FindObjectOfType<CameraManager>();
            //LookIn = true;

        }

        // Update is called once per frame
        void Update()
        {
            //if (CanSwitchCam)
            //{
            //    if (Input.GetKeyDown(KeyCode.B))
            //    {
            //        ChangeView();
            //    }
            //}

            
           

        }

        //void ChangeView()
        //{
        //    switch (CamManager.LookIn)
        //    {
        //        case true:
        //            virtualCamera.SetActive(false);
        //            virtualCamera2.SetActive(true);
        //            CamManager.LookIn = false;
        //            break;
        //        case false:
        //            virtualCamera.SetActive(true);
        //            virtualCamera2.SetActive(false);
        //            CamManager.LookIn = true;
        //            break;
        //    }

        //}

     
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {

               

                MainCam.SetActive(false);
                virtualCamera.SetActive(true);
                
                // switch (CamManager.LookIn)
                //{
                //    case true:
                //        virtualCamera.SetActive(true);
                //        break;
                //    case false:
                //        virtualCamera2.SetActive(true);
                //        break;
                //}

            }
        }

   
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                MainCam.SetActive(false);
                
               
            }

         


        }
        private void OnTriggerExit(Collider other)
        {
            
            if (other.CompareTag("Player"))
            {
                //CanSwitchCam = false;
                //virtualCamera.enabled = false;
                virtualCamera.SetActive(false);
                //virtualCamera2.SetActive(false);
                MainCam.SetActive(true);
               CamManager.LookIn = true;
            }
        }

        private void OnValidate()
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }

}
