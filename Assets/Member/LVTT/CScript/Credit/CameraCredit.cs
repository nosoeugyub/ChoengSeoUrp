using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cam
{
    public class CameraCredit : MonoBehaviour
    {
        [SerializeField] GameObject mainCam;
        [SerializeField] CameraManager CamManager;
        [SerializeField] Animator WhiteScreenAnim;
        void Start()
        {
            Invoke("Zoomout", 0.5f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Zoomout()
        {
            //CamManager.DeactiveSubCamera();
            CamManager.ActiveSubCamera(1);
            WhiteScreenAnim.SetTrigger("startwhitescreen");
        }
    }

}
