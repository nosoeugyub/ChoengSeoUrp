using NSY.Manager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSY.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] List<IInteractable> interacts = new List<IInteractable>();//상호작용 범위 내 있는 IInteractable오브젝트 리스트
        //[SerializeField] Dictionary<IInteractable, T> interactss= new Dictionary<IInteractable>();//상호작용 범위 내 있는 IInteractable오브젝트 리스트

        //IInteractable closestObj;//가장 가까운 친구
        public GameObject interactUI;//띄울 UI
        public Text interactUiText;//띄울 UI

        [SerializeField]//임시
        Item handItem;

        [SerializeField]
        PlayerController playerController;

        RaycastHit hit;
        Ray ray;
        IInteractable nowInteractable;
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
                interacts.Add(interactable);
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

        private void InvokeInteract(IInteractable interactable)
        {
            switch (handItem.OutItemType)
            {
                case OutItemType.Tool://손에 도구를 들고 있으면
                    IMineable mineable = interactable.ReturnTF().GetComponent<IMineable>();
                    if (mineable != null)
                    {
                        mineable.Mine(handItem);
                    }
                    break;
                case OutItemType.Food://음식 들고있으면
                    IEatable eatable = interactable.ReturnTF().GetComponent<IEatable>();
                    if (eatable != null)
                    {
                        eatable.Eat();
                    }
                    break;
                case OutItemType.Etc://이벤트 들고있으면
                    IEventable eventable = interactable.ReturnTF().GetComponent<IEventable>();
                    if (eventable != null)
                    {
                        eventable.EtcEvent();
                    }
                    //기타 아이템을 NPC에 전달하는 기능이 있다면 여기 추가
                    break;

                default://어느 타입도 아닌 맨손>> 인벤에 넣을 수 있는 아이템이라면 인벤에 넣기. 대화도 걸기
                    ICollectable collectable = interactable.ReturnTF().GetComponent<ICollectable>();
                    if (collectable != null)
                    {
                        collectable.Collect();
                        break;
                    }
                    ITalkable talkable = interactable.ReturnTF().GetComponent<ITalkable>();
                    if (talkable != null)
                    {
                        talkable.Talk();
                        break;
                    }
                    ItemObject item= interactable.ReturnTF().GetComponent<ItemObject>();
                    //item.
                    break;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log("interact false");
                canInteract = false;
                interacts.Remove(interactable);
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
                interactUI.SetActive(false);
                return;
            }
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

            if (Physics.Raycast(ray, out hit, 10000, layerMask))
            {
                //print(hit.collider.name);
                nowInteractable = hit.collider.GetComponent<IInteractable>();
                if (nowInteractable != null && IsInteracted(nowInteractable))// 클릭한 옵젝이 닿은 옵젝 리스트에 있다면 통과
                {
                    interactUI.SetActive(true);
                    interactUiText.text = nowInteractable.CanInteract();
                    Vector3 uiPos = new Vector3(nowInteractable.ReturnTF().position.x, nowInteractable.ReturnTF().position.y + 2, nowInteractable.ReturnTF().position.z);
                    interactUI.transform.position = Camera.main.WorldToScreenPoint(uiPos);
                }
            }
            else
                interactUI.SetActive(false);

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 10000, layerMask))
                {
                    nowInteractable = hit.collider.GetComponent<IInteractable>();
                    if (nowInteractable != null && IsInteracted(nowInteractable))
                    {
                        print(hit.collider.name);
                        InvokeInteract(nowInteractable);
                        //interactable.Interact();
                    }
                }
            }
        }
        public bool IsInteracted(IInteractable it)
        {
            return interacts.Contains(it);
        }

        public void SetHandItem(Item item)
        {
            handItem = item;
            //애니메이션 변경
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
