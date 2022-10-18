using DM.Building;
using DM.Event;
using DM.NPC;
using NSY.Manager;
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

        public RectTransform interactUI;//띄울 UI
        public RectTransform buildinginteractUi;//띄울 UI
        public RectTransform introduceUi;//띄울 UI
        public RectTransform introduceUituto;//띄울 UI
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

        [SerializeField] Shader GlowColor;

        Camera mainCam;

        private void Awake()
        {
            layerMask = 1 << LayerMask.NameToLayer("Interactable");
            cursorManager = FindObjectOfType<CursorManager>();
            buildingManager = FindObjectOfType<BuildingManager>();
            eventContainer = FindObjectOfType<EventContainer>();
            mainCam = Camera.main;
        }
        private void Start()
        {
            canInteractCount = 0;
            PlayerInput.OnPressFDown = InvokeInteractClosestObj;
            DIalogEventManager.EventActions[(int)EventEnum.OnFollowPlayer] = NPCIntroduceSetting;
        }
        private void Update()
        {
            if (!canInteract) return;
            LightClosestObj();
            InteractWithObjects();
        }

        public void SetCanInteract(bool _canInteract)
        {
            if (_canInteract)
            {
                canInteractCount--;

                if (canInteractCount <= 0)
                {
                    canInteract = _canInteract;
                    //Debug.Log("SetInteract true  " + closestObj);
                }
            }
            else
            {
                canInteractCount++;
                canInteract = _canInteract;
                //Debug.Log("SetInteract false  " + closestObj);
            }
        }

        public bool SetNpc(HouseNpc npc)
        {
            if (npc == null)
            {
                followNpc = npc;
                return true;
            }

            //if (followNpc == null)
            {
                followNpc = npc;
                return true;
            }
            //else
            //{
            //    return false;
            //}
        }

        public void SetIsAnimation(bool isTrue)
        {
            if (isTrue)
            {
                if (!isAnimating)
                {
                    SetCanInteract(false);
                    eventContainer.RaiseEvent(GameEventType.playerMoveOffEvent);// playerMoveOffEvent.Raise();
                }
            }
            else
            {
                if (isAnimating)
                {
                    SetCanInteract(true);
                    eventContainer.RaiseEvent(GameEventType.playerMoveOnEvent); // playerMoveOnEvent.Raise();
                }
            }
            isAnimating = isTrue;
        }

        private void NPCIntroduceSetting()
        {
            followNpc.SetIsFollowPlayer(true);
            introduceUi.gameObject.SetActive(true);

            DIalogEventManager.EventAction -= DIalogEventManager.EventActions[(int)EventEnum.OnFollowPlayer];

        }
        public void IntroduceCancel()
        {
            followNpc.SetIsFollowPlayer(false);
            introduceUi.gameObject.SetActive(false);
            SetNpc(null);
            DebugText.Instance.SetText("소개를 중단했습니다.");
        }
        public void EndIntroduce()
        {
            followNpc.SetIsFollowPlayer(false);
            introduceUituto.gameObject.SetActive(false);
            introduceUi.gameObject.SetActive(false);
            SetNpc(null);

        }

        private void InvokeInteract(Interactable interactable)
        {
            if (!interactable) return;

            CollectObject collectObj = interactable.transform.GetComponent<CollectObject>();
            if (collectObj != null)
            {
                Debug.Log(collectObj.item.ItemName);
                if (collectObj.Collect(playerAnimator.animator)) //콜렉트에서 애니 발생함
                    SetIsAnimation(true);
                return;
            }

            NPC talkable = interactable.transform.GetComponent<NPC>();
            if (talkable != null)
            {
                if (!followNpc || !followNpc.IsFollowPlayer())
                {
                    SetNpc(interactable.transform.GetComponent<HouseNpc>());
                    talkable.Talk();
                    return;
                }
            }

            TeleportObject teleportable = interactable.transform.GetComponent<TeleportObject>();
            if (teleportable != null)
            {
                teleportable.Teleport(this.transform);
                return;
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
                    Debug.Log(mineable.item.ItemName);
                    SetIsAnimation(true);
                    return;
                }
            }
            BuildingBlock buildAreaObject = interactable.transform.GetComponent<BuildingBlock>();
            if (buildAreaObject != null)
            {
                SetIsAnimation(false);

                if (followNpc && followNpc.IsFollowPlayer())
                {
                    followNpc.FindLikeHouse(buildAreaObject);
                    EndIntroduce();
                }
                else
                {
                    print(buildAreaObject.name);
                    buildinginteractUi.gameObject.SetActive(false);
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

            buildinginteractUi.gameObject.SetActive(false);
            //StartCoroutine(cursorManager.SetCursor((int)CursorType.Normal));

            if (Physics.Raycast(ray, out hit, 20, layerMask2.value) && !buildingManager.isBuildMode)
            {
                nowInteractable = hit.collider.GetComponent<Interactable>();
                if (nowInteractable != null && IsInteracted(nowInteractable))// 클릭한 옵젝이 닿은 옵젝 리스트에 있다면 통과ds
                {
                    if (nowInteractable.GetComponent<BuildingBlock>() && !IsPointerOverUIObject())
                    {
                        //StartCoroutine(cursorManager.SetCursor(nowInteractable.CanInteract()));//건축 외에는 변하지 않음
                        buildinginteractUi.gameObject.SetActive(true);
                        Vector3 uiPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 40, Input.mousePosition.z);

                        RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRectTr, uiPos, uiCamera, out screenPoint);
                        buildinginteractUi.localPosition = screenPoint;
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
                interactUI.gameObject.SetActive(false);
            }

            if (interacts.Count <= 1) return;

            DistCheck();

            if (closestObj)
            {
                closestObj.CanInteract();
                interactUI.gameObject.SetActive(true);
                Vector3 vector3 = mainCam.WorldToScreenPoint(closestObj.transform.position);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRectTr, vector3, uiCamera, out screenPoint);
                interactUI.localPosition = screenPoint;
            }
        }
        public void InvokeInteractClosestObj()
        {
            if (!canInteract && isAnimating) return;
            LightClosestObj();
            InvokeInteract(closestObj);
        }
        //거리 계산. 한 메서드가 하는 일 많음.
        public void DistCheck()
        {
            float shortestDist = 1000000;

            for (int i = 0; i < interacts.Count; i++)
            {
                if (interacts[i] == null)
                    interacts.Remove(interacts[i]);
                if (interacts[i].gameObject.layer != 9) continue;

                float dist = Vector3.Distance(transform.position, interacts[i].transform.position);
                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    closestObj = interacts[i];
                    //Debug.Log(string.Format("closestObj: {0} // i: {1} // length: {2}", closestObj,i, interacts.Count));
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
                //Debug.LogWarning("Remove: "+interactable.name);
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
        private void OnDisable()
        {
            interactUI.gameObject.SetActive(false);
        }
    }

}

//[SerializeField] Dictionary<IInteractable, T> interactss= new Dictionary<IInteractable>();//상호작용 범위 내 있는 IInteractable오브젝트 리스트
