//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TT.Test;

//namespace TT.BuildSystem
//{
//    public class BuildingCollider : MonoBehaviour
//    {
//        UIOnOff TheUI;

//        void Start()
//        {
//            TheUI = FindObjectOfType<UIOnOff>();

//        }

        
//        void Update()
//        {

//        }


//        private void OnTriggerEnter(Collider col)
//        {
            
//            if (col.gameObject.tag == "Player")
//            {
//                TheUI.TurnOnUI(0);
                
//            }
//        }
//        void OnTriggerExit(Collider col)
//        {
//            if (col.gameObject.tag == "Player")
//            {

//                TheUI.TurnOffUI(0);
//            }
//        }

//    }
//}

