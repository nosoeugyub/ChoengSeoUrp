using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TT.ObjINTERACT
{
    public class TutUIAnimControl : MonoBehaviour
    {
        Animator UIAnim;
        InteractUI InteractUI;
        void Start()
        {
            UIAnim = GetComponent<Animator>();
            InteractUI = FindObjectOfType<InteractUI>();

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.K))
            {
                OnButtonPressAnim();
                //Invoke(InteractUI.UnshowPlayerInteract, 0.5f);
               // InteractUI.UnshowPlayerInteract();
            }
        }

        public void OnButtonPressAnim()
        {
            UIAnim.SetTrigger("OnButtonPress");
        }

        
    }

}
