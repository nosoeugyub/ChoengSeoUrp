using DM.NPC;
using NSY.Iven;
using NSY.Manager;
using NSY.Player;
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

        public BuildingItemObj curInteractObj;
        private float BuildItemScaleVar = 0.04f;
        private float BuildItemRotationVar = 4;
        private float BuildItemGap = 0.002f;

        public Transform CameraPos;

        SpecialHouse specialHouse;

        RaycastHit hit;
        Ray ray;
        int layerMask;   // Player 레이어만 충돌 체크함

        bool isEmpty;

        public HouseNpc _livingCharacter { get { return livingCharacter; } set { livingCharacter = value; } }
        public int Seasonnum { get { return seasonnum; } set { seasonnum = value; } }
        public SpecialHouse SpecialHouse { get { return specialHouse; } set { specialHouse = value; } }
        public Transform HouseOwnerTransform { get { return houseOwnerTransform; } set { houseOwnerTransform = value; } }
        public Transform FriendTransform { get { return friendTransform; } set { friendTransform = value; } }
        public Transform HouseBuild { get { return houseBuild; } set { houseBuild = value; } }
        public float AreaWidthsize { get { return areaWidthsize; } set { areaWidthsize = value; } }
        public float AreaHeightsize { get { return areaHeightsize; } set { areaHeightsize = value; } }
        public int BuildingID { get { return buildingId; } set { buildingId = value; } }

        public delegate void VoidDelegate(BuildingBlock buildingBlock);
        public delegate void CancelUIDelegate(bool ison);
        public CancelUIDelegate cancelUIDelegate;

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
        public void CancelUIAction(CancelUIDelegate action)
        {
            cancelUIDelegate += action;
        }
        public void BuildModeOnSetting()
        {
            Interact();
            inventory.EnableCanBuildItem();
            ConstructSignsActive(false);
            GetComponent<BoxCollider>().enabled = false;
            SetBuildMode(BuildMode.BuildHouseMode);
        }
        public void BuildModeOffSetting(VoidDelegate addBuilding)
        {
            GetComponent<BoxCollider>().enabled = true;
            SetBuildMode(BuildMode.None);
            inventory.InvenAllOnOff(true);

            if (curInteractObj)
                curInteractObj.ItemisSet = true;

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
            //if(curInteractObj.ParentBuildArea == this)
            curInteractObj = buildingItemObj;
        }

        public HouseNpc GetLivingChar()
        {
            return livingCharacter;
        }
        public void SetLivingChar(HouseNpc mainNpc)
        {
            livingCharacter = mainNpc;
        }
        public bool HaveLivingChar()
        {
            return livingCharacter;
        }
        private void Update()
        {
            if (CurBuildMode != BuildMode.BuildHouseMode) return;

            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 20, Color.blue, 0.3f);

                if (Physics.Raycast(ray, out hit, 20) && !IsPointerOverUIObject())// 
                {
                    //if (curInteractObj != null)
                    //{
                    //    if (curInteractObj.ItemisSet) //고정된 상태라면...
                    //    {
                    //        Debug.Log("curInteractObj.ItemisSet");
                    //        invenmanager.CheckBuliditem = hit.collider.GetComponent<BuildingItemObj>().item;//  건축 슬롯말고 건축존에서 다시 클릭할때
                    //        inventory.InvenAllOnOff(false);
                    //        SetCurInteractObj(hit.collider.GetComponent<BuildingItemObj>());

                    //        curInteractObj.ItemisSet = false;
                    //    }
                    //    else //움직이고 있는 상태라면...
                    //    {
                    //        SetBuildingItemObj();
                    //    }
                    //}
                    //else
                    //{
                    //    //if (IsChildItemObj(hit.collider.GetComponent<BuildingItemObj>()))
                    //    {
                    //        SetCurInteractObj(hit.collider.GetComponent<BuildingItemObj>());
                    //        invenmanager.CheckBuliditem = curInteractObj.item;//  건축 슬롯말고 건축존에서 다시 클릭할때
                    //        inventory.InvenAllOnOff(false);
                    //        curInteractObj.ItemisSet = false;
                    //        // BuildingItemObjAndSorting();
                    //    }
                    //}
                    if (curInteractObj != null) //내려놓았거나 든 게 없는 상태  && !curInteractObj.IsFirstDrop
                    {
                        SetBuildingItemObj();
                    }
                    else
                    {
                        inventory.SetCheckBuildItem(hit.collider.GetComponent<BuildingItemObj>().item);
                        inventory.InvenAllOnOff(false);
                        SetCurInteractObj(hit.collider.GetComponent<BuildingItemObj>());

                        curInteractObj.ItemisSet = false;
                    }
                }
            }
            if (!curInteractObj) return;

            if (!curInteractObj.ItemisSet)
            {
                ScaleBuildItem();
                RotateBuildItem();
                FrontBackMoveBuildItem();
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (curInteractObj.IsFirstDrop)
                {
                    BackToInventory();
                }
                else
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

                    if (!curInteractObj.ItemisSet && Physics.Raycast(ray, out hit, 100, layerMask))
                    {
                        if (hit.collider.GetComponent<BuildingItemObj>() == null) return;

                        SetCurInteractObj(hit.collider.GetComponent<BuildingItemObj>());
                        if (curInteractObj.Demolish()) //삭제에 성공했다면? 하지만 삭제에 성공하려면 그 전에 RemoveBuildItemToList 해야...
                                                       //하지만 그러려면 curInteractObj에서 건축 영역의 메서드를 호출해야함
                        {
                            RemoveBuildItemToList(curInteractObj);
                            DeleteBuildingItemObjSorting(curInteractObj.gameObject);
                            Destroy(curInteractObj.gameObject);

                            inventory.InvenSlotResetCanBuildMode(); //빌딩가능모드로 인벤 리셋
                        }
                    }
                }
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

        public void BackToInventory() //인벤토리로 넘기기
        {
            SuperManager.Instance.inventoryManager.AddItem(curInteractObj.item);
            inventory.InvenSlotResetCanBuildMode(); //빌딩가능모드로 인벤 리셋

            RemoveBuildItemToList(curInteractObj);
            DeleteBuildingItemObjSorting(curInteractObj.gameObject);
            cancelUIDelegate(false);

            Destroy(curInteractObj.gameObject);
        }

        private void SetBuildingItemObj()//설치하기
        {
            inventory.InvenSlotResetCanBuildMode(); //빌딩가능모드로 인벤 리셋
            curInteractObj.ItemisSet = true;
            if (curInteractObj.IsFirstDrop)
            {
                curInteractObj.IsFirstDrop = false;
                //어..
                PlayerData.AddValue((int)curInteractObj.GetItem().InItemType, (int)ItemBehaviorEnum.builditem, PlayerData.ItemData, (int)ItemBehaviorEnum.length);
            }
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
                specialHouse.CanExist(curInteractObj, false);

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

        ///
        /// <summary>
        /// //////////BuildObj Sorting
        public void BtnSpawnHouseBuildItem(Item spawnObj)
        {
            Vector3 spawnPos = HouseBuild.transform.position;

            //기존에 설치되어있던 건축자재 뒤로 이동
            foreach (GameObject item in BuildItemList)
            {
                item.transform.position += item.transform.forward * BuildItemGap / 2;
            }

            //새로 설치할 건축자재 앞으로 이동 및 y 세팅
            spawnPos += HouseBuild.forward * -(BuildItemGap / 2 * BuildItemList.Count);
            spawnPos.y = HouseBuild.transform.position.y + AreaHeightsize / 2;

            GameObject newPrefab = Instantiate(spawnObj.ItemPrefab, spawnPos, Quaternion.identity, HouseBuild.transform);
            newPrefab.transform.localRotation = Quaternion.Euler(0, 0, 0);
            newPrefab.GetComponent<BuildingItemObj>().SetParentBuildArea(this, HouseBuild.position);
            SetCurInteractObj(newPrefab.GetComponent<BuildingItemObj>());
            newPrefab.name = spawnObj.name;
            if (specialHouse)
                specialHouse.CanExist(curInteractObj, true);
            curInteractObj.SetOrder(BuildItemList.Count);
            AddBuildItemToList(newPrefab);
            FindObjectOfType<EnvironmentManager>().ChangeCleanliness(newPrefab.GetComponent<BuildingItemObj>().GetItem().CleanAmount + 1);
            cancelUIDelegate(true);
        }

        //void BuildingItemObjAndSorting()//n개 
        //{
        //    int frontCount = 0;
        //    foreach (GameObject item in BuildItemList)
        //    {
        //        float bigZinWalls = curInteractObj.transform.localPosition.z;//클릭한 오브젝트의 z값

        //        if (bigZinWalls <= item.transform.localPosition.z) continue;//선택한게 다른 옵젝보다 더 가깝다면 검사 패스 z가 크면 멀음

        //        item.transform.position += item.transform.forward * BuildItemGap;

        //        frontCount++;
        //        print(frontCount);
        //    }
        //    curInteractObj.transform.position -= curInteractObj.transform.forward * (BuildItemGap * frontCount);
        //}
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
        public float DistanceToNowBuildItem(Vector3 movePos)
        {
            Vector3 VecY = new Vector3(HouseBuild.transform.position.x, 0, HouseBuild.transform.position.z);
            Vector3 moveposY = new Vector3(movePos.x, 0, movePos.z);
            float dist = Vector3.Distance(moveposY, VecY);
            float disc = ((BuildItemList.Count - 1f) / 2f) * BuildItemGap;
            dist -= disc;

            return dist;

        }
        public float DistanceToNowBuildItemToNewSort(Vector3 movePos)
        {
            Vector3 VecY = new Vector3(HouseBuild.transform.position.x, 0, HouseBuild.transform.position.z);
            Vector3 moveposY = new Vector3(movePos.x, 0, movePos.z);
            float dist = Vector3.Distance(moveposY, VecY);
            float disc = ((BuildItemList.Count - 1f) / 2f) * BuildItemGap;
            float closeDist = dist - disc;
            //gap* count -1 - order(2)
            float dis2c = ((BuildItemList.Count - 1) - curInteractObj.MyOrder) * BuildItemGap + closeDist;

            return dis2c;
        }
        public override int CanInteract()
        {
            //EndInteract();
            return (int)CursorType.Build;
        }
        public void EndInteract_()
        {
            //inventory.InvenAllOnOff(true);
            //buildManager.BuildingInteractButtonOnOff(false);
            //EndInteract();
        }
    }
}