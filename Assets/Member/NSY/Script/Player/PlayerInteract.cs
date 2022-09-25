using DM.Building;
using DM.Event;
using DM.NPC;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NSY.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] List<Interactable> interacts = new List<Interactable>();//상호작용 범위 내 있는 IInteractable오브젝트 리스트
        [SerializeField] Interactable closestObj;//가장 가까운 친구

        CursorManager cursorManager;
        BuildingManager buildingManager;
        EventContainer eventContainer;

        public GameObject interactUI;//띄울 UI
        public GameObject buildinginteractUi;//띄울 UI
        public TextMeshProUGUI interactUiText2;

        //[SerializeField] Item handItem;

        [SerializeField] SpriteRenderer handItemObj;

        [SerializeField] PlayerAnimator playerAnimator;

        [SerializeField] Item[] testToolItems;

        [SerializeField] HouseNpc followNpc;
        [SerializeField] Camera uiCamera;

        RaycastHit hit;
        Ray ray;
        Interactable nowInteractable;
        public bool canInteract = true;
        public int canInteractCount;
        [HideInInspector] public int layerMask;   // Player 레이어만 충돌 체크함
        [SerializeField] LayerMask layerMask2;   // Player 레이어만 충돌 체크함

        public RectTransform targetRectTr;
        public bool isAnimating = false;
        private Vector2 screenPoint;

        [SerializeField] GameEvent playerMoveOnEvent;
        [SerializeField] GameEvent playerMoveOffEvent;

        [SerializeField] Shader GlowColor;



        private void Awake()
        {
            layerMask = 1 << LayerMask.NameToLayer("Interactable");
            //마우스 상호작용 오브젝트는 Interactable 이라는 레이어를 가지고 있어야 합니다.
            canInteractCount = 0;
            cursorManager = FindObjectOfType<CursorManager>();
            buildingManager = FindObjectOfType<BuildingManager>();
        }
        private void Start()
        {
            PlayerInput.OnPressFDown = InvokeInteractClosestObj;
        }
        private void Update()
        {
            LightClosestObj();
            if (!canInteract) return;
            InteractWithObjects();
        }

        public void SetCanInteract(bool _canInteract)
        {
            if (_canInteract)
            {
                canInteractCount--;

                if (canInteractCount <= 0)
                {
                    Debug.Log("SetInteract true");
                    canInteract = _canInteract;
                }
            }
            else
            {
                canInteractCount++;
                Debug.Log("SetInteract false");
                canInteract = _canInteract;
            }
        }
        public bool SetNpc(HouseNpc npc)
        {
            if (npc == null)
            {
                followNpc = npc;
                return true;
            }

            if (followNpc == null)
            {
                followNpc = npc;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetIsAnimation(bool isTrue)
        {
            if (isTrue)
            {
                if (!isAnimating)
                {
                    SetCanInteract(false);
                    playerMoveOffEvent.Raise();
                }
            }
            else
            {
                if (isAnimating)
                {
                    SetCanInteract(true);
                    playerMoveOnEvent.Raise();
                }
            }
            isAnimating = isTrue;
        }

        private void InvokeInteract(Interactable interactable)
        {
            if (!interactable) return;
            CollectObject collectObj = interactable.transform.GetComponent<CollectObject>();
            if (collectObj != null)
            {
                Debug.Log(collectObj.item.ItemName);
                //if (SuperManager.Instance.inventoryManager.isGettingItem == false)
                {
                    if (collectObj.Collect(playerAnimator.animator)) //콜렉트에서 애니 발생함
                        SetIsAnimation(true);
                    return;
                }
                //collectObj.Collect(playerAnimator.animator); //콜렉트에서 애니 발생함
                //SetIsAnimation(true);
                //return;
            }

            NPC talkable = interactable.transform.GetComponent<NPC>();
            if (talkable != null)
            {
                talkable.Talk();
                return;
            }
            //IEventable eventable = interactable.transform.GetComponent<IEventable>();
            //if (eventable != null)
            //{
            //    eventable.EtcEvent(handItem);
            //    return;
            //}
            TeleportObject teleportable = interactable.transform.GetComponent<TeleportObject>();
            if (teleportable != null)
            {
                teleportable.Teleport(this.transform);
                return;
            }

            MagnifyObject bubbleCollectable = interactable.transform.GetComponent<MagnifyObject>();
            if (bubbleCollectable != null)
            {
                if (!bubbleCollectable.CheckBubble(playerAnimator.animator))
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
                if (!mineable.Mine(playerAnimator.animator))
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
                    buildinginteractUi.SetActive(false);
                    buildingManager.BuildModeOn(buildAreaObject);
                }
                return;
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
            Vector3 mousepos = Input.mousePosition;
            mousepos.z = 20;
            Vector3 nordir = (Camera.main.ScreenToWorldPoint(mousepos) - Camera.main.transform.position).normalized;
            ray = new Ray(Camera.main.transform.position + nordir * 10, nordir);
            Debug.DrawRay(ray.origin, ray.direction * 20, Color.blue, 0.3f);

            if (nowInteractable)
                nowInteractable.EndInteract();

            buildinginteractUi.SetActive(false);
            StartCoroutine(cursorManager.SetCursor((int)CursorType.Normal));

            if (Physics.Raycast(ray, out hit, 20, layerMask2.value) && !buildingManager.isBuildMode)
            {
                nowInteractable = hit.collider.GetComponent<Interactable>();
                if (nowInteractable != null && IsInteracted(nowInteractable))// 클릭한 옵젝이 닿은 옵젝 리스트에 있다면 통과ds
                {

                    if (nowInteractable.GetComponent<BuildingBlock>() && !IsPointerOverUIObject())
                    {
                        StartCoroutine(cursorManager.SetCursor(nowInteractable.CanInteract()));//건축 외에는 변하지 않음
                        buildinginteractUi.SetActive(true);
                        Vector3 uiPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 40, Input.mousePosition.z);

                        RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRectTr, uiPos, uiCamera, out screenPoint);
                        buildinginteractUi.GetComponent<RectTransform>().localPosition = screenPoint;
                    }
                }
                else
                {
                }
            }


            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 10000, layerMask2.value))
                {
                    nowInteractable = hit.collider.GetComponent<Interactable>();
                    if (nowInteractable != null && IsInteracted(nowInteractable) && !IsPointerOverUIObject())
                    {
                        InvokeInteract(nowInteractable);
                    }
                }
            }
        }

        ////가장 가까운 오브젝트 검출
        public void LightClosestObj()
        {
            if (closestObj)
            {
                closestObj.EndInteract();
                closestObj = null;
                interactUI.SetActive(false);
            }

            if (interacts.Count <= 1) return;

            DistCheck();

            if (closestObj)
            {
                closestObj.CanInteract();
                interactUI.SetActive(true);
                Vector3 vector3 = Camera.main.WorldToScreenPoint(closestObj.transform.position);

                RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRectTr, vector3, uiCamera, out screenPoint);
                interactUI.GetComponent<RectTransform>().localPosition = screenPoint;
                //interactUI.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, interactUI.transform.position);

                //ChangeLightShader(closestObj);
            }
        }
        public void InvokeInteractClosestObj()
        {
            if (!canInteract) return;
            Debug.Log(canInteract);
            InvokeInteract(closestObj);
        }
        //거리 계산. 한 메서드가 하는 일 많음.
        public void DistCheck()
        {
            float shortestDist = 1000000;

            foreach (var item in interacts)
            {
                if (item == null) interacts.Remove(item);
                if (item.gameObject.layer != 9) continue;
                float dist = Vector3.Distance(transform.position, item.transform.position);
                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    closestObj = item;
                }
            }
        }
        private void ChangeLightShader(Interactable interactableobj)
        {
            if (!interactableobj.GetComponent<BuildingBlock>())
            {
                interactableobj.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = GlowColor;
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
        public bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        private void EndInteract(Interactable interactable)
        {
            if (interactable)
            {
                //BuildingBlock buildAreaObject = interactable.transform.GetComponent<BuildingBlock>();
                //if (buildAreaObject)
                //    buildAreaObject.EndInteract_();
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

    }

}

//[SerializeField] Dictionary<IInteractable, T> interactss= new Dictionary<IInteractable>();//상호작용 범위 내 있는 IInteractable오브젝트 리스트
