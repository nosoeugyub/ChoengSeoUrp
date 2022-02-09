﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.Manager;

namespace NSY.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        //

        [SerializeField]
        PlayerController playerController;

        private GameObject triggeringNpc;
        private GameObject triggerObj;
        private bool trigger;
        private bool triggerObjs;

        private void Update()
        {
            //상호작용하면 불러올 함수들
            if (triggerObjs)
            {
               // SuperManager.Instance.uimanager.FoodBoxUi.SetActive(true);
                if (playerController.playerinput.interectObj == true)
                {
                    Debug.Log("오브젝트와 충돌됐고 F키를 눌렀다.");
                    
                }

            }
            else
            {
              //  SuperManager.Instance.uimanager.FoodBoxUi.SetActive(false);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("NPC"))
            {
                triggerObjs = true;
                triggerObj = other.gameObject;
                // 이벤트 함수 호출
               

            }

            if (other.CompareTag("InteractObj"))
            {
                other.GetComponent<InteractObject>().DropItems();
                print("spawn" + other.name);

            }

        }
        private void OnTriggerStay(Collider other)
        {
            
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("NPC"))
            {
                triggerObjs = false;
                triggerObj = null;
                //종료 함수
              
            }
        }
    }

}
