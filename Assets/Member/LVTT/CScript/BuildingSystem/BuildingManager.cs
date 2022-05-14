//using Game.Cam;
//using TT.Test;
//using NSY.Iven;
//using UnityEngine;



//namespace TT.BuildSystem
//{
//    public enum BuildItemKind {  Wall, Roof, Door, Window, Signboard, Etc }

//    public class BuildingManager : MonoBehaviour
//    {
//        //public BuildMode CurBuildMode;
//        //[SerializeField] public Transform CurBuilding;
//        public GameObject Player;
//        //GuideObj
//        public float GuideObjOffsetY;
//        [SerializeField] Transform GuideObjCorner;
//        [SerializeField] Transform GuideObj;
//        [HideInInspector]
//        public float HalfGuideObjWidth;
//        [HideInInspector]
//        public float HalfGuideObjHeight;

//        public InventoryNSY inventoryNSY;

//        [HideInInspector]
//        public int BuildItemDragIndex = 0;
//        [HideInInspector]
//        public bool OnBuildItemDrag = false;

//        //CameraManager CamManager;
//        //UIOnOff TheUI;
//        BuildItemInventorySlot SlotManager;

//        //BuildItemObj
//        public BuildingItemObj curDragObj;
//        public float BuildItemScaleVar = 0.1f;

//        //BuildBlock Obj
//        public GameObject[] BuildBlockObjList;
//        public BuildingBlock nowBuildingBlock;

//        // public GameObject SpawnBuildItem;

//        //public static bool isBuildMode = false;
//        //public static bool isBuildDemolishMode = false;
//        private void Awake()
//        {
//            //TheUI = FindObjectOfType<UIOnOff>();
//            //CamManager = FindObjectOfType<CameraManager>();
//            SlotManager = FindObjectOfType<BuildItemInventorySlot>();
//            inventoryNSY = FindObjectOfType<InventoryNSY>();

//        }

//        private void Start()
//        {
//            GuideObjCal();
//            //SetBuildMode(BuildMode.None);
//        }

//        //public void BuildModeOn(BuildingBlock buildingBlock, UnityEngine.UI.Button[] buttons, GameObject interactUI)
//        //{
//        //    foreach (var button in buttons)
//        //    {
//        //        button.gameObject.SetActive(false);
//        //        button.onClick.RemoveAllListeners();
//        //    }

//        //    buildingBlock.buildButtonFuncAdded = false;
//        //    interactUI.SetActive(false);
//        //    nowBuildingBlock = buildingBlock;

//        //    TheUI.CurBuilding = nowBuildingBlock.gameObject.transform;

//        //    CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 1);
//        //    CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 2);
//        //    CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 3);

//        //    TheUI.IsBuildMode = true;
//        //    SetBuildMode(BuildMode.BuildHouseMode);

//        //    isBuildMode = true;
//        //    TheUI.TurnOffUI(0);
//        //    TheUI.TurnOnUI(1);

//        //    CamManager.ActiveSubCamera(1);

//        //    Player.SetActive(false);

//        //    ViewGuideObject(0);
//        //    ViewGuideObject(2);
//        //    UnViewGuideObject(1);

//        //    inventoryNSY.CheckCanBuildItem(nowBuildingBlock);
//        //}

//        //public void BuildDemolishModeOn(BuildingBlock buildingBlock, UnityEngine.UI.Button[] buttons, GameObject interactUI)
//        //{
//        //    foreach (var button in buttons)
//        //    {
//        //        button.gameObject.SetActive(false);
//        //        button.onClick.RemoveAllListeners();
//        //    }

//        //    buildingBlock.buildButtonFuncAdded = false;
//        //    interactUI.SetActive(false);
//        //    nowBuildingBlock = buildingBlock;

//        //    TheUI.CurBuilding = nowBuildingBlock.gameObject.transform;

//        //    CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 1);
//        //    CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 2);
//        //    CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 3);

//        //    isBuildDemolishMode = true;
//        //    SetBuildMode(BuildMode.DemolishMode);

//        //    TheUI.TurnOnUI(0);

//        //    CamManager.ActiveSubCamera(1);

//        //    Player.SetActive(false);

//        //    ViewGuideObject(0);
//        //    ViewGuideObject(2);
//        //    UnViewGuideObject(1);
//        //}

//        //public void BuildModeOff()
//        //{
//        //    isBuildMode = false;
//        //    isBuildDemolishMode = false;
//        //    TheUI.TurnOffUI(0);
//        //    SetBuildMode(BuildMode.None);
//        //    CamManager.DeactiveSubCamera(1);
//        //    CamManager.DeactiveSubCamera(2);
//        //    CamManager.DeactiveSubCamera(3);

//        //    Player.SetActive(true);

//        //    //UnViewGuideObject(0);
//        //    //UnViewGuideObject(2);

//        //    inventoryNSY.CheckCanBuildItem(null);
//        //}

//        //void ViewGuideObject(int ObjNum)
//        //{
//        //    BuildBlockObjList[ObjNum].SetActive(true);
//        //    Vector3 GuidePos = nowBuildingBlock.HouseBuild.transform.position;
//        //    GuidePos.y = GuidePos.y + GuideObjOffsetY;
//        //    BuildBlockObjList[ObjNum].transform.position = GuidePos;
//        //}
//        //void UnViewGuideObject(int ObjNum)
//        //{
//        //    BuildBlockObjList[ObjNum].SetActive(false);
//        //}

    
//        //public void SetBuildMode(BuildMode buildmode)
//        //{
//        //    CurBuildMode = buildmode;
//        //    Debug.Log("curBuildMode is" + buildmode);
//        //}
//        void GuideObjCal()
//        {
//            // Debug.Log("GuideObjCalculate");
//            HalfGuideObjHeight = GuideObjCorner.transform.position.y - GuideObj.transform.position.y;
//            HalfGuideObjWidth = GuideObjCorner.transform.position.x - GuideObj.transform.position.x;
//            //Debug.Log(HalfGuideObjHeight);
//            //Debug.Log(HalfGuideObjWidth);
//        }
//        //private void Update()
//        //{
//        //    if (isBuildDemolishMode)
//        //    {
//        //        nowBuildingBlock.RemoveDemolishedBuildItem();
//        //    }

//        //    if (isBuildMode)
//        //    {
//        //        if (OnBuildItemDrag)
//        //        {
//        //            ScaleBuildItem();
//        //        }
//        //    }

//        //    if(Input.GetKeyDown(KeyCode.T))
//        //    { BuildModeOff(); }
//        //}
//    }
//}
//public enum BuildState { NotFinish, Finish }

//public enum BuildMode { None, BuildHouseMode, DemolishMode }
