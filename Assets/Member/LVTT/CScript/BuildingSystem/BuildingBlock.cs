using DM.NPC;
using NSY.Iven;
using NSY.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BuildState { None, NotFinish, Finish }
public enum BuildMode { None, BuildHouseMode, }

namespace DM.Building
{
    public class BuildingBlock : Interactable
    {
        [SerializeField] private int buildingId;
        [SerializeField] private BuildMode CurBuildMode;
        [SerializeField] private HouseNpc livingCharacter;
        [SerializeField] private int seasonnum;

        [SerializeField] private Transform houseBuild;
        [SerializeField] private List<GameObject> BuildItemList;

        [SerializeField] BuildState buildState;

        [SerializeField] private float areaWidthsize;
        [SerializeField] private float areaHeightsize;

        [SerializeField] InItemType toolType;

        [SerializeField] Transform houseOwnerTransform;
        [SerializeField] Transform friendTransform;
        [SerializeField] Transform[] constructsign;

        private InventoryNSY inventory;
        private PlayerInput playerInput;

        [SerializeField] BuildingItemObj curInteractObj;
        private float BuildItemScaleVar = 0.015f;
        private float BuildItemRotationVar = 1.4f;
        private float BuildItemGap = 0.002f;

        [SerializeField] Transform CameraPos;

        SpecialHouse specialHouse;

        RaycastHit hit;
        Ray ray;
        int layerMask;   // Player 레이어만 충돌 체크함

        private Transform HouseBuild { get { return houseBuild; } set { houseBuild = value; } }
        private float AreaWidthsize { get { return areaWidthsize; } set { areaWidthsize = value; } }

        private float AreaHeightsize { get { return areaHeightsize; } set { areaHeightsize = value; } }

        public HouseNpc _livingCharacter { get { return livingCharacter; } set { livingCharacter = value; } }


        public int Seasonnum { get { return seasonnum; } set { seasonnum = value; } }
        public SpecialHouse SpecialHouse { get { return specialHouse; } set { specialHouse = value; } }
        public Transform HouseOwnerTransform { get { return houseOwnerTransform; } set { houseOwnerTransform = value; } }
        public Transform FriendTransform { get { return friendTransform; } set { friendTransform = value; } }
        public int BuildingID { get { return buildingId; } set { buildingId = value; } }

        //액션을 사용해보기
        public delegate void VoidDelegate(BuildingBlock buildingBlock);
        public delegate void CancelUIDelegate(bool ison);
        public CancelUIDelegate cancelUIDelegate;

        public GameObject sscam;
        private float _distance;
        private float _blendTime;


        private void Awake()
        {
            specialHouse = GetComponent<SpecialHouse>();
            playerInput = FindObjectOfType<PlayerInput>();
            inventory = FindObjectOfType<InventoryNSY>();
        }
        void Start()
        {
            layerMask = 1 << LayerMask.NameToLayer("Wall");
            ConstructSignsActive(!IsCompleteBuilding());
        }
        internal void InitItemDestroyCount()
        {
            foreach (GameObject item in BuildItemList)
            {
                item.GetComponent<BuildingItemObj>().InitDestroyCount();
            }
        }
        internal void SetDistanceWhitCam(float distance)
        {
            _distance = distance;
        }

        internal void SetBlentTime(float blendTime)
        {
            _blendTime = blendTime;
        }

