using UnityEngine;


namespace Game.Cam
{
    [RequireComponent(typeof(Collider))]
    public class Camerazone : MonoBehaviour
    {
        [SerializeField]
        private GameObject virtualCamera;
        [SerializeField]
        private GameObject MainCam;
        bool CanSwitchCam;
        CameraManager CamManager;
        static GameObject nowCam;
        static GameObject nowTrig;
        public static int camcount;
        void Start()
        {
            CamManager = FindObjectOfType<CameraManager>();
            camcount = 0;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.GetComponent<CharacterController>())
                {
                    nowCam = virtualCamera;
                    
                    nowTrig = this.gameObject;

                    if (camcount <= 0)
                    {
                        Debug.Log(nowCam + "On");
                        nowCam.SetActive(true);
                    }
                    camcount++;
                }
            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && other.GetComponent<CharacterController>())
            {

                if (nowTrig != this.gameObject)
                {
                    Debug.Log(gameObject.name);
                    Debug.Log(virtualCamera + "Off");
                    virtualCamera.SetActive(false);

                    if (camcount > 1)
                    {
                        Debug.Log(nowCam + "On");
                        nowCam.SetActive(true);
                    }

                }
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
