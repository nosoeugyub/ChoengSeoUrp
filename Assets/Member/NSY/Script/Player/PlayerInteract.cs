using System.Collections.Generic;
using TT.BuildSystem;
using UnityEngine;
using UnityEngine.UI;

namespace NSY.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] List<IInteractble> interacts = new List<IInteractble>();//상호작용 범위 내 있는 IInteractable오브젝트 리스트
        //[SerializeField] Dictionary<IInteractable, T> interactss= new Dictionary<IInteractable>();//상호작용 범위 내 있는 IInteractable오브젝트 리스트
        [SerializeField] Button[] buildingButtons;
        //IInteractable closestObj;//가장 가까운 친구
        public GameObject interactUI;//띄울 UI
        public Text interactUiText;//띄울 UI

        [SerializeField] Item handItem;

        [SerializeField] SpriteRenderer handItemObj;

        [SerializeField] PlayerAnimator playerAnimator;

        [SerializeField] Item[] testToolItems;

        RaycastHit hit;
        Ray ray;
        IInteractble nowInteractable;
        bool canInteract = false;
        int layerMask;   // Player 레이어만 충돌 체크함

        public bool isAnimating = false;

        ////열매 상태
        //FrutStateManager state;

        private void Awake()
        {
            layerMask = 1 << LayerMask.NameToLayer("Interactable");
            //마우스 상호작용 오브젝트는 Interactable 이라는 레이어를 가지고 있어야 합니다.
        }

        public void OnTriggerEnter(Collider other)
        {
            ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
            IInteractble interactable = other.GetComponent<IInteractble>();
            if (interactable != null)
            {
                //Debug.Log("interact true");
                canInteract = true;
                interacts.Add(interactable);
            }
        }
        private void InvokeInteract(IInteractble interactable)
        {
            ICollectable collectable = interactable.ReturnTF().GetComponent<ICollectable>();
            if (collectable != null)
            {
                collectable.Collect();
                return;
            }
            ITalkable talkable = interactable.ReturnTF().GetComponent<ITalkable>();
            if (talkable != null)
            {
                talkable.Talk(handItem);
                return;
            }
            IEventable eventable = interactable.ReturnTF().GetComponent<IEventable>();
            if (eventable != null)
            {
                eventable.EtcEvent(handItem);
                return;
            }

            if (!handItem) return;

            switch (handItem.OutItemType)
            {
                case OutItemType.Tool://손에 도구를 들고 있으면
                    ISpeechBubbleCollectable bubbleCollectable = interactable.ReturnTF().GetComponent<ISpeechBubbleCollectable>();
                    if (bubbleCollectable != null)
                    {
                        if (!bubbleCollectable.CheckBubble(handItem, playerAnimator.animator))
                        {
                            isAnimating = false;
                        }
                        else
                        {
                            isAnimating = true;
                            return;
                        }
                    }
                    IMineable mineable = interactable.ReturnTF().GetComponent<IMineable>();
                    if (mineable != null)
                    {
                        if (!mineable.Mine(handItem, playerAnimator.animator))
                        {
                            isAnimating = false;
                        }
                        else
                        {
                            isAnimating = true;
                            return;
                        }
                    }
                    BuildingBlock buildAreaObject = interactable.ReturnTF().GetComponent<BuildingBlock>();
                    //IBuildable buildable = interactable.ReturnTF().GetComponent<IBuildable>();
                    if (buildAreaObject != null)
                    {
                        isAnimating = false;
                        buildAreaObject.OnBuildMode(buildingButtons, interactUI);
                        return;
                    }
                    IBuildable buildMat = interactable.ReturnTF().GetComponent<IBuildable>();
                    //IBuildable buildable = interactable.ReturnTF().GetComponent<IBuildable>();
                    if (buildMat != null)
                    {
                        isAnimating = false;
                        buildMat.Demolish();
                        return;
                    }
                    break;
            }
            isAnimating = false;


            ItemObject itemObject = interactable.ReturnTF().GetComponent<ItemObject>();
            if (itemObject != null)
            {
                itemObject.Interact();
                return;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
            IInteractble interactable = other.GetComponent<IInteractble>();
            if (interactable != null)
            {
                Debug.Log("interact false");
                canInteract = false;
                interacts.Remove(interactable);
            }
        }


        private void Update()
        {
            ///////////test Input/////////
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetHandItem(testToolItems[0]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetHandItem(testToolItems[1]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetHandItem(testToolItems[2]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetHandItem(testToolItems[3]);
            }
            ///////////test Input/////////
            ///

            if (!canInteract || IsAnimating())
            {
                foreach (var button in buildingButtons)
                {
                    button.gameObject.SetActive(false);
                }
                interactUI.SetActive(false);
                return;
            }


            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

            if (Physics.Raycast(ray, out hit, 10000, layerMask))
            {
                //print(hit.collider.name);
                nowInteractable = hit.collider.GetComponent<IInteractble>();
                if (nowInteractable != null && IsInteracted(nowInteractable))// 클릭한 옵젝이 닿은 옵젝 리스트에 있다면 통과
                {
                    interactUI.SetActive(true);
                    interactUiText.text = nowInteractable.CanInteract();
                    Vector3 uiPos = new Vector3(nowInteractable.ReturnTF().position.x, nowInteractable.ReturnTF().position.y + 2, nowInteractable.ReturnTF().position.z);
                    interactUI.transform.position = Camera.main.WorldToScreenPoint(uiPos);
                }
            }
            else
            {
                interactUI.SetActive(false);
            }



            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit, 10000, layerMask))
                {
                    nowInteractable = hit.collider.GetComponent<IInteractble>();
                    if (nowInteractable != null && IsInteracted(nowInteractable))
                    {
                        Debug.Log("상호작용한 물체: " + hit.collider.name);
                        InvokeInteract(nowInteractable);
                    }
                }
            }
        }

        public bool IsAnimating()
        {
            return isAnimating;
        }

        public bool IsInteracted(IInteractble it)
        {
            return interacts.Contains(it);
        }

        public void SetHandItem(Item item)
        {
            handItem = item;
            handItemObj.sprite = handItem.ItemSprite;
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
