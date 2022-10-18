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
        public static int camcount;
        void Start()
        {
            CamManager = FindObjectOfType<CameraManager>();
            camcount = 0;
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
            if (other.CompareTag("Player"))
            {
                if (other.GetComponent<CharacterController>())
                {
                    nowCam = virtualCamera;

                    if (camcount <= 0)
                        nowCam.SetActive(true);

                    camcount++;

                    Debug.Log(virtualCamera);
                }
            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && other.GetComponent<CharacterController>())
            {
                Debug.Log(nowCam);
                virtualCamera.SetActive(false);

                if (camcount > 1)
                    nowCam.SetActive(true);

                CamManager.LookIn = true;
                camcount--;
            }
        }

        private void OnValidate()
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }

}
