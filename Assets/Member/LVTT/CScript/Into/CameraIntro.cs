using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cam
{
    public enum Introstate {First,Second,Third }
    public class CameraIntro : MonoBehaviour
    {
        [SerializeField] GameObject mainCam;
        [SerializeField] Transform SecondRot;
        [SerializeField]CameraManager CamManager;
        [SerializeField] Animator WhiteScreenAnim;

        bool isRotate = false;
        
        public Introstate State;
        void Start()
        {
            // StartCoroutine(IntroRoutine());
            
        }

        private void FixedUpdate()
        {
            if(isRotate)
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
                  
                }
            }
        }

        void CamRotate(GameObject camera)
        {
            
            Transform curRot = camera.transform; 
            camera.transform.localRotation = Quaternion.Lerp(curRot.rotation,SecondRot.rotation,Time.deltaTime* 2.5f);
            if(curRot.rotation==SecondRot.rotation)
            {
                isRotate = false;
            }
           
        }    

        public void OnSecondstate()
        {
            ChangeCam();
            WhiteScreenAnim.SetTrigger("whitescreen");
        }    
        public void ChangeCam()
        {
            CamManager.DeactiveSubCamera(1);
            CamManager.ActiveSubCamera(2);
        }

        
        IEnumerator IntroRoutine()
        {
            yield return new WaitForSeconds(0.7f);
           
            CamManager.ActiveSubCamera(1);      
           
            yield return new WaitForSeconds(1.8f);
           
            State = Introstate.Second;
        }
    }

}
