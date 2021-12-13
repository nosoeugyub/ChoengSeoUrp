using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Game.Cam
{
    public class CameraController : MonoBehaviour
    {
        public float RotateXSpeed;
        public float RotateYSpeed;
        float XRotangle;
        float YRotangle;
        //private CinemachineVirtualCamera virtualCamera;
        [SerializeField]
        private Camera mainCamera;
        // Start is called before the first frame update
        void Start()
        {
            //virtualCamera = GetComponent<CinemachineVirtualCamera>();

        }

        // Update is called once per frame
        void Update()
        {
            CameraRotate();
        }

        void CameraRotate()
        {
            XRotangle += Input.GetAxis("Mouse Y") * RotateXSpeed * -Time.deltaTime;
            // transform.localRotation = Quaternion.AngleAxis(XRotangle, Vector3.up);
            //XRotangle = Mathf.Clamp(YRotangle, -25, 30);
            if(XRotangle<-25)
            {
                XRotangle = -25;
            }
            if (XRotangle > 30)
            {
                XRotangle = 30;
            }

            YRotangle += Input.GetAxis("Mouse X") * RotateYSpeed * -Time.deltaTime;
            //transform.localRotation = Quaternion.AngleAxis(YRotangle, Vector3.right);
            if (YRotangle > -100)
            {
                YRotangle = -100;
            }
            if (YRotangle < -250)
            {
                YRotangle = -250;
            }

            transform.localRotation = Quaternion.Euler(XRotangle,YRotangle,0);
        }
    }
}

