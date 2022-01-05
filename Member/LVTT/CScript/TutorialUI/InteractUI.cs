using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace TT.ObjINTERACT
{
    
    public class InteractUI : MonoBehaviour
    {
        [SerializeField]public Transform Player_tr;
        
       
        //상호작용할 때 안내 UI
        [Header("InteractPlayerUI")]
        [SerializeField] Transform PlayerUI;
        [SerializeField] GameObject PrefabPlayerUi;
        [SerializeField] Vector3 PlayerUiOffset;
    

        ////////////////////////////
        GameObject UiInteract=null;
        
        
        public bool onInteractTrigger=false;
       
        void Start()
        {
           

        }

        
        void Update()
        {
            if(PlayerUI.childCount>0)
            {
                if (onInteractTrigger)
                { UiInteract.transform.position = Camera.main.WorldToScreenPoint(Player_tr.position + PlayerUiOffset); }
            }

        }

        public void ShowPlayerInteract()
        {
           UiInteract = Instantiate(PrefabPlayerUi, PlayerUI.transform);
            onInteractTrigger = true;
        }
        public void UnshowPlayerInteract()
        {
            foreach(Transform child in PlayerUI.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
           // onInteractTrigger = false;
        }

       

    }
}

   