        public void SetCancelUIAction(CancelUIDelegate action)
        {
            cancelUIDelegate = action;
        }
        public void BuildModeOnSetting()
        {
            StartCoroutine(BuildModeOnSettingDelay());
        }
        IEnumerator BuildModeOnSettingDelay()
        {
            inventory.InvenAllOnOff(false);
            ConstructSignsActive(false);
            GetComponent<BoxCollider>().enabled = false;
            Interact();
            yield return new WaitForSeconds(_blendTime);
            inventory.EnableCanBuildItem();
            SetBuildMode(BuildMode.BuildHouseMode);
        }
        public void BuildModeOffSetting(VoidDelegate addBuilding)
        {
            StartCoroutine(BuildModeOffSettingDelay(addBuilding));
        }
        IEnumerator BuildModeOffSettingDelay(VoidDelegate addBuilding)
        {
            inventory.InvenAllOnOff(false);
            inventory.SetCheckBuildItem(null);
            SetCurInteractObj(null);
            SetBuildMode(BuildMode.None);

            if (IsCompleteBuilding())
            {
                SetBuildingState(BuildState.Finish);
                addBuilding(this);
            }
            else
            {
                SetBuildingState(BuildState.NotFinish);
                ConstructSignsActive(true);
            }
            yield return new WaitForSeconds(_blendTime);
            GetComponent<BoxCollider>().enabled = true;
            inventory.InvenAllOnOff(true);
        }

        internal float CalculatePercent(dynamic dynamic, Type type)
        {
            if (BuildItemList.Count <= 0) return 0;
            int clear = 0;
            //bool canContinue = false;
            //dynamic 타입과 이외의 타입
            foreach (BuildingItemObj itemObj in GetBuildItemList())
            {
                //Type type1 = itemObj.GetAttribute().buildShape.GetType();
                //Type type2 = type.GetType(); // 이거 하면 안됨. type은 타입으로만 쓰기

                if (itemObj.GetAttribute().buildShape.GetType() == type)
                {
                    if (itemObj.GetAttribute().buildShape == dynamic)
                    {
                        clear++;
                        continue;
                    }
                }
                //canContinue = false;
                for (int i = 0; i < itemObj.GetAttribute().buildThema.Length; i++)
                {
                    if (itemObj.GetAttribute().buildThema[i].GetType() == type)
                    {
                        if (itemObj.GetAttribute().buildThema[i] == dynamic)
                        {
                            clear++;
                            //canContinue = true;
                            break;
                        }
                    }
                    
                }
            }
            float percent = (float)clear / BuildItemList.Count * 100;
            Debug.Log("percent: " + percent);
            return (int)percent;
        }

        public void ConstructSignsActive(bool isActive)
        {
            for (int i = 0; i < constructsign.Length; i++)
            {
                constructsign[i].gameObject.SetActive(isActive);
            }
        }

        public void SetCurInteractObj(BuildingItemObj buildingItemObj)
        {
            if (curInteractObj)
            {
                curInteractObj.ItemisSet = true;
                curInteractObj = buildingItemObj;
            }
            else
            {
                if (buildingItemObj)
                {
                    curInteractObj = buildingItemObj;
                    curInteractObj.ItemisSet = false;
                }
            }
        }

        private void Update()
        {
            if (CurBuildMode != BuildMode.BuildHouseMode) return;

            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 20, Color.blue, 0.3f);

                if (Physics.Raycast(ray, out hit, 25, layerMask) && !IsPointerOverUIObject())
                {
                    if (curInteractObj != null)//뭘 이미 들고 있다면?
                    {
                        SetBuildingItemObj();
                    }
                    else
                    {
                        inventory.SetCheckBuildItem(hit.collider.GetComponent<BuildingItemObj>().item);
                        inventory.InvenAllOnOff(false);
                        SetCurInteractObj(hit.collider.GetComponent<BuildingItemObj>());
                    }
                }
            }
            if (!curInteractObj) return;

