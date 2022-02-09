using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.Manager;
using Game.NPC;

namespace NSY.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        //

        [SerializeField]
        PlayerController playerController;

        
        public GameObject triggerObj;
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
            print("OnTriggerEnter");
            MainNpc npc = other.GetComponent<MainNpc>();
            if (npc)
            {
                triggerObjs = true;
                triggerObj = other.gameObject;
                // 이벤트 함수 호출
                npc.PlayDialog();

            }
            InteractObject interactObject = other.GetComponent<InteractObject>();
            if (interactObject)
            {
                interactObject.DropItems();
                print("spawn" + other.name);

            }
            if (other.CompareTag("FristTree"))
            {
                Debug.Log("첫 번째 나무 부딪히고 사과 떨어짐");
                EventManager._Instace.StartFirstTree();

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
            if (other.CompareTag("FristTree"))
            {
                Debug.Log("나무 이벤트 끝");
            }
        }
    }

}
