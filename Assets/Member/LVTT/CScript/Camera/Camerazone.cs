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
        static Camerazone nowTrig;
        public static int camcount;
        void Start()
        {
            CamManager = FindObjectOfType<CameraManager>();
            camcount = 0;
        }
        public bool SameAs(Camerazone other)
        {
            return this.virtualCamera == other.virtualCamera;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.GetComponent<CharacterController>())
                {

                    nowTrig = this;

                    if (camcount <= 0)
                    {
                        Debug.Log(nowCam + "On");
                        if (nowCam)
                            nowCam.SetActive(false);
                        nowCam = virtualCamera;
                        nowCam.SetActive(true);
                    }
                    else
                    {
                        nowCam = virtualCamera;
                    }
                    camcount++;
                    Debug.Log(nowTrig + "<now  this>" + gameObject.name + " ++ " + camcount);
                }
            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && other.GetComponent<CharacterController>())
            {
                if (nowTrig.virtualCamera != this.virtualCamera)
                {
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
                Debug.Log(nowTrig + "<now  this>" + gameObject.name + " -- " + camcount);

            }
        }

        private void OnValidate()
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }

}
