using DM.Building;
using DM.NPC;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NSY.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] List<Interactable> interacts = new List<Interactable>();//상호작용 범위 내 있는 IInteractable오브젝트 리스트
        //IInteractable closestObj;//가장 가까운 친구

        CursorManager cursorManager;

        //public GameObject interactUI;//띄울 UI
        //public Text interactUiText;//띄울 UI
        public TextMeshProUGUI interactUiText2;

        [SerializeField] Item handItem;

        [SerializeField] SpriteRenderer handItemObj;

        [SerializeField] PlayerAnimator playerAnimator;

        [SerializeField] Item[] testToolItems;

        [SerializeField] HouseNpc followNpc;
        [SerializeField] Camera uiCamera;

        RaycastHit hit;
        Ray ray;
        Interactable nowInteractable;
        bool canInteract = false;
        int layerMask;   // Player 레이어만 충돌 체크함

        public RectTransform targetRectTr;
        public bool isAnimating = false;
        private Vector2 screenPoint;



        [SerializeField] Shader GlowColor;



        private void Awake()
        {
            layerMask = 1 << LayerMask.NameToLayer("Interactable");
            //마우스 상호작용 오브젝트는 Interactable 이라는 레이어를 가지고 있어야 합니다.
            cursorManager = FindObjectOfType<CursorManager>();
        }
        private void Update()
        {
            TestInputs();

            InteractWithObjects();
        }

        public void SetNpc(HouseNpc npc)
        {
            followNpc = npc;
        }

        public void SetIsAnimation(bool isTrue)
        {
            isAnimating = isTrue;
        }

        private void InvokeInteract(Interactable interactable)
        {
            CollectObject collectObj = interactable.transform.GetComponent<CollectObject>();
            if (collectObj != null)
            {
                collectObj.Collect(playerAnimator.animator);
                SetIsAnimation(true);
                return;
            }
            NPC talkable = interactable.transform.GetComponent<NPC>();
            if (talkable != null)
            {
                talkable.Talk(handItem);
                return;
            }
            //IEventable eventable = interactable.transform.GetComponent<IEventable>();
            //if (eventable != null)
            //{
            //    eventable.EtcEvent(handItem);
            //    return;
            //}

            if (!handItem) return;

            switch (handItem.OutItemType)
            {
                case OutItemType.Tool://손에 도구를 들고 있으면
                    MagnifyObject bubbleCollectable = interactable.transform.GetComponent<MagnifyObject>();
                    if (bubbleCollectable != null)
                    {
                        if (!bubbleCollectable.CheckBubble(handItem, playerAnimator.animator))
                        {
                            SetIsAnimation(false);
                        }
                        else
                        {
                            SetIsAnimation(true);
                            return;
                        }
                    }
                    MineObject mineable = interactable.transform.GetComponent<MineObject>();
                    if (mineable != null)
                    {
                        if (!mineable.Mine(handItem, playerAnimator.animator))
                        {
                            SetIsAnimation(false);
                        }
                        else
                        {
                            SetIsAnimation(true);
                            return;
                        }
                    }
                    BuildingBlock buildAreaObject = interactable.transform.GetComponent<BuildingBlock>();
                    if (buildAreaObject != null)
                    {
                        SetIsAnimation(false);

                        if (followNpc)
                        {
                            followNpc.FindLikeHouse(buildAreaObject);
                        }
                        else
                        {
                            print(buildAreaObject.name);
                            buildAreaObject.OnBuildMode();
                        }
                        return;
                    }
                    break;
            }
            SetIsAnimation(false);


            ItemObject itemObject = interactable.transform.GetComponent<ItemObject>();
            if (itemObject != null)
            {
                itemObject.Interact();
                return;
            }
        }

        private void InteractWithObjects()
        {
            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 mousepos = Input.mousePosition;
            mousepos.z = 20;
            Vector3 nordir = (Camera.main.ScreenToWorldPoint(mousepos) - Camera.main.transform.position).normalized;
            ray = new Ray(Camera.main.transform.position + nordir * 10, nordir);
            Debug.DrawRay(ray.origin, ray.direction * 20, Color.blue, 0.3f);
            if (nowInteractable)
                nowInteractable.EndInteract();
            if (Physics.Raycast(ray, out hit, 20, layerMask) && !BuildingBlock.isBuildMode)
            {
                //print(hit.collider.name);
                nowInteractable = hit.collider.GetComponent<Interactable>();
                if (nowInteractable != null && IsInteracted(nowInteractable))// 클릭한 옵젝이 닿은 옵젝 리스트에 있다면 통과ds
                {
                    StartCoroutine(cursorManager.SetCursor(nowInteractable.CanInteract()));
                    Vector3 uiPos = new Vector3(nowInteractable.transform.position.x, nowInteractable.transform.position.y + 2, nowInteractable.transform.position.z);
                    //형광 셰이더로 변환....
                  
                    if(!nowInteractable.GetComponent<BuildingBlock>())
                    {
                        nowInteractable.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = GlowColor;
                    }
                  
                }
                else
                    StartCoroutine(cursorManager.SetCursor((int)CursorType.Normal));
            }


            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 10000, layerMask))
                {
                    nowInteractable = hit.collider.GetComponent<Interactable>();
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

        public bool IsInteracted(Interactable it)
        {
            return interacts.Contains(it);
        }

        public void SetHandItem(Item item)
        {
            handItem = item;
            handItemObj.sprite = handItem.ItemSprite;
            //애니메이션 변경
        }
        private void TestInputs()
        {
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
        }

        public void OnTriggerEnter(Collider other)
        {
            ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);

            Interactable interactable = other.GetComponent<Interactable>();
            if (interactable != null)
            {
                //canInteract = true;
                interacts.Add(interactable);
            }
        }
        public void OnTriggerExit(Collider other)
        {
            ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);

            Interactable interactable = other.GetComponent<Interactable>();
            if (interactable != null)
            {
                interacts.Remove(interactable);
                EndInteract(interactable);
            }
        }

        private void EndInteract(Interactable interactable)
        {
            if (interactable)
            {
                BuildingBlock buildAreaObject = interactable.transform.GetComponent<BuildingBlock>();
                if (buildAreaObject)
                    buildAreaObject.EndInteract_();
            }
        }





        /*
          //interactUI.SetActive(true);


                    //interactUiText2.text = nowInteractable.CanInteract();

                    //interactUI.transform.position = uiCamera.WorldToScreenPoint(uiPos);

                    //var position = uiCamera.WorldToScreenPoint(uiPos);
                    //position.z = (interactUI.transform.position - uiCamera.transform.position).magnitude;
                    //interactUI.transform.position = Camera.main.ScreenToWorldPoint(position);

                    //RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRectTr, Input.mousePosition, uiCamera, out screenPoint);
                    //interactUI.GetComponent<RectTransform>().localPosition = screenPoint;

                    //interactUI.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, interactUI.transform.position);

        */
        ////가장 가까운 오브젝트 검출
        //public void LightClosestObj()
        //{
        //    if (interacts.Count == 0)
        //    {
        //        interactUI.SetActive(false);
        //        return;
        //    }
        //    interactUI.SetActive(true);

        //    DistChect();
        //    closestObj.CanInteract();

        //    Vector3 uiPos = new Vector3(closestObj.ReturnTF().position.x, closestObj.ReturnTF().position.y + 4, closestObj.ReturnTF().position.z);
        //    interactUI.transform.position = Camera.main.WorldToScreenPoint(uiPos);
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

//[SerializeField] Dictionary<IInteractable, T> interactss= new Dictionary<IInteractable>();//상호작용 범위 내 있는 IInteractable오브젝트 리스트
