using Game.Cam;
using NSY.Manager;
using System.Collections.Generic;
using UnityEngine;
public enum BuildItemKind { Wall, Roof, Door, Window, Signboard, Etc }
public enum BuildState { NotFinish, Finish }
public enum BuildMode { None, BuildHouseMode, DemolishMode }

namespace TT.BuildSystem
{
    public class BuildingBlock : MonoBehaviour, IInteractble
    {
        public BuildMode CurBuildMode;

        public Transform HouseBuild;
        public List<GameObject> BuildItemList;

        [SerializeField] public BuildState buildState;
        [SerializeField] int buildingId;

        [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
        [SerializeField] InItemType toolType;
        [HideInInspector]
        public bool buildButtonFuncAdded;
        [HideInInspector]
        public bool hasWall = false;
        internal bool hasSign = false;

        public static bool isBuildMode = false;
        public static bool isBuildDemolishMode = false;
        public static BuildingBlock nowBuildingBlock;

        CameraManager CamManager;

        public float areaWidthsize;
        public float areaHeightsize;

        //BuildItemObj
        public BuildingItemObj curDragObj;
        public float BuildItemScaleVar = 0.1f;

        private float BuildItemGap = 0.0001f;

        Transform player;

        RaycastHit hit;
        Ray ray;
        int layerMask;   // Player 레이어만 충돌 체크함
        ////////////////////////////////////////////////////////
        void Start()
        {
            layerMask = 1 << LayerMask.NameToLayer("Interactable");
            buildButtonFuncAdded = false;
            CamManager = FindObjectOfType<CameraManager>();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                BuildModeOff();
            }

            if (CurBuildMode == BuildMode.BuildHouseMode)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

                if (Input.GetMouseButtonDown(0))
                {
                    if (curDragObj == null) return;
                    if (Physics.Raycast(ray, out hit, 10000, layerMask))
                    {
                        print(hit.collider.name);
                        if (hit.collider.GetComponent<BuildingItemObj>() == null)
                        {
                            if (!curDragObj.ItemisSet && !curDragObj.IsFirstDrop)
                            {
                                curDragObj.ItemisSet = true;
                            }
                            else
                                curDragObj.IsFirstDrop = false;

                            return;
                        }
                        if (curDragObj.ItemisSet)
                        {
                            curDragObj = hit.collider.GetComponent<BuildingItemObj>();
                            curDragObj.ItemisSet = false;
                            BuildingItemObjAndSorting();
                            print("itemisSet = false; " + curDragObj);
                        }
                        else
                        {
                            print("itemisSet = true;");
                            curDragObj.ItemisSet = true;
                        }
                    }
                    else if (!curDragObj.ItemisSet)
                    {
                        curDragObj.ItemisSet = true;
                        print("itemisSet = true;");
                    }
                }

                if (curDragObj && !curDragObj.ItemisSet)
                    ScaleBuildItem();
            }
            else if (CurBuildMode == BuildMode.DemolishMode)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out hit, 10000, layerMask))
                    {

                    }
                }
            }

        }

        public void AddBuildItemToList(GameObject Item)
        {
            BuildItemList.Add(Item);
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

        public void OnBuildMode(UnityEngine.UI.Button[] buttons, GameObject interactUI, Transform playerTf)
        {
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(true);
            }
            nowBuildingBlock = this;
            player = playerTf;
            //건축물 상호작용 인덱스 체크
            Interact();
            //Set Event Methods
            if (buildState == BuildState.NotFinish)
            {
                if (!this.buildButtonFuncAdded)
                {
                    buttons[0].onClick.AddListener(() =>
                    {
                        BuildModeOn(interactUI);
                        player.gameObject.SetActive(false);
                        ResetButtonEvents(buttons);
                        print("1. Build Building");
                        //1. Build Building
                    });
                    buttons[1].onClick.AddListener(() =>
                    {
                        BuildDemolishModeOn(interactUI);
                        player.gameObject.SetActive(false);
                        ResetButtonEvents(buttons);
                        print("2. break Building");
                        //2. break Building
                    });
                    buttons[2].onClick.AddListener(() =>
                    {
                        print("3. Finish Building");
                        //3. Finish Building
                    });
                    this.buildButtonFuncAdded = true;
                }

            }
            else if (buildState == BuildState.Finish)
            {
                if (!this.buildButtonFuncAdded)
                {
                    buttons[0].onClick.AddListener(() =>
                    {
                        //BuildBuilding();
                        print("1. Repair Building");
                        //1. Repair Building
                    });
                    buttons[1].onClick.AddListener(() =>
                    {
                        //DemolishBuidling();
                        print("2. break Building");
                        //2. break Building
                    });
                    buttons[2].gameObject.SetActive(false);
                    this.buildButtonFuncAdded = true;
                }
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

        public void BuildModeOn(GameObject interactUI)
        {
            buildButtonFuncAdded = false;
            interactUI.SetActive(false);

            GetComponent<BoxCollider>().enabled = false;

            CamManager.ChangeFollowTarger(gameObject.transform, 1);
            CamManager.ChangeFollowTarger(gameObject.transform, 2);
            CamManager.ChangeFollowTarger(gameObject.transform, 3);

            SetBuildMode(BuildMode.BuildHouseMode);

            isBuildMode = true;

            CamManager.ActiveSubCamera(1);

            SuperManager.Instance.inventoryManager.CheckCanBuildItem(nowBuildingBlock);
            //Inventory UI On + Can't turn Off while in build mode + Press X button, Invoke BuildModeOff method
        }

        public void BuildDemolishModeOn(GameObject interactUI)
        {
            buildButtonFuncAdded = false;
            interactUI.SetActive(false);

            CamManager.ChangeFollowTarger(gameObject.transform, 1);
            CamManager.ChangeFollowTarger(gameObject.transform, 2);
            CamManager.ChangeFollowTarger(gameObject.transform, 3);

            SetBuildMode(BuildMode.DemolishMode);

            isBuildDemolishMode = true;

            CamManager.ActiveSubCamera(1);
        }

        public void BuildModeOff()
        {
            if (!isBuildMode || isBuildDemolishMode) return;
            CamManager.DeactiveSubCamera(1);
            CamManager.DeactiveSubCamera(2);
            CamManager.DeactiveSubCamera(3);

            GetComponent<BoxCollider>().enabled = true;
            SetBuildMode(BuildMode.None);

            player.gameObject.SetActive(true);
            isBuildMode = false;
            isBuildDemolishMode = false;

            SuperManager.Instance.inventoryManager.CheckCanBuildItem(null);
        }
        public void SetBuildMode(BuildMode buildmode)
        {
            CurBuildMode = buildmode;
        }
        void ScaleBuildItem()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                Vector3 var = curDragObj.transform.localScale;
                var.x += BuildItemScaleVar;
                var.y += BuildItemScaleVar;
                curDragObj.SetBuildItemScale(var);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                Vector3 var = curDragObj.transform.localScale;
                var.x -= BuildItemScaleVar;
                var.y -= BuildItemScaleVar;
                curDragObj.SetBuildItemScale(var);
            }
        }
        public void BtnSpawnHouseBuildItem(Item spawnObj)
        {
            Vector3 spawnPos = HouseBuild.transform.position;
            spawnPos.y = HouseBuild.transform.position.y + areaHeightsize / 2;

            if (BuildItemList.Count == 0)
            {
            }
            else if (spawnObj.OutItemType == OutItemType.BuildWall)
            {
                float bigZinWalls = 999999;
                foreach (GameObject item in BuildItemList)
                {
                    if (item.GetComponent<BuildingItemObj>().GetOutItemType() == OutItemType.BuildWall)
                    {
                        if (bigZinWalls > item.transform.position.z)
                        {
                            bigZinWalls = item.transform.position.z;
                        }

                        item.transform.position
                         = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z + BuildItemGap / 2);
                    }
                    if (item.GetComponent<BuildingItemObj>().GetOutItemType() == OutItemType.BuildNormal)
                    {
                        item.transform.position
                         = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z - BuildItemGap / 2);
                    }
                }
                spawnPos.z = bigZinWalls - BuildItemGap / 2;// when the building is facing South
                hasWall = true;
                SuperManager.Instance.inventoryManager.CheckCanBuildItem(nowBuildingBlock);
            }
            else
            {
                foreach (GameObject item in BuildItemList)
                {
                    item.transform.position
                        = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z + BuildItemGap / 2);
                }
                spawnPos.z = spawnPos.z - (BuildItemGap / 2 * BuildItemList.Count);// when the building is facing South
            }
            GameObject newPrefab = Instantiate(spawnObj.ItemPrefab, spawnPos, Quaternion.identity, HouseBuild.transform);
            newPrefab.GetComponent<BuildingItemObj>().SetParentBuildArea(nowBuildingBlock);
            newPrefab.name = spawnObj.name;
            AddBuildItemToList(newPrefab);
        }

        void BuildingItemObjAndSorting()
        {
            int frontCount = 0;
            foreach (GameObject item in BuildItemList)
            {
                if (curDragObj.GetOutItemType() == OutItemType.BuildWall)//현재오브젝트가 벽일때
                {
                    if (item.GetComponent<BuildingItemObj>().GetOutItemType() != OutItemType.BuildWall) continue;//선택한게 벽이 아니라면
                }
                else if (curDragObj.GetOutItemType() == OutItemType.BuildNormal)//현재오브젝트가 벽일때
                {
                    if (item.GetComponent<BuildingItemObj>().GetOutItemType() != OutItemType.BuildNormal) continue;//선택한게 일반이 아니라면
                }

                float bigZinWalls = curDragObj.transform.position.z;//클릭한 오브젝트의 z값
                if (bigZinWalls <= item.transform.position.z) continue;//선택한게 다른 옵젝보다 더 가깝다면 검사 패스

                item.transform.position
                 = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z + BuildItemGap);
                frontCount++;
            }
            curDragObj.transform.position =
                    new Vector3(curDragObj.transform.position.x, curDragObj.transform.position.y, curDragObj.transform.position.z - (BuildItemGap * frontCount));
        }

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
    }
}



