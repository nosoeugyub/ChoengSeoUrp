using Game.Cam;
using TT.Test;
using UnityEngine;



namespace TT.BuildSystem
{
    public class BuildingManager : MonoBehaviour
    {
        public BuildMode CurBuildMode;
        //[SerializeField] public Transform CurBuilding;
        public GameObject Player;
        //GuideObj
        public float GuideObjOffsetY;
        [SerializeField] Transform GuideObjCorner;
        [SerializeField] Transform GuideObj;
        [HideInInspector]
        public float HalfGuideObjWidth;
        [HideInInspector]
        public float HalfGuideObjHeight;


        [HideInInspector]
        public int BuildItemDragIndex = 0;
        [HideInInspector]
        public bool OnBuildItemDrag = false;

        CameraManager CamManager;
        BuildItemInventorySlot SlotManager;
        UIOnOff TheUI;

        //BuildItemObj
        public BuildingItemObj curDragObj;
        public float BuildItemScaleVar = 0.1f;

        //BuildBlock Obj
        public GameObject[] BuildBlockObjList;
        public BuildingBlock nowBuildingBlock;

        // public GameObject SpawnBuildItem;

        public static bool isBuildMode = false;
        public static bool isBuildDemolishMode = false;
        private void Awake()
        {
            TheUI = FindObjectOfType<UIOnOff>();
            SlotManager = FindObjectOfType<BuildItemInventorySlot>();
            CamManager = FindObjectOfType<CameraManager>();

        }

        private void Start()
        {
            GuideObjCal();
            SetBuildMode(BuildMode.None);
        }

        public void BuildModeOn(BuildingBlock buildingBlock, UnityEngine.UI.Button[] buttons, GameObject interactUI)
        {
            //remove all current listener when click on any button on the BuildInteracMenu 
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false);
                button.onClick.RemoveAllListeners();
            }

            buildingBlock.buildButtonFuncAdded = false;
            interactUI.SetActive(false);
            nowBuildingBlock = buildingBlock;

            TheUI.CurBuilding = nowBuildingBlock.gameObject.transform;

            CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 1);
            CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 2);
            CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 3);

            SlotManager.AssignBuildItemSpawnPos(nowBuildingBlock.HouseBuild, nowBuildingBlock.gameObject.transform);

            //TheUI.IsBuildMode = true;
            isBuildMode = true;
            SetBuildMode(BuildMode.BuildHouseMode);

            TheUI.TurnOnUI(0);

            CamManager.ActiveSubCamera(1);

            Player.SetActive(false);

            //SlotManager.MoveInventToRight();

            ViewGuideObject(0);
            ViewGuideObject(2);
            UnViewGuideObject(1);


        }

        public void BuildDemolishModeOn(BuildingBlock buildingBlock, UnityEngine.UI.Button[] buttons, GameObject interactUI)
        {
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false);
                button.onClick.RemoveAllListeners();
            }

            buildingBlock.buildButtonFuncAdded = false;
            interactUI.SetActive(false);
            nowBuildingBlock = buildingBlock;

            TheUI.CurBuilding = nowBuildingBlock.gameObject.transform;

            CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 1);
            CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 2);
            CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 3);

            SlotManager.AssignBuildItemSpawnPos(nowBuildingBlock.HouseBuild, nowBuildingBlock.gameObject.transform);

            // TheUI.IsBuildMode = true;
            isBuildDemolishMode = true;
            SetBuildMode(BuildMode.DemolishMode);
            //isBuildMode = true;

            TheUI.TurnOnUI(0);

            CamManager.ActiveSubCamera(1);

            Player.SetActive(false);

            ViewGuideObject(0);
            ViewGuideObject(2);
            UnViewGuideObject(1);
            //foreach (Transform child in buildingBlock.HouseBuild)
            //{
            //    GameObject.Destroy(child.gameObject);
            //}    

        }

        public void BuildModeOff()
        {
            BuildingManager.isBuildMode = false;
            BuildingManager.isBuildDemolishMode = false;
            TheUI.TurnOffUI(0);
            isBuildMode = false;
            SetBuildMode(BuildMode.None);
            CamManager.DeactiveSubCamera(1);
            CamManager.DeactiveSubCamera(2);
            CamManager.DeactiveSubCamera(3);

            Player.SetActive(true);

            // SlotManager.ResetInventPos();

            UnViewGuideObject(0);
            UnViewGuideObject(2);

        }

        void ViewGuideObject(int ObjNum)
        {
            BuildBlockObjList[ObjNum].SetActive(true);
            Vector3 GuidePos = nowBuildingBlock.HouseBuild.transform.position;
            GuidePos.y = GuidePos.y + GuideObjOffsetY;
            BuildBlockObjList[ObjNum].transform.position = GuidePos;
        }
        void UnViewGuideObject(int ObjNum)
        {
            BuildBlockObjList[ObjNum].SetActive(false);
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
        public void SetBuildMode(BuildMode buildmode)
        {
            CurBuildMode = buildmode;
            Debug.Log("curBuildMode is" + buildmode);
        }
        void GuideObjCal()
        {
            // Debug.Log("GuideObjCalculate");
            HalfGuideObjHeight = GuideObjCorner.transform.position.y - GuideObj.transform.position.y;
            HalfGuideObjWidth = GuideObjCorner.transform.position.x - GuideObj.transform.position.x;
            //Debug.Log(HalfGuideObjHeight);
            //Debug.Log(HalfGuideObjWidth);
        }
        private void Update()
        {
            if (BuildingManager.isBuildDemolishMode)
            {
                nowBuildingBlock.RemoveDemolishedBuildItem();
            }

            if (BuildingManager.isBuildMode)
            {
                if (OnBuildItemDrag)
                {
                    ScaleBuildItem();
                }
            }

        }
    }
}
public enum BuildState { NotFinish, Finish }

public enum BuildMode {None,BuildHouseMode,DemolishMode }
