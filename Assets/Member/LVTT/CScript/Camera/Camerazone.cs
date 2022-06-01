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
        //[SerializeField]
        //bool FixedZone;
        bool CanSwitchCam;
        CameraManager CamManager;
        static GameObject nowCam;
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
            if (other.CompareTag("Player") && other.GetComponent<CharacterController>())
            {

                //CanSwitchCam = true;

                //MainCam.SetActive(false);
                //virtualCamera.SetActive(true);
                nowCam = virtualCamera;
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
            // 채원아.. CompareTag보다는 Layer 비교연산이 더 빠르단다..
            // GetCompont를 Stay에 호출하면 어떡하니..

            /*
            if (other.CompareTag("Player") && other.GetComponent<CharacterController>())
            {
                virtualCamera.SetActive(true);
            }
            */

            // 차라리 이렇게 쓰렴..... 안느리단다..
            if (other.CompareTag("Player"))
            {
                if (other.GetComponent<CharacterController>())
                    virtualCamera.SetActive(true);
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && other.GetComponent<CharacterController>())
            {
                //CanSwitchCam = false;
                //virtualCamera.enabled = false;
                virtualCamera.SetActive(false);
                //virtualCamera2.SetActive(false);
                //MainCam.SetActive(true);
                CamManager.LookIn = true;
            }
        }

        private void OnValidate()
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }

}
