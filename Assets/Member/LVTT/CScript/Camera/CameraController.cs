using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Game.Cam
{
    public class CameraController : MonoBehaviour
    {
        //public float MaxRotY;
        //public float MinRotY;
        /// <summary>
        //public float StartCamXRot;
        //public float RotateXSpeed;
        //public Transform target;
        /// </summary>

        // public Vector3 offset;
        float XRotangle;
        // float smoothedSpeed = 0.125f;


        CameraManager CamManager;
        void Start()
        {
            CamManager = FindObjectOfType<CameraManager>();
            //SetStartCamPos();

        }

        // Update is called once per frame

        private void Update()
        {
            

        void LateUpdate()
        {
            //Vector3 desiredPosition = target.position + offset;
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothedSpeed);
            //transform.position = smoothedPosition;
            
            //Rotate Camera by draging mouse
            //if(Input.GetMouseButton(0))
            //{
            //    CameraRotate();
            //}    
            ///////////////////////////////
        
        }

        //void SetStartCamPos()
        //{
        //    transform.localRotation = Quaternion.Euler(0, StartCamXRot, 0);
        //    XRotangle = StartCamXRot;
        //}
        

        //void CameraRotate()
        //{
        //    XRotangle += Input.GetAxis("Mouse X") * RotateXSpeed * -Time.deltaTime;
        //    transform.localRotation = Quaternion.AngleAxis(XRotangle, Vector3.up);
           
        //   target.localRotation = Quaternion.AngleAxis(XRotangle, Vector3.up);
        //}

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


        //Rotate both direction
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
}

