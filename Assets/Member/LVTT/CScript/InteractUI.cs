using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TT.ObjINTERACT
{
    //[System.Serializable]
    //public class InteractAction
    //{
    //    public string ActionName;
    //    public GameObject ActionUi;
    //}
    public class InteractUI : MonoBehaviour
    {
        [SerializeField]public Transform Player_tr;
        //[SerializeField]public Transform Item_tr;
        [SerializeField] Transform PlayerUI;
        [SerializeField] Transform ItemUI;
        [Header("InteractTriggerUI")]
        [SerializeField] GameObject PrefabUi;
        [SerializeField] Vector3 TriggerUIoffset;
        GameObject UiInteract=null;
        GameObject UiLocate = null;

        //[Header("ActionList")]
        //public GameObject[] InteractAction;
        //[SerializeField] Vector3 ActionMenuoffset;
        //List<Vector3> SpawnPos;
        public bool onInteractTrigger=false;
        public bool onActionMenu = false;

        void Start()
        {
            // Player_tr = transform;
           

        }

        
        void Update()
        {
            if(PlayerUI.childCount>0)
            {
                if (onInteractTrigger)
                { UiInteract.transform.position = Camera.main.WorldToScreenPoint(Player_tr.position + TriggerUIoffset); }

                //if(onActionMenu)
                //{
                //   // UiInteract.transform.position = Camera.main.WorldToScreenPoint(Player_tr.position + TriggerUIoffset);
                //}
            }

           
           
        }

        public void ShowInteract()
        {
           UiInteract = Instantiate(PrefabUi, PlayerUI.transform);
            onInteractTrigger = true;
        }

        //public void SpawnActionItem(int ActionNum,int ActionCount)
        //{

        //        UiInteract = Instantiate(InteractAction[ActionNum], PlayerUI.transform);
        //        UiInteract.transform.position = Camera.main.WorldToScreenPoint(Player_tr.position +(( ActionMenuoffset * (ActionCount+1))));
        //}    

        public void UnshowInteract()
        {
            foreach(Transform child in PlayerUI.transform)
            {
                GameObject.Destroy(child.gameObject);
                onActionMenu = false;
            }
            onInteractTrigger = false;
        }


    }
}

   
