using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TT.ObjINTERACT
{
    public class InteractPlayer : MonoBehaviour
    { 
        public bool isInteracting;

        InteractUI InteractUI;
        void Start()
        {
            isInteracting = false;
            InteractUI = FindObjectOfType<InteractUI>();
        }

       
        void Update()
        {
            if (isInteracting)
            {
                //if(!InteractUI.onInteractTrigger)
                //{ InteractUI.ShowInteract();
                //  }
               

            }

            if (!isInteracting)
            {
                //InteractUI.UnshowInteract();
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
   
