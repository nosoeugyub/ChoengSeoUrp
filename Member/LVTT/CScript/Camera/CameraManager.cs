using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Game.Cam
{ public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera [] virtualCamera = null;
        // Start is called before the first frame update
        void Start()
        {
            DeactiveAllSubCam();
        }

        // Update is called once per frame
        void Update()
        {
            //if (Input.GetKey(KeyCode.Q))
            //{
            //    ActiveCamera(0);
            //}

            //if(Input.GetKey(KeyCode.E))
            //{
            //    DeactiveCamera(0);
            //}
        }

        void DeactiveAllSubCam()
        {
            for(int i=0;i<virtualCamera.Length;i++)
            {
                virtualCamera[i].enabled = false;
            }

            
           
        }
        
       public void ActiveCamera(int camNum)
        {
            virtualCamera[camNum].enabled = true;
        }

        public void DeactiveCamera(int camNum)
        {
            virtualCamera[camNum].enabled = false;
        }
    }
}

