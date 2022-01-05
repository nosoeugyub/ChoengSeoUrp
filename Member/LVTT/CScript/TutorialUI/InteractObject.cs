using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TT.ObjINTERACT
    {
   
    public class InteractObject : MonoBehaviour
    {
        public bool isinteracting;
        public float ShowUIDistant;
        [SerializeField] Transform ItemUI;
        public GameObject prefabUi;
        private GameObject TutUi=null;
        [SerializeField] Vector3 ItemUiOffset;

        private GameObject ThePlayer;
        public bool TutUIisOn;
        InteractUI InteractUI;
     
        void Start()
        {
            TutUIisOn = false;
            ThePlayer =GameObject.FindGameObjectWithTag("Player");
            InteractUI = FindObjectOfType<InteractUI>();
        }

        void Update()
        {
            float DistantFromPlayer = Vector3.Distance(transform.position, ThePlayer.transform.position);

            if (ItemUI.childCount > 0)
            {
                TutUi.transform.position = Camera.main.WorldToScreenPoint(transform.position+ ItemUiOffset);



                float UiScaleValue = Vector3.Distance(transform.position, ThePlayer.transform.position);
                UiScaleValue = Mathf.Clamp(UiScaleValue, 0.1f, 0.5f);
                TutUi.transform.localScale = new Vector3(UiScaleValue, UiScaleValue, 0);

                if (DistantFromPlayer > ShowUIDistant)
                {
                    //InteractUI.UnshowPlayerInteract();

                    UnshowItemUi();
                    TutUIisOn = false;
                }

                if (DistantFromPlayer < 1.0f)
                {
                    UnshowItemUi();
                    TutUIisOn = false;
                }    


            }

            if (ItemUI.childCount <= 0)
            {
                if (DistantFromPlayer <= ShowUIDistant && DistantFromPlayer >= 1.0f)
                {

                    if (!TutUIisOn)
                    {
                        //if(!InteractUI.onInteractTrigger)
                        //{ InteractUI.ShowPlayerInteract();
                        //  }
            
                        ShowItemInteract();
                        TutUIisOn = true;
                    }

                }
                if(DistantFromPlayer>ShowUIDistant)
                {

                }

             }

            }
       
        void ShowItemInteract()
        {
            TutUi = Instantiate(prefabUi, ItemUI.transform);
        }
        void UnshowItemUi()
        {
            foreach (Transform child in ItemUI.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }    
    }
}

