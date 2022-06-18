using DM.NPC;
using Game.Cam;
using NSY.Iven;
using NSY.Manager;
using System.Collections.Generic;
using UnityEngine;
public enum BuildState { None, NotFinish, Finish }
public enum BuildMode { None, BuildHouseMode, DemolishMode }

namespace DM.Building
{
    public class BuildingBlock : Interactable
    {
        [SerializeField] private int buildingId;
        [SerializeField] private BuildMode CurBuildMode;
        [SerializeField] private HouseNpc livingCharacter;


        [SerializeField] private Transform houseBuild;
        [SerializeField] private List<GameObject> BuildItemList;

        [SerializeField] BuildState buildState;

        [SerializeField] private float areaWidthsize;
        [SerializeField] private float areaHeightsize;

        [SerializeField] InItemType toolType;

        [SerializeField] Transform houseOwnerTransform;
        [SerializeField] Transform friendTransform;
        [SerializeField] Transform constructsign;

        private bool buildButtonFuncAdded;

        private CameraManager CamManager;
        private BuildingManager buildManager;
        private InvenToryManagers invenmanager;

        public BuildingItemObj curInteractObj;
        private float BuildItemScaleVar = 0.01f;
        private float BuildItemRotationVar = 1;
        private float BuildItemGap = 0.002f;

        //늙고 병든 노성엽이 추가한 카메라 포지션
        public Transform CameraPos;

        SpecialHouse specialHouse;
        static TextBox textBox;

        RaycastHit hit;
        Ray ray;
        int layerMask;   // Player 레이어만 충돌 체크함

        bool isEmpty;

        public HouseNpc _livingCharacter { get { return livingCharacter; } set { livingCharacter = value; } }
        public SpecialHouse SpecialHouse { get { return specialHouse; } set { specialHouse = value; } }
        public static bool isBuildMode { get; set; } = false;
        public static bool isBuildDemolishMode { get; set; } = false;
        public static BuildingBlock nowBuildingBlock { get; set; } = null;

        public Transform HouseOwnerTransform { get { return houseOwnerTransform; } set { houseOwnerTransform = value; } }
        public Transform FriendTransform { get { return friendTransform; } set { friendTransform = value; } }
        public Transform HouseBuild { get { return houseBuild; } set { houseBuild = value; } }
        public float AreaWidthsize { get { return areaWidthsize; } set { areaWidthsize = value; } }
        public float AreaHeightsize { get { return areaHeightsize; } set { areaHeightsize = value; } }
        public int BuildingID { get { return buildingId; } set { buildingId = value; } }


        //
        private InventoryNSY inventory;

        private void Awake()
        {
            CamManager = FindObjectOfType<CameraManager>();
            buildManager = FindObjectOfType<BuildingManager>();
            specialHouse = GetComponent<SpecialHouse>();
            invenmanager = FindObjectOfType<InvenToryManagers>();
            inventory = SuperManager.Instance.inventoryManager;
        }
        void Start()
        {
            layerMask = 1 << LayerMask.NameToLayer("Wall");
            buildButtonFuncAdded = false;
            buildManager.SetbuildOffButtonEvents(BuildModeOff);

            constructsign.gameObject.SetActive(!IsCompleteBuilding());
        }

        public void SetCurInteractObj(BuildingItemObj buildingItemObj)
        {
            //if(curInteractObj.ParentBuildArea == this)
            //print(buildingItemObj);
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
            if (CurBuildMode == BuildMode.BuildHouseMode)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //if (curInteractObj == null) return;

                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

                    if (Physics.Raycast(ray, out hit, 100, layerMask))
                    {
                        if (hit.collider.GetComponent<BuildingItemObj>() == null) //자재가 아닌걸 클릭 시
                        {
                            if (curInteractObj != null)
                            {
                                if (!curInteractObj.ItemisSet && !curInteractObj.IsFirstDrop)
                                {
                                    print("ItemisSet = true 1 ");
                                    SetBuildingItemObj();
                                }
                            }
                        }
                        else //자재인걸 클릭 시
                        {
                            if (curInteractObj != null)
                            {
                                if (curInteractObj.ItemisSet) //자재 클릭 + 세팅된 자재일 때  세팅 >> 논세팅
                                {
                                    invenmanager.CheckBuliditem = hit.collider.GetComponent<BuildingItemObj>().item;//  건축 슬롯말고 건축존에서 다시 클릭할때
                                    inventory.InvenAllOnOff(false);
                                    SetCurInteractObj(hit.collider.GetComponent<BuildingItemObj>());
                                    curInteractObj.ItemisSet = false;
                                    //BuildingItemObjAndSorting();
                                }
                                else //자재 클릭 + 무빙중일 때
                                {
                                    SetBuildingItemObj();
                                }
                            }
                            else
                            {
                                invenmanager.CheckBuliditem = hit.collider.GetComponent<BuildingItemObj>().item;//  건축 슬롯말고 건축존에서 다시 클릭할때
                                inventory.InvenAllOnOff(false);
                                SetCurInteractObj(hit.collider.GetComponent<BuildingItemObj>());
                                curInteractObj.ItemisSet = false;
                                // BuildingItemObjAndSorting();
                            }
                        }
                    }
                }
                if (Input.GetMouseButtonDown(1))
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

