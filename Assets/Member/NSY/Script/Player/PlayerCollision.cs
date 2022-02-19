using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.Manager;
using Game.NPC;
using DM.Building;

namespace NSY.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] List<IInteractable> interacts = new List<IInteractable>();
        IInteractable closestObj;
        public GameObject collisionUI;

        [SerializeField]
        PlayerController playerController;

        public void OnTriggerEnter(Collider other)
        {
            print("OnTriggerEnter");
            
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interacts.Add(interactable);
            }

            //InteractObject interactObject = other.GetComponent<InteractObject>();
            //if (interactObject)
            //{
            //    interactObject.DropItems();
            //    print("spawn" + other.name);
            //    return;
            //}
            //ItemObject io = other.GetComponent<ItemObject>();
            //if (io)
            //{
            //    io.AddItem();
            //    return;
            //}
            //BuildingObject buildingObject = other.GetComponent<BuildingObject>();
            //if (buildingObject)
            //{
            //    PlayerData.AddValue(buildingObject.BuildID(), (int)BuildingBehaviorEnum.Interact, PlayerData.BuildBuildingData);

            //    return;
            //}
            ////////초반 튜토리얼 오브젝트와 충돌 판정
            if (other.CompareTag("FristPost"))
            {
                Debug.Log("첫 번째 표지판 부딪히고 유아이 띄우셈");
                EventManager._Instace.StartFirstPost();

            }
            if (other.CompareTag("FristTree"))
            {
                Debug.Log("첫 번째 나무 부딪히고 사과 떨어짐");
                EventManager._Instace.StartFirstTree();

            }
            //과일나무랑 만남
            if (other.CompareTag("FruitTree"))
            {

                //이벤트 함수 적어놀예정
                EventManager._Instace.PlayerActiveFruitTree(state);
                Debug.Log("열매 떨어져!");
            }
        }
        public void OnTriggerExit(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interacts.Remove(interactable);
            }

            if (other.CompareTag("NPC"))
            {
                triggerObjs = false;
                triggerObj = null;
                //종료 함수

            }
            if (other.CompareTag("FristTree"))
            {
                Debug.Log("나무 이벤트 끝");
                EventManager._Instace.EndFirstTree();
            }
            if (other.CompareTag("FristPost"))
            {
                Debug.Log("표지판 이벤트 끝");
                EventManager._Instace.EndFirstPost();
            }
        }
        public void Update()
        {
            LightClosestObj();
        }
        public void LightClosestObj()
        {
            if (interacts.Count == 0)
            {
                collisionUI.SetActive(false);
                return;
            }
            collisionUI.SetActive(true);

            DistChect();
            closestObj.CanInteract(playerController.gameObject);

            Vector3 uiPos = new Vector3(closestObj.ReturnTF().position.x, closestObj.ReturnTF().position.y + 4, closestObj.ReturnTF().position.z);
            collisionUI.transform.position = Camera.main.WorldToScreenPoint(uiPos);
        }
        public void DistChect()
        {
            float shortestDist = 1000000;

            foreach (var item in interacts)
            {
                float dist = Vector3.Distance(transform.position, item.ReturnTF().position);
                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    closestObj = item;
                }
            }

        }







        
        public GameObject triggerObj;
        private bool trigger;
        private bool triggerObjs;
        //열매 상태
        FrutStateManager state;

        //private void Update()
        //{
        //    //상호작용하면 불러올 함수들
        //    if (triggerObjs)
        //    {
               
        //        if (playerController.playerinput.interectObj == true)
        //        {
        //            Debug.Log("오브젝트와 충돌됐고 F키를 눌렀다.");
                    
        //        }

        //    }
        //    else
        //    {
        //      //  SuperManager.Instance.uimanager.FoodBoxUi.SetActive(false);
        //    }
        //}
    }

}
