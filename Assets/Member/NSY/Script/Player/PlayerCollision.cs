using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSY.Player
{
    public class PlayerCollision : MonoBehaviour
    {
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
                if (playerController.playerinput.GetItem == true)
                {
                    Debug.Log("음식상자가 충돌됐고 E키를 눌렀다. + 애니메이션 재생");
                    // 튜툐리얼 이벤트 함수 호출
                }
                else
                {

                }
               
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Food Box"))
            {
                triggerObjs = true;
                triggerObj = other.gameObject;
            }

            
        }
        private void OnTriggerStay(Collider other)
        {
            
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Food Box"))
            {
                triggerObjs = false;
                triggerObj = null;
            }
        }
    }

}
