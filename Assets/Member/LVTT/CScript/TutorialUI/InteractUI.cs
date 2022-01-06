using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace TT.ObjINTERACT
{
    
    public class InteractUI : MonoBehaviour
    {
        [SerializeField]public Transform Player_tr;
        
       
        //상호작용할 수 있는 영역 들어갈 때 안내UI
        [Header("InteractGuidePlayerUI")]
        [SerializeField] Transform PlayerGuideUI;
        [SerializeField] GameObject PrefabPlayerGuideUi;
        [SerializeField] Vector3 PlayerGuideUiOffset;
        ////////////////////////////

        //Object 까가올 때 동지UI
        //[Header("InteractGuidePlayerUI")]
        //[SerializeField] Transform PlayerNoticeUI;
        //[SerializeField] GameObject PrefabPlayerNoticeUi;
        //[SerializeField] Vector3 PlayerNoticeUiOffset;
        ////////////////////////////

        GameObject UiInteract =null;
        public bool onInteractTrigger=false;
       
        void Start()
        {
           

        }

        
        void Update()
        {
            if(PlayerGuideUI.childCount>0)
            {
                if (onInteractTrigger)
                { UiInteract.transform.position = Camera.main.WorldToScreenPoint(Player_tr.position + PlayerGuideUiOffset); }
            }

        }

        public void ShowPlayerInteractGuide()
        {
           UiInteract = Instantiate(PrefabPlayerGuideUi, PlayerGuideUI.transform);
            onInteractTrigger = true;
        }
        public void UnshowPlayerInteractGuide()
        {
            foreach(Transform child in PlayerGuideUI.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        //public void ShowPlayerNotification()
        //{
        //    UiInteract = Instantiate(PrefabPlayerNoticeUi, PlayerNoticeUI.transform);
        //    
        //}
        //public void UnshowPlayerNotification()
        //{
        //    foreach (Transform child in PlayerNoticeUI.transform)
        //    {
        //        GameObject.Destroy(child.gameObject);
        //    }
        //}

    }
}

   
