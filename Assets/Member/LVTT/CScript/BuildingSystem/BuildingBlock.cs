using NSY.Manager;
using System.Collections.Generic;
using UnityEngine;
using Game.Cam;
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
        [HideInInspector]
        public float CurWallItemzPos;
        [HideInInspector]
        public float CurFrontItemzPos;
        [HideInInspector]
        public float MaxBackItemzPos;

        [SerializeField] public BuildState buildState;
        [SerializeField] int buildingId;

        [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
        [SerializeField] InItemType toolType;
        [HideInInspector]
        public bool buildButtonFuncAdded;
        [HideInInspector]
        public bool hasWall = false;
        internal bool hasSign =  false;

        public static bool isBuildMode = false;
        public static bool isBuildDemolishMode = false;
        public static BuildingBlock nowBuildingBlock;

        CameraManager CamManager;

        public float HalfGuideObjWidth;
        public float HalfGuideObjHeight;

        //BuildItemObj
        public BuildingItemObj curDragObj;
        public float BuildItemScaleVar = 0.1f;

        [HideInInspector]
        public bool OnBuildItemDrag = false;

        private float BuildItemGap = 0.000001f;
        public float SpawnOffsetY;
        public float SpawnOffsetZ;

        ////////////////////////////////////////////////////////
        void Start()
        {
            buildButtonFuncAdded = false;
            CamManager = FindObjectOfType<CameraManager>();
            //hasWall = false; 
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

            //건축물 상호작용 인덱스 체크
            Interact();
            //Set Event Methods
            if (buildState == BuildState.NotFinish)
            {
                if (!this.buildButtonFuncAdded)
                {
                    buttons[0].onClick.AddListener(() =>
                    {
                        BuildModeOn( interactUI);
                        playerTf.gameObject.SetActive(false);
                        ResetButtonEvents(buttons);
                        print("1. Build Building");
                        //1. Build Building
                    });
                    buttons[1].onClick.AddListener(() =>
                    {
                        BuildDemolishModeOn(interactUI);
                        playerTf.gameObject.SetActive(false);
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

        public void BuildModeOn( GameObject interactUI)
        {
            buildButtonFuncAdded = false;
            interactUI.SetActive(false);

            CamManager.ChangeFollowTarger(gameObject.transform, 1);
            CamManager.ChangeFollowTarger(gameObject.transform, 2);
            CamManager.ChangeFollowTarger(gameObject.transform, 3);

            SetBuildMode(BuildMode.BuildHouseMode);

            isBuildMode = true;

            CamManager.ActiveSubCamera(1);

            SuperManager.Instance.inventoryManager.CheckCanBuildItem(nowBuildingBlock);
            //Inventory UI On + Can't turn Off while in build mode + Press X button, Invoke BuildModeOff method
        }

        public void BuildDemolishModeOn( GameObject interactUI)
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
            CamManager.DeactiveSubCamera(1);
            CamManager.DeactiveSubCamera(2);
            CamManager.DeactiveSubCamera(3);

            SetBuildMode(BuildMode.None);

            isBuildMode = false;
            isBuildDemolishMode = false;

            SuperManager.Instance.inventoryManager.CheckCanBuildItem(null);
        }
        public void SetBuildMode(BuildMode buildmode)
        {
            CurBuildMode = buildmode;
            Debug.Log("curBuildMode is" + buildmode);
        }
            void ScaleBuildItem()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                Vector3 var = curDragObj.transform.localScale;
                var.x += BuildItemScaleVar;
                var.y += BuildItemScaleVar;
                curDragObj.SetBuildItemScale(var);
                // Debug.Log("Mouse is Scrolling up");
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                Vector3 var = curDragObj.transform.localScale;
                var.x -= BuildItemScaleVar;
                var.y -= BuildItemScaleVar;
                curDragObj.SetBuildItemScale(var);
                // Debug.Log("Mouse is Scrolling down");
            }
        }
        public void BtnSpawnHouseBuildItem(Item spawnObj)
        {
            Vector3 spawnPos = HouseBuild.transform.position;
            spawnPos.y = spawnPos.y + SpawnOffsetY;
            if (spawnObj.OutItemType == OutItemType.BuildWall)
            {
                spawnPos.z = spawnPos.z - SpawnOffsetZ;// when the building is facing South
                CurWallItemzPos = spawnPos.z;
                hasWall = true;
                SuperManager.Instance.inventoryManager.CheckCanBuildItem(nowBuildingBlock);

            }
            else
            {
                spawnPos.z = spawnPos.z - SpawnOffsetZ - (BuildItemGap * BuildItemList.Count);// when the building is facing South
                if (BuildItemList.Count == 1)
                {
                    MaxBackItemzPos = spawnPos.z;
                }
            }
            Debug.Log(spawnPos);
            GameObject newPrefab = Instantiate(spawnObj.ItemPrefab, spawnPos, Quaternion.identity, HouseBuild.transform);
            newPrefab.GetComponent<BuildingItemObj>().SetParentBuildArea(nowBuildingBlock);
            newPrefab.name = spawnObj.name;
            AddBuildItemToList(newPrefab);
            CurFrontItemzPos = spawnPos.z;
        }

        //void GuideObjCal()
        //{
        //    // Debug.Log("GuideObjCalculate");
        //    HalfGuideObjHeight = GuideObjCorner.transform.position.y - GuideObj.transform.position.y;
        //    HalfGuideObjWidth = GuideObjCorner.transform.position.x - GuideObj.transform.position.x;
        //    //Debug.Log(HalfGuideObjHeight);
        //    //Debug.Log(HalfGuideObjWidth);
        //}
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

        void BuildButtonsListenerRemove(UnityEngine.UI.Button[] buttons)
        {
            foreach (var button in buttons)
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }
}



