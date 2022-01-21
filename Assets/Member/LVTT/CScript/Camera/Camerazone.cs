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
        private GameObject MainCam;
        void Start()
        {
            //virtualCamera.enabled = false;
         
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                virtualCamera.SetActive(true);

                MainCam.SetActive(false);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                virtualCamera.SetActive(true);

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
