using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TT_INTERACT
{
    public class InteractPlayer : MonoBehaviour
    { 
        public bool isInteracting;
        void Start()
        {
            isInteracting = false;
        }

       
        void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "InteractObj")
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
            if (other.gameObject.tag == "InteractObj")
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
   