            if (Input.GetMouseButtonDown(1))
            {
                if (curInteractObj.IsFirstDrop)
                {
                    BackToInventory();
                }
                else
                {
                    // 직관적이지 않은 부분 존재
                    if (curInteractObj.Demolish())
                    {
                        RemoveBuildItemToList(curInteractObj);
                        DeleteBuildingItemObjSorting(curInteractObj.gameObject);
                        Destroy(curInteractObj.gameObject);

                        inventory.InvenSlotResetCanBuildMode(); //빌딩가능모드로 인벤 리셋
                    }
                }
            }
        }
        private void FixedUpdate()
        {
            if (!curInteractObj) return;
            //자재를 들고있을 때
            curInteractObj.CallUpdate(DistanceToNowBuildItemToNewSort());
            ScaleBuildItem();
            RotateBuildItem();
            FrontBackMoveBuildItem();
        }
        //UI 위에라면 레이 안쏨
        public bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        public void BackToInventory()
        {
            inventory.AddItem(curInteractObj.item, false);
            inventory.InvenSlotResetCanBuildMode(); //빌딩가능모드로 인벤 리셋

            RemoveBuildItemToList(curInteractObj);
            DeleteBuildingItemObjSorting(curInteractObj.gameObject);
            cancelUIDelegate(false);

            Destroy(curInteractObj.gameObject);
        }

        private void SetBuildingItemObj()//설치하기
        {
            inventory.InvenSlotResetCanBuildMode(); //빌딩가능모드로 인벤 리셋
            cancelUIDelegate(false);
            curInteractObj.PutDownBuildingItemObj(AreaWidthsize, AreaHeightsize);
            SetCurInteractObj(null);
        }

        public List<BuildingItemObj> GetBuildItemList()
        {
            List<BuildingItemObj> items = new List<BuildingItemObj>();
            //if (isBuildMode) return items; ?? 이거 왜있음.

            foreach (var item in BuildItemList)
            {
                items.Add(item.GetComponent<BuildingItemObj>());
            }

            return items;
        }
        public void AddBuildItemToList(GameObject Item)
        {
            BuildItemList.Add(Item);
        }
        public void RemoveBuildItemToList(BuildingItemObj Item)
        {
            if (specialHouse)
                specialHouse.CheckExist(curInteractObj, false);

            FindObjectOfType<EnvironmentManager>().ChangeCleanliness(-(Item.GetItem().CleanAmount + 1));
            BuildItemList.Remove(Item.gameObject);
        }

        public bool IsCompleteBuilding()//벽과 문이 있다면 건설 완료 처리
        {
            if (buildState == BuildState.Finish) return true;

            if (BuildItemList.Count > 0)
                return true;
            else return false;
        }

        public void SetBuildMode(BuildMode buildmode)
        {
            CurBuildMode = buildmode;
            //DebugText.Instance.SetText(string.Format("CurBuildMode: {0}", CurBuildMode.ToString()));
        }

        void ScaleBuildItem()
        {
            if (Input.GetKey(playerInput.scaleUpKey))
            {
                curInteractObj.SetBuildingItemScale(BuildItemScaleVar);
                PlayerData.AddValue(0, (int)BuildInputBehaviorEnum.ScaleUp, PlayerData.BuildInputData, (int)BuildInputBehaviorEnum.length);
            }
            else if (Input.GetKey(playerInput.scaleDownKey))
            {
                curInteractObj.SetBuildingItemScale(-BuildItemScaleVar);
                PlayerData.AddValue(0, (int)BuildInputBehaviorEnum.ScaleDown, PlayerData.BuildInputData, (int)BuildInputBehaviorEnum.length);
            }
        }

        void RotateBuildItem()
        {
            if (Input.GetKey(playerInput.rotateLeftKey))
            {
                curInteractObj.SetBuildItemRotation(+BuildItemRotationVar);
                PlayerData.AddValue(0, (int)BuildInputBehaviorEnum.RotationLeft, PlayerData.BuildInputData, (int)BuildInputBehaviorEnum.length);

            }
            else if (Input.GetKey(playerInput.rotateRightKey))
            {
                curInteractObj.SetBuildItemRotation(-BuildItemRotationVar);
                PlayerData.AddValue(0, (int)BuildInputBehaviorEnum.RotationRight, PlayerData.BuildInputData, (int)BuildInputBehaviorEnum.length);
            }
        }

        void FrontBackMoveBuildItem()
        {
            if (Input.GetKeyDown(playerInput.frontKey))
            {
                curInteractObj.SwitchBuildingItemObjZPos(true, BuildItemList, BuildItemGap);
                PlayerData.AddValue(0, (int)BuildInputBehaviorEnum.LayerUp, PlayerData.BuildInputData, (int)BuildInputBehaviorEnum.length);

            }
            else if (Input.GetKeyDown(playerInput.BackKey))
            {
                curInteractObj.SwitchBuildingItemObjZPos(false, BuildItemList, BuildItemGap);
                PlayerData.AddValue(0, (int)BuildInputBehaviorEnum.LayerDown, PlayerData.BuildInputData, (int)BuildInputBehaviorEnum.length);
            }
        }

        // 메서드 나누기 필요. + instantiate 부분 개선 필요
        public void BtnSpawnHouseBuildItem(Item spawnObj)
        {
            Vector3 spawnPos = HouseBuild.transform.position;

            MoveBackBuildItems();

            GameObject newPrefab = Instantiate(spawnObj.ItemPrefab, CalculateNewBuildItemSpawnPos(spawnPos), Quaternion.identity, HouseBuild.transform);
            newPrefab.transform.localRotation = Quaternion.Euler(0, 0, 0);

            SetCurInteractObj(newPrefab.GetComponent<BuildingItemObj>());

            if (specialHouse)
                specialHouse.CheckExist(curInteractObj, true);

            curInteractObj.name = spawnObj.name;
            curInteractObj.SetOrder(BuildItemList.Count);
            curInteractObj.SetAreaSize(AreaWidthsize, AreaHeightsize);
            curInteractObj.SetPivotPos(HouseBuild.position);

            AddBuildItemToList(newPrefab);
            FindObjectOfType<EnvironmentManager>().ChangeCleanliness(curInteractObj.GetItem().CleanAmount + 1);
            cancelUIDelegate(true);
        }

        private Vector3 CalculateNewBuildItemSpawnPos(Vector3 spawnPos)
        {
            //새로 설치할 건축자재 앞으로 이동 및 y 세팅
            spawnPos += HouseBuild.forward * -(BuildItemGap / 2 * BuildItemList.Count);
            spawnPos.y = HouseBuild.transform.position.y + AreaHeightsize / 2;
            return spawnPos;
        }

        private void MoveBackBuildItems()
        {
            //기존에 설치되어있던 건축자재 뒤로 이동
            foreach (GameObject item in BuildItemList)
            {
                item.transform.position += item.transform.forward * BuildItemGap / 2;
            }
        }

        public void DeleteBuildingItemObjSorting(GameObject deleteObj) //있는 아이템을 소팅함.
        {
            foreach (GameObject item in BuildItemList)
            {
                float bigZinWalls = deleteObj.transform.localPosition.z;//삭제할 오브젝트의 z값

                if (bigZinWalls <= item.transform.localPosition.z)//삭제할애가 더 앞이면
                {
                    item.transform.position -= item.transform.forward * BuildItemGap / 2; //반값전진
                }
                else
                {
                    item.transform.position -= item.transform.forward * BuildItemGap / 2; //반값전진
                    item.transform.position += item.transform.forward * BuildItemGap; //가까운것들 정값후진
                    item.GetComponent<BuildingItemObj>().MyOrder--;
                }
            }
        }
        /// </summary>
        ////////////////////////////////////////////////////////
        public void Interact()
        {
            PlayerData.AddValue(buildingId, (int)BuildingBehaviorEnum.Interact, PlayerData.BuildBuildingData, (int)BuildingBehaviorEnum.length);
        }

        public Transform ReturnTF()
        {
            return transform;
        }
        public void SetBuildingState(BuildState buildstate)
        {
            buildState = buildstate;
        }
        public float DistanceToNowBuildItemToNewSort()
        {
            float disc = ((BuildItemList.Count - 1f) / 2f) * BuildItemGap;
            float toClosestItemDist = _distance - disc;
            float dis2c = ((BuildItemList.Count - 1) - curInteractObj.MyOrder) * BuildItemGap + toClosestItemDist;

            return dis2c;
        }
        public override int CanInteract()
        {
            return (int)CursorType.Build;
        }
    }
}