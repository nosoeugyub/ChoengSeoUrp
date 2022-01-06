using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Cam;


namespace TT.ObjINTERACT
{
    public class InteractPlayer : MonoBehaviour
    { 
        public bool isInteracting;

        InteractUI InteractUI;
        CameraManager CameraManager;
        void Start()
        {
            isInteracting = false;
            InteractUI = FindObjectOfType<InteractUI>();
            CameraManager = FindObjectOfType<CameraManager>();
        }

       
        void Update()
        {
            //상호작용할 수 있는 영역 들어갈 때
            if (isInteracting)
            {
               //상호작용 안내 UI 불 수 있음
                if (!InteractUI.onInteractTrigger)
                {
                    InteractUI.ShowPlayerInteractGuide();
                }  

            }
            //상호작용할 수 있는 영역 나갈 때 
            if (!isInteracting)
            {
                //상호작용 안내 UI 없을
                InteractUI.UnshowPlayerInteractGuide();
                InteractUI.onInteractTrigger = false;
            }

           
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "TutItem")
            {
                if (!isInteracting)
                {
                    isInteracting = true;
                    InteractObject interactobj = other.gameObject.GetComponent<InteractObject>();
                    interactobj.isinteracting = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "TutItem")
            {
                if (isInteracting)
                {
                    isInteracting = false;
                    InteractObject interactobj = other.gameObject.GetComponent<InteractObject>();
                    interactobj.isinteracting = false;
                }
            }

           
        }
       
    }
}
   
