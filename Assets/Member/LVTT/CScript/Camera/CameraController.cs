using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cam
{
    public class CameraController : MonoBehaviour
    {
        public float MaxRotY;
        public float MinRotY;
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
           

        }

        // Update is called once per frame
        void Update()
        {
            CameraRotate();
           //if(Input.GetKey(KeyCode.Q))
           // {
           //     XRotangle +=  RotateXSpeed * -Time.deltaTime;

           //     transform.localRotation = Quaternion.Euler(XRotangle, YRotangle, 0);
           //     //transform.localRotation = Quaternion.AngleAxis(XRotangle, Vector3.up);
           // }

           // if (Input.GetKey(KeyCode.E))
           // {
           //     XRotangle += (-RotateXSpeed) * -Time.deltaTime;
           //     transform.localRotation = Quaternion.Euler(XRotangle, YRotangle, 0);
           // }


        }

        void CamRotateY()
        {
            YRotangle += Input.GetAxis("Mouse Y") * RotateYSpeed * -Time.deltaTime;
            if (YRotangle < MinRotY)
            {
                YRotangle = MinRotY;
            }
            if (YRotangle > MaxRotY)
            {
                YRotangle = MaxRotY;
            }
            transform.localRotation = Quaternion.AngleAxis(YRotangle, Vector3.right);
        }
        void CameraRotate()
        {
            XRotangle += Input.GetAxis("Mouse Y") * RotateYSpeed * -Time.deltaTime;
            // transform.localRotation = Quaternion.AngleAxis(XRotangle, Vector3.up);
            //XRotangle = Mathf.Clamp(YRotangle, -25, 30);
            if(XRotangle< MinRotY)
            {
                XRotangle = MinRotY;
            }
            if (XRotangle > MaxRotY)
            {
                XRotangle = MaxRotY;
            }

            YRotangle += Input.GetAxis("Mouse X") * RotateXSpeed * -Time.deltaTime;
            //transform.localRotation = Quaternion.AngleAxis(YRotangle, Vector3.right);
            //if (YRotangle > -100)
            //{
            //    YRotangle = -100;
            //}
            //if (YRotangle < -250)
            //{
            //    YRotangle = -250;
            //}

            transform.localRotation = Quaternion.Euler(XRotangle,YRotangle,0);
        }
    }
}

