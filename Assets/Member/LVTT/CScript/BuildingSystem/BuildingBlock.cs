using DM.NPC;
using Game.Cam;
using NSY.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum BuildState { None, NotFinish, Finish }
public enum BuildMode { None, BuildHouseMode, DemolishMode }

namespace DM.Building
{
    public class BuildingBlock : MonoBehaviour, IInteractble
    {
        [SerializeField] int buildingId;
        [SerializeField] BuildMode CurBuildMode;
        [SerializeField] MainNpc livingCharacter;

        [SerializeField] Transform HouseBuild;
        [SerializeField] List<GameObject> BuildItemList;

        [SerializeField] public BuildState buildState;

        [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
        [SerializeField] InItemType toolType;
        private bool buildButtonFuncAdded;
        public bool hasWall = false;
        public bool hasDoor = false;
        public bool hasSign = false;

        //static
        public static bool isBuildMode = false;
        public static bool isBuildDemolishMode = false;
        public static BuildingBlock nowBuildingBlock;
        //static public Action UpdateBuildingInfos;//임시선언

        CameraManager CamManager;
        BuildingManager buildManager;

        public float areaWidthsize;
        public float areaHeightsize;

        //BuildItemObj
        [HideInInspector]
        public BuildingItemObj curInteractObj;
        private float BuildItemScaleVar = 0.01f;
        private float BuildItemRotationVar = 1;

        private float BuildItemGap = 0.0001f;


        RaycastHit hit;
        Ray ray;
        int layerMask;   // Player 레이어만 충돌 체크함

        public int BuildingID
        {
            get
            {
                return buildingId;
            }
            set
            {
                buildingId = value;
            }
        }

        ////////////////////////////////////////////////////////
        void Start()
        {
            layerMask = 1 << LayerMask.NameToLayer("Interactable");
            buildButtonFuncAdded = false;
            CamManager = FindObjectOfType<CameraManager>();
            buildManager = FindObjectOfType<BuildingManager>();
            buildManager.SetbuildOffButtonEvents(BuildModeOff);
        }
        public MainNpc GetLivingChar()
        {
            return livingCharacter;
        }
        public void SetLivingChar(MainNpc mainNpc)
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
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

                if (Input.GetMouseButtonDown(0))
                {
                    print("MouseDown");
                    if (curInteractObj == null) return;

                    if (Physics.Raycast(ray, out hit, 10000, layerMask))
                    {

                        print(hit.collider.name);

                        if (hit.collider.GetComponent<BuildingItemObj>() == null) //자재가 아닌걸 클릭 시
                        {
                            if (!curInteractObj.ItemisSet && !curInteractObj.IsFirstDrop)
                            {
                                print("ItemisSet = true 1 ");
                                SetBuildingItemObj();
                            }
                            else //처음 생성 시
                            {
                                print("ItemisSet = false 1");
                            }
                        }
                        else
                        {
                            if (curInteractObj.ItemisSet) //자재 클릭 + 세팅된 자재일 때
                            {
                                print("ItemisSet = false 2");
                                curInteractObj = hit.collider.GetComponent<BuildingItemObj>();
                                curInteractObj.ItemisSet = false;
                                BuildingItemObjAndSorting();
                            }
                            else //자재 클릭 + 무빙중일 때
                            {
                                print("ItemisSet = true 2 ");
                                SetBuildingItemObj();
                            }
                        }
                    }
                    else
                    {
                        print("ItemisSet = true 3 ");

                    }
                }

                if (curInteractObj && !curInteractObj.ItemisSet)
                {
                    ScaleBuildItem();
                    RotateBuildItem();
                }
            }
            else if (CurBuildMode == BuildMode.DemolishMode)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out hit, 10000, layerMask))
                    {
                        if (hit.collider.GetComponent<BuildingItemObj>() == null) return;
                        curInteractObj = hit.collider.GetComponent<BuildingItemObj>();

                        curInteractObj.Demolish();
                    }
                }
            }

        }

        private void SetBuildingItemObj()
        {
            curInteractObj.ItemisSet = true;
            curInteractObj.IsFirstDrop = false;
            CancleUI(false);
            curInteractObj.PutDownBuildingItemObj(areaWidthsize, areaHeightsize);
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
            BuildItemList.Remove(Item);
        }
        public void RemoveDemolishedBuildItem()
        {
            foreach (GameObject Item in BuildItemList)
            {
                if (Item == null)
                {
                    this.BuildItemList.Remove(Item);
                }
            }
        }

        public void OnBuildMode()
        {

            buildManager.BuildingInteractButtonOnOff(true);

            nowBuildingBlock = this;
            Interact();

            Action buildModeOn = BuildModeOn;
            Action buildDemolishModeOn = BuildDemolishModeOn;

            if (!this.buildButtonFuncAdded)
            {
                buildManager.SetBuildButtonEvents(buildModeOn, buildDemolishModeOn);

                this.buildButtonFuncAdded = true;
            }
        }

        private static void ResetButtonEvents(UnityEngine.UI.Button[] buttons)
        {
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false);
                button.onClick.RemoveAllListeners();
            }
        }

        public void BuildModeOn()
        {
            buildButtonFuncAdded = false;
            TutoUI(true);

            BuildOffUI(true);
            nowBuildingBlock.GetComponent<BoxCollider>().enabled = false;

            CamManager.ChangeFollowTarger(gameObject.transform, 1);
            CamManager.ChangeFollowTarger(gameObject.transform, 2);
            CamManager.ChangeFollowTarger(gameObject.transform, 3);

            SetBuildMode(BuildMode.BuildHouseMode);

            isBuildMode = true;

            CamManager.ActiveSubCamera(1);

            //SuperManager.Instance.inventoryManager.CheckCanBuildItem(nowBuildingBlock);
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

            SetBuildMode(BuildMode.DemolishMode);

            isBuildDemolishMode = true;

            CamManager.ActiveSubCamera(1);
        }

        public void BuildModeOff()
        {
            if (!isBuildMode && !isBuildDemolishMode) return;

            CamManager.DeactiveSubCamera(1);
            CamManager.DeactiveSubCamera(2);
            CamManager.DeactiveSubCamera(3);
            TutoUI(false);

            nowBuildingBlock.GetComponent<BoxCollider>().enabled = true;

            if (curInteractObj)
            {
                curInteractObj.ItemisSet = true;
                curInteractObj.IsFirstDrop = false;
            }
            CancleUI(false);
            SetBuildMode(BuildMode.None);

            if (nowBuildingBlock.IsCompleteBuilding())
            {
                SetBuildingState(BuildState.Finish);
                buildManager.AddBuilding(nowBuildingBlock);
            }
            else
                SetBuildingState(BuildState.NotFinish);

            BuildOffUI(false);
            buildManager.PlayerOnOff(true);
            isBuildMode = false;
            isBuildDemolishMode = false;
            //SuperManager.Instance.inventoryManager.CheckCanBuildItem(null);
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
        /// <summary>
        /// //////////BuildObj Sorting
        public void BtnSpawnHouseBuildItem(Item spawnObj)
        {
            Vector3 spawnPos = HouseBuild.transform.position;

            //if (BuildItemList.Count == 0)
            //{
            //    if (spawnObj.InItemType == InItemType.BuildWall)
            //        hasWall = true;
            //}
            //else if (spawnObj.InItemType == InItemType.BuildWall)
            //{
            //    float bigZinWalls = 999999;
            //    foreach (GameObject item in BuildItemList)
            //    {
            //        if (item.GetComponent<BuildingItemObj>().GetInItemType() == InItemType.BuildWall)
            //        {
            //            if (bigZinWalls > item.transform.position.z)
            //            {
            //                bigZinWalls = item.transform.position.z;
            //            }

            //            item.transform.position
            //             = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z + BuildItemGap / 2);
            //        }
            //        if (item.GetComponent<BuildingItemObj>().GetInItemType() == InItemType.BuildNormal)
            //        {
            //            item.transform.position
            //             = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z - BuildItemGap / 2);
            //        }
            //    }
            //    spawnPos.z = bigZinWalls - BuildItemGap / 2;// when the building is facing South
            //    hasWall = true;
            //    SuperManager.Instance.inventoryManager.CheckCanBuildItem(nowBuildingBlock);
            //}
            //else
            //{
            //if (spawnObj.InItemType == InItemType.BuildWall) hasDoor = true;

            foreach (GameObject item in BuildItemList)
            {
                item.transform.position += item.transform.forward * BuildItemGap / 2;
            print(item.transform.position.z);
                //item.transform.position
                //      = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z + BuildItemGap / 2);
            }
            spawnPos += HouseBuild.forward * -(BuildItemGap/2 * BuildItemList.Count);// when the building is facing South
            //}
            CancleUI(true);
            spawnPos.y = HouseBuild.transform.position.y + areaHeightsize / 2;
            GameObject newPrefab = Instantiate(spawnObj.ItemPrefab, spawnPos, Quaternion.identity, HouseBuild.transform);
            newPrefab.transform.localRotation = Quaternion.Euler(0, 0, 0);
            newPrefab.GetComponent<BuildingItemObj>().SetParentBuildArea(nowBuildingBlock);
            newPrefab.name = spawnObj.name;
            AddBuildItemToList(newPrefab);
            FindObjectOfType<EnvironmentManager>().ChangeCleanliness(newPrefab.GetComponent<BuildingItemObj>().GetItem().CleanAmount+ 1);
        }

        void BuildingItemObjAndSorting()
        {
            int frontCount = 0;
            foreach (GameObject item in BuildItemList)
            {
                //if (curInteractObj.GetInItemType() == InItemType.BuildWall)//현재오브젝트가 벽일때
                //{
                //    if (item.GetComponent<BuildingItemObj>().GetInItemType() != InItemType.BuildWall) continue;//선택한게 벽이 아니라면
                //}
                //else if (curInteractObj.GetInItemType() == InItemType.BuildNormal)//현재오브젝트가 벽 아닐때
                //{
                //    if (item.GetComponent<BuildingItemObj>().GetInItemType() != InItemType.BuildNormal) continue;//선택한게 일반이 아니라면
                //}

                float bigZinWalls = curInteractObj.transform.localPosition.z;//클릭한 오브젝트의 z값

                //print(bigZinWalls +" / "+ item.transform.localPosition.z);

                if (bigZinWalls <= item.transform.localPosition.z) continue;//선택한게 다른 옵젝보다 더 가깝다면 검사 패스 z가 크면 멀음

                item.transform.position += item.transform.forward * BuildItemGap;

                //item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, Mathf.Round((item.transform.position.z + BuildItemGap) *10000)*0.0001f);
                frontCount++;
                print(frontCount);
            }

            curInteractObj.transform.position += curInteractObj.transform.forward * -(BuildItemGap * frontCount);
            //curInteractObj.transform.position =
            //new Vector3(curInteractObj.transform.position.x, curInteractObj.transform.position.y, curInteractObj.transform.position.z - (BuildItemGap * frontCount));
        }
        /// </summary>

        ////////////////////////////////////////////////////////
        public void Interact()
        {
            PlayerData.AddValue(buildingId, (int)BuildingBehaviorEnum.Interact, PlayerData.BuildBuildingData, (int)BuildingBehaviorEnum.length);
        }
        public string CanInteract()
        {
            return "건물 상호작용";
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
        public void EndInteract()
        {
            buildManager.BuildingInteractButtonOnOff(false);
        }
    }
}



