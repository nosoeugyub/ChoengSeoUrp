using System.Collections;
using UnityEngine;

namespace Game.Cam
{
    public enum Introstate { First, Second, Third, Start, None, }
    public class CameraIntro : MonoBehaviour
    {
        [SerializeField] GameObject mainCam;
        [SerializeField] Transform SecondRot;
        [SerializeField] CameraManager CamManager;
        [SerializeField] Animator WhiteScreenAnim;
        [SerializeField] Animator curtainAnim;
        [SerializeField] Animator curtainAnim1;
        [SerializeField] CanvasGroup textUI;

        bool isRotate = false;

        public Introstate State;
        void Start()
        {
            // StartCoroutine(IntroRoutine());
            TextUIOnOff(false);
        }

        private void FixedUpdate()
        {
            if (isRotate)
            {
                CamRotate(mainCam);
            }

        }
        // Update is called once per frame
        void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                switch (State)
                {
                    case Introstate.First:
                        {
                            isRotate = true;
                            StartCoroutine(IntroRoutine());
                            TextUIOnOff(false);

                            break;
                        }
                    case Introstate.Second:
                        {


                            break;
                        }
                    case Introstate.Third:
                        {

                            break;
                        }
                    case Introstate.Start:
                        {
                            curtainAnim.SetBool("isOpen", true);
                            curtainAnim1.SetBool("isOpen", true);
                            State = Introstate.None;

                            TextUIOnOff(true);

                            break;
                        }
                    case Introstate.None:
                        {
                            break;
                        }
                }
            }
        }

        private void TextUIOnOff(bool ison)
        {
            if (ison)
            {
                StartCoroutine(alphaOn());

            }
            textUI.alpha = 0;
        }
        IEnumerator alphaOn()
        {
            yield return new WaitForSeconds(0.4f);
            while (textUI.alpha < 1)
            {
                textUI.alpha += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            State = Introstate.First;
        }
        void CamRotate(GameObject camera)
        {

            Transform curRot = camera.transform;
            camera.transform.localRotation = Quaternion.Lerp(curRot.rotation, SecondRot.rotation, Time.deltaTime * 3f);
            if (curRot.rotation == SecondRot.rotation)
            {
                isRotate = false;
            }

        }

        public void OnSecondstate()
        {
            ChangeCam();
            WhiteScreenAnim.SetTrigger("whitescreen");
            StartCoroutine(ChangeScene());

        }
        IEnumerator ChangeScene()
        {
            yield return new WaitForSeconds(2.5f);
            FindObjectOfType<SceneChangeManager>().LoadSceneFadeString("MainScene");
        }
        public void ChangeCam()
        {
            CamManager.DeactiveSubCamera(1);
            CamManager.ActiveSubCamera(2);
        }


        IEnumerator IntroRoutine()
        {
            yield return new WaitForSeconds(0.3f);

            CamManager.ActiveSubCamera(1);

            yield return new WaitForSeconds(2.0f);

            State = Introstate.Second;
        }
    }

}
