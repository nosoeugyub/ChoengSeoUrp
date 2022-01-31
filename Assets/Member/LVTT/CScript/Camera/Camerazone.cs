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
        [SerializeField]
        private GameObject virtualCamera2;
        [SerializeField]
        private GameObject MainCam;
        [SerializeField]
        bool CanSwitchCam;
        bool OnCam1 = true;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (CanSwitchCam)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ChangeView();
                }
            }

        }

        void ChangeView()
        {
            switch (OnCam1)
            {
                case true:
                    virtualCamera.SetActive(false);
                    virtualCamera2.SetActive(true);
                    OnCam1 = false;
                    break;
                case false:
                    virtualCamera.SetActive(true);
                    virtualCamera2.SetActive(false);
                    OnCam1 = true;
                    break;
            }

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                virtualCamera.SetActive(true);

                MainCam.SetActive(false);

                OnCam1 = true;

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
                //virtualCamera.enabled = false;
                virtualCamera.SetActive(false);

                MainCam.SetActive(true);
                // currentVirtualCam.enabled = true;
            }
        }

        private void OnValidate()
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }

}
