using NSY.Manager;
using System.Collections.Generic;
using UnityEngine;

namespace NSY.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        //[SerializeField] List<IInteractable> interacts = new List<IInteractable>();//상호작용 범위 내 있는 IInteractable오브젝트 리스트
        //IInteractable closestObj;//가장 가까운 친구
        public GameObject collisionUI;//띄울 UI

        [SerializeField]
        PlayerController playerController;

        RaycastHit hit;
        Ray ray;
        IInteractable interactable;
        bool canInteract = false;
        int layerMask;   // Player 레이어만 충돌 체크함

        //열매 상태
        FrutStateManager state;

        private void Awake()
        {
            layerMask = 1 << LayerMask.NameToLayer("Interactable");
            //마우스 상호작용 오브젝트는 Interactable 이라는 레이어를 가지고 있어야 합니다.
        }
        public void OnTriggerEnter(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log("interact true");
                canInteract = true;
            }
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
                EventManager._Instace.PlayerActiveFruitTree();
                Debug.Log("열매 떨어져!");
            }
        }
        public void OnTriggerExit(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log("interact false");
                canInteract = false;
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
        private void Update()
        {
            if (!canInteract)
            {
                collisionUI.SetActive(false);
                return;
            }
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

            if (Physics.Raycast(ray, out hit, 10000, layerMask))
            {
                print(hit.collider.name);
                interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)//그 옵젝이 상호작용 가능하면? 추가해야함
                {
                    collisionUI.SetActive(true);
                    interactable.CanInteract();
                    Vector3 uiPos = new Vector3(interactable.ReturnTF().position.x, interactable.ReturnTF().position.y + 2, interactable.ReturnTF().position.z);
                    collisionUI.transform.position = Camera.main.WorldToScreenPoint(uiPos);
                }
            }
            else
                collisionUI.SetActive(false);


            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 10000, layerMask))
                {
                    print(hit.collider.name);
                    interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactable.Interact();
                    }
                }
            }


        }
        ////가장 가까운 오브젝트 검출
        //public void LightClosestObj()
        //{
        //    if (interacts.Count == 0)
        //    {
        //        collisionUI.SetActive(false);
        //        return;
        //    }
        //    collisionUI.SetActive(true);

        //    DistChect();
        //    closestObj.CanInteract();

        //    Vector3 uiPos = new Vector3(closestObj.ReturnTF().position.x, closestObj.ReturnTF().position.y + 4, closestObj.ReturnTF().position.z);
        //    collisionUI.transform.position = Camera.main.WorldToScreenPoint(uiPos);
        //}
        ////거리 계산
        //public void DistChect()
        //{
        //    float shortestDist = 1000000;

        //    foreach (var item in interacts)
        //    {
        //        float dist = Vector3.Distance(transform.position, item.ReturnTF().position);
        //        if (dist < shortestDist)
        //        {
        //            shortestDist = dist;
        //            closestObj = item;
        //        }
        //    }
        //}
        
    }

}
