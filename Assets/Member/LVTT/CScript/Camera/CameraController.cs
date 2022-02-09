using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Game.Cam
{
    public class CameraController : MonoBehaviour
    {
        [Header("X Rotate Area")]
        public float MinRotX;
        public float MaxRotX; 
        [Header("X Rotation Option")]
        public float StartCamXRot;
        public float RotateXSpeed;
        public Transform target;
       // public Vector3 offset;
        float XRotangle;
        public bool reverseRotation;


        CameraManager CamManager;
        void Start()
        {
            CamManager = FindObjectOfType<CameraManager>();
           // SetStartCamPos();

        }

        // Update is called once per frame
        void Update()
        {
            //Vector3 desiredPosition = target.position + offset;
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothedSpeed);
            //transform.position = smoothedPosition;

            //if(Input.GetMouseButton(0))
            //{
            //    CameraRotate();
            //}    
            
            //플레이어가 X방향 이동할 때 카메라 자동 회전해당 
            
                        if (reverseRotation)
                        {
                            CameraRotate();
                        }

                        if (!reverseRotation)
                        {
                            CameraCornerRotate();
                        } 
        }

        void SetStartCamPos()
        {
            transform.localRotation = Quaternion.Euler(0, StartCamXRot, 0);
            XRotangle = StartCamXRot;
        }
        

        void CameraRotate()
        {
            //XRotangle += Input.GetAxis("Mouse X") * RotateXSpeed * -Time.deltaTime;
            XRotangle += Input.GetAxisRaw("Horizontal") * (-RotateXSpeed) * -Time.deltaTime;
            transform.localRotation = Quaternion.AngleAxis(XRotangle, Vector3.up);
            XRotangle = Mathf.Clamp(XRotangle, MinRotX, MaxRotX);

            target.localRotation = Quaternion.AngleAxis(XRotangle, Vector3.up);
        }

        void CameraCornerRotate()
        {
            //XRotangle += Input.GetAxis("Mouse X") * RotateXSpeed * -Time.deltaTime;
            XRotangle += Input.GetAxisRaw("Horizontal") * (RotateXSpeed) * -Time.deltaTime;
            transform.localRotation = Quaternion.AngleAxis(XRotangle, Vector3.up);
            XRotangle = Mathf.Clamp(XRotangle, MinRotX, MaxRotX);

            target.localRotation = Quaternion.AngleAxis(XRotangle, Vector3.up);
        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    if(other.gameObject.tag=="Floor")
        //    {
        //        CamManager.ActiveCamera(1);
        //    }
        //}

        //private void OnTriggerExit(Collider other)
        //{
        //    if (other.gameObject.tag == "Floor")
        //    {
        //        CamManager.DeactiveCamera(1);
        //    }
        //}
        //void CameraRotate()
        //{
        //    XRotangle += Input.GetAxis("Mouse Y") * RotateYSpeed * -Time.deltaTime;
        //    // transform.localRotation = Quaternion.AngleAxis(XRotangle, Vector3.up);
        //    //XRotangle = Mathf.Clamp(YRotangle, -25, 30);
        //    if(XRotangle< MinRotY)
        //    {
        //        XRotangle = MinRotY;
        //    }
        //    if (XRotangle > MaxRotY)
        //    {
        //        XRotangle = MaxRotY;
        //    }

        //    YRotangle += Input.GetAxis("Mouse X") * RotateXSpeed * -Time.deltaTime;
        //    //transform.localRotation = Quaternion.AngleAxis(YRotangle, Vector3.right);
        //    //if (YRotangle > -100)
        //    //{
        //    //    YRotangle = -100;
        //    //}
        //    //if (YRotangle < -250)
        //    //{
        //    //    YRotangle = -250;
        //    //}

        //    transform.localRotation = Quaternion.Euler(XRotangle,YRotangle,0);

        ///////
        //if (Input.GetKey(KeyCode.Q))
        //{
        //   XRotangle +=  RotateXSpeed * -Time.deltaTime;
        //    transform.localRotation = Quaternion.AngleAxis(XRotangle, Vector3.up);
        //}

        //if (Input.GetKey(KeyCode.E))
        //{
        //    XRotangle += (-RotateXSpeed) * -Time.deltaTime;
        //    transform.localRotation = Quaternion.AngleAxis(XRotangle, Vector3.up);
        //}
        //}


    }
}

