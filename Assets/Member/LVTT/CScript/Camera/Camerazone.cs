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
        private CinemachineVirtualCamera virtualCamera = null;
       // CamreaCtrl CameraCtrl;
        // Start is called before the first frame update
        void Start()
        {
            virtualCamera.enabled = false;
           // CameraCtrl = FindObjectOfType<CamreaCtrl>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                virtualCamera.enabled = true;   
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                virtualCamera.enabled = false;
            }
        }

        private void OnValidate()
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }

}