                    if (Physics.Raycast(ray, out hit, 100, layerMask))
                    {
                        if (hit.collider.GetComponent<BuildingItemObj>() == null ||
                            !hit.collider.GetComponent<BuildingItemObj>().ItemisSet) return;

                        SetCurInteractObj(hit.collider.GetComponent<BuildingItemObj>());
                        curInteractObj.Demolish();
                    }
                }

                if (curInteractObj && !curInteractObj.ItemisSet)
                {
                    ScaleBuildItem();
                    RotateBuildItem();
                    FrontBackMoveBuildItem();
                }
            }

        }
        public void InvenSlotResetCanBuildMode()
        {
            invenmanager.CheckBuliditem = null; //설치하면 다른거 할수없음
            inventory.InvenAllOnOff(true);
        }

        private void SetBuildingItemObj()//설치하기
        {
            InvenSlotResetCanBuildMode(); //빌딩가능모드로 인벤 리셋

            curInteractObj.ItemisSet = true;
            curInteractObj.IsFirstDrop = false;
            CancleUI(false);
            curInteractObj.PutDownBuildingItemObj(AreaWidthsize, AreaHeightsize);
        }

        public List<BuildingItemObj> GetBuildItemList()
        {
            List<BuildingItemObj> items = new List<BuildingItemObj>();
            if (isBuildMode || isBuildDemolishMode) return items;

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
        public void RemoveBuildItemToList(GameObject Item)
        {
            if (specialHouse)
                specialHouse.CanExist(curInteractObj, false);

            FindObjectOfType<EnvironmentManager>().ChangeCleanliness(-(Item.GetComponent<BuildingItemObj>().GetItem().CleanAmount + 1));
            BuildItemList.Remove(Item);
            invenmanager.CheckBuliditem = null; //설치하면 다른거 할수없음
        }
        public void RemoveDemolishedBuildItem()
        {
            foreach (GameObject Item in BuildItemList)
            {
                if (Item == null)
                {
                    FindObjectOfType<EnvironmentManager>().ChangeCleanliness(-(Item.GetComponent<BuildingItemObj>().GetItem().CleanAmount + 1));
                    this.BuildItemList.Remove(Item);

                }
            }
        }

        public void OnBuildMode()
        {
            nowBuildingBlock = this;
            Interact();

            BuildModeOn();
            //if (!this.buildButtonFuncAdded)
            //{

            //    buildManager.SetBuildButtonEvents(BuildModeOn, BuildDemolishModeOn);

            //    this.buildButtonFuncAdded = true;
            //}
        }

        private static void ResetButtonEvents(UnityEngine.UI.Button[] buttons)
        {
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false);
                button.onClick.RemoveAllListeners();
            }
        }
        public static void SetTextBox(TextBox textBox_)
        {
            textBox = textBox_;
        }
        public void BuildModeOn()
        {
            if (SuperManager.Instance.dialogueManager.IsTalking) return;
            if (textBox)
                textBox.gameObject.SetActive(false);
            buildButtonFuncAdded = false;
            TutoUI(true);
            SuperManager.Instance.npcManager.AllNpcActive(false);
            //buildManager.PlayerOnOff(false);
            BuildOffUI(true);
            nowBuildingBlock.constructsign.gameObject.SetActive(false);
            nowBuildingBlock.GetComponent<BoxCollider>().enabled = false;

            CamManager.ChangeFollowTarger(gameObject.transform, 1);
            CamManager.ChangeFollowTarger(gameObject.transform, 2);
            CamManager.ChangeFollowTarger(gameObject.transform, 3);

            nowBuildingBlock.SetBuildMode(BuildMode.BuildHouseMode);

            isBuildMode = true;

            CamManager.ActiveSubCamera(1);

            //주석 부분
            SuperManager.Instance.inventoryManager.CheckCanBuildItem();
            //Inventory UI On + Can't turn Off while in build mode + Press X button, Invoke BuildModeOff method
            //FindObjectOfType<PopUpManager>().OpenPopup(FindObjectOfType<PopUpManager>()._ivenPopup);
        }

        public void BuildDemolishModeOn()
        {
            buildButtonFuncAdded = false;
            //interactUI.SetActive(false);
            BuildOffUI(true);

            nowBuildingBlock.GetComponent<BoxCollider>().enabled = false;

            CamManager.ChangeFollowTarger(gameObject.transform, 1);
            CamManager.ChangeFollowTarger(gameObject.transform, 2);
            CamManager.ChangeFollowTarger(gameObject.transform, 3);

            nowBuildingBlock.SetBuildMode(BuildMode.DemolishMode);

            isBuildDemolishMode = true;

            CamManager.ActiveSubCamera(1);
        }

        public void BuildModeOff()
        {

            foreach (ItemSlot itemSlot in inventory.ItemSlots)
            {
                itemSlot.canInteractWithSlot = false;
            }

            if (!isBuildMode && !isBuildDemolishMode) return;

            CamManager.DeactiveSubCamera(1);
            CamManager.DeactiveSubCamera(2);
            CamManager.DeactiveSubCamera(3);
            TutoUI(false);
            if (textBox)
                textBox.gameObject.SetActive(true);

            nowBuildingBlock.GetComponent<BoxCollider>().enabled = true;

            if (nowBuildingBlock.curInteractObj)
            {
                nowBuildingBlock.curInteractObj.ItemisSet = true;
                nowBuildingBlock.curInteractObj.IsFirstDrop = false;
            }
            CancleUI(false);
            nowBuildingBlock.SetBuildMode(BuildMode.None);

            if (nowBuildingBlock.IsCompleteBuilding())
            {
                SetBuildingState(BuildState.Finish);
                buildManager.AddBuilding(nowBuildingBlock);
            }
            else
            {
                SetBuildingState(BuildState.NotFinish);
                nowBuildingBlock.constructsign.gameObject.SetActive(true);
            }

            BuildOffUI(false);
            //buildManager.PlayerOnOff(true);
            SuperManager.Instance.npcManager.AllNpcActive(true);
            isBuildMode = false;
            isBuildDemolishMode = false;
            //주석 부분
            SuperManager.Instance.inventoryManager.InvenAllOnOff(true);
        }
        public void CancleUI(bool on)
        {
            buildManager.CancleUIState(on);
        }
        public void BuildOffUI(bool on)
        {
            buildManager.BuildOffUiState(on);
        }
        public void TutoUI(bool on)
        {
            buildManager.TutoUIState(on);
        }
        public bool IsCompleteBuilding()//벽과 문이 있다면 건설 완료 처리
        {
            if (buildState == BuildState.Finish) return true;

            if (BuildItemList.Count > 0)
                return true;
            //if (hasWall)
            //{
            //    if (PlayerData.BuildBuildingData[BuildingID].amounts[(int)BuildingBehaviorEnum.CompleteBuild] < 1)
            //    {
            //        PlayerData.AddValue(BuildingID, (int)BuildingBehaviorEnum.CompleteBuild, PlayerData.BuildBuildingData, ((int)BuildingBehaviorEnum.length));
            //        Debug.Log(PlayerData.BuildBuildingData[BuildingID].amounts[(int)BuildingBehaviorEnum.CompleteBuild]);
            //    }
            //    return true;
            //}
            else return false;
        }

        public void SetBuildMode(BuildMode buildmode)
        {
            CurBuildMode = buildmode;
            //DebugText.Instance.SetText(string.Format("CurBuildMode: {0}", CurBuildMode.ToString()));
        }
        void ScaleBuildItem()
        {
            if (Input.GetKey(buildManager.scaleUpKey))
            {
                Vector3 var = curInteractObj.transform.localScale;
                var.x += BuildItemScaleVar;
                var.y += BuildItemScaleVar;
                curInteractObj.SetBuildItemScale(var);
            }
            else if (Input.GetKey(buildManager.scaleDownKey))
            {
                Vector3 var = curInteractObj.transform.localScale;
                var.x -= BuildItemScaleVar;
                var.y -= BuildItemScaleVar;
                curInteractObj.SetBuildItemScale(var);
            }
        }
        void RotateBuildItem()
        {
            if (Input.GetKey(buildManager.rotateLeftKey))
            {
                curInteractObj.SetBuildItemRotation(+BuildItemRotationVar);
            }
            else if (Input.GetKey(buildManager.rotateRightKey))
            {
                curInteractObj.SetBuildItemRotation(-BuildItemRotationVar);
            }
        }
        void FrontBackMoveBuildItem()
        {
            if (Input.GetKeyDown(buildManager.frontKey))
            {
                SwitchBuildingItemObjZPos(true);
            }
            else if (Input.GetKeyDown(buildManager.BackKey))
            {
                SwitchBuildingItemObjZPos(false);
            }
        }
        /// <summary>
        /// //////////BuildObj Sorting
        public void BtnSpawnHouseBuildItem(Item spawnObj)
        {
            Vector3 spawnPos = HouseBuild.transform.position;

            //기존에 설치되어있던 건축자재 뒤로 이동
            foreach (GameObject item in BuildItemList)
            {
                item.transform.position += item.transform.forward * BuildItemGap / 2;
                print(item.transform.position.z);
            }

            //새로 설치할 건축자재 앞으로 이동 및 y 세팅
            spawnPos += HouseBuild.forward * -(BuildItemGap / 2 * BuildItemList.Count);
            spawnPos.y = HouseBuild.transform.position.y + AreaHeightsize / 2;

            GameObject newPrefab = Instantiate(spawnObj.ItemPrefab, spawnPos, Quaternion.identity, HouseBuild.transform);
            newPrefab.transform.localRotation = Quaternion.Euler(0, 0, 0);
            newPrefab.GetComponent<BuildingItemObj>().SetParentBuildArea(nowBuildingBlock, HouseBuild.position);
            newPrefab.name = spawnObj.name;
            if (specialHouse)
                specialHouse.CanExist(curInteractObj, true);
            curInteractObj.MyOrder = BuildItemList.Count;
            AddBuildItemToList(newPrefab);
            FindObjectOfType<EnvironmentManager>().ChangeCleanliness(newPrefab.GetComponent<BuildingItemObj>().GetItem().CleanAmount + 1);
            CancleUI(true);
        }

        void BuildingItemObjAndSorting()//n개 
        {
            int frontCount = 0;
            foreach (GameObject item in BuildItemList)
            {
                float bigZinWalls = curInteractObj.transform.localPosition.z;//클릭한 오브젝트의 z값

                if (bigZinWalls <= item.transform.localPosition.z) continue;//선택한게 다른 옵젝보다 더 가깝다면 검사 패스 z가 크면 멀음

                item.transform.position += item.transform.forward * BuildItemGap;

                frontCount++;
                print(frontCount);
            }
            curInteractObj.transform.position -= curInteractObj.transform.forward * (BuildItemGap * frontCount);
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
        public void SwitchBuildingItemObjZPos(bool isUp)
        {
            GameObject nearObj = null;
            float curObjZ = curInteractObj.transform.localPosition.z;//선택한 오브젝트의 z값
            float bujildItemZ = 10000;
            //float minDIst = 10000;
            if (isUp)
            {
                foreach (GameObject item in BuildItemList)
                {
                    bujildItemZ = item.transform.localPosition.z;
                    if (!nearObj)
                    {
                        if (curObjZ > bujildItemZ)
                        {
                            nearObj = item;
                        }
                    }
                    else
                    {
                        if (curObjZ > bujildItemZ && nearObj.transform.localPosition.z < bujildItemZ)//해당 자재가 나보다 더 가깝고, 현재 가까운 오브젝트보다 
                        {
                            nearObj = item;
                        }
                    }
                }
                if (nearObj)
                {
                    print("Up Near Obj is " + nearObj.name);
                    nearObj.transform.position += nearObj.transform.forward * BuildItemGap; //가장 가까운 자재후진
                    nearObj.GetComponent<BuildingItemObj>().MyOrder--;
                    curInteractObj.transform.position -= curInteractObj.transform.forward * BuildItemGap; //선택중인 자재 전진
                    curInteractObj.MyOrder++;
                }
                else
                    print("NO NEAROBJ");

            }
            else
            {
                foreach (GameObject item in BuildItemList)
                {
                    bujildItemZ = item.transform.localPosition.z;
                    if (!nearObj)
                    {
                        if (curObjZ < bujildItemZ)
                        {
                            nearObj = item;
                        }
                    }
                    else
                    {
                        if (curObjZ < bujildItemZ && nearObj.transform.localPosition.z > bujildItemZ)//해당 자재가 나보다 더 가깝고, 현재 가까운 오브젝트보다 
                        {
                            nearObj = item;
                        }
                    }
                }
                if (nearObj)
                {
                    print("Down Near Obj is " + nearObj.name);
                    nearObj.transform.position -= nearObj.transform.forward * BuildItemGap; //가장 가까운 자재전진
                    nearObj.GetComponent<BuildingItemObj>().MyOrder++;
                    curInteractObj.transform.position += curInteractObj.transform.forward * BuildItemGap; //선택중인 자재 후진
                    curInteractObj.MyOrder--;
                }
                else
                    print("NO NEAROBJ");

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
            buildManager.BuildingInteractButtonOnOff(false);
            //EndInteract();
        }
    }
}