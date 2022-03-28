using Game.Cam;
using TT.Test;
using UnityEngine;



namespace TT.BuildSystem
{
    public class BuildingManager : MonoBehaviour
    {
        //[SerializeField] public Transform CurBuilding;
        public GameObject Player;
        public float GuideObjOffsetY;
        [HideInInspector]
        public int BuildItemDragIndex = 0;
        [HideInInspector]
        public bool OnBuildItemDrag = false;

        CameraManager CamManager;
        BuildItemInventorySlot SlotManager;
        UIOnOff TheUI;

        //BuildBlock Obj
        public GameObject[] BuildBlockObjList;
        public BuildingBlock nowBuildingBlock;

        //BuildItemObj
        public BuildingItemObj curDragObj;
        public float BuildItemScaleVar=0.2f;
       // public GameObject SpawnBuildItem;

        public bool isBuildMode=false;
        private void Awake()
        {
            TheUI = FindObjectOfType<UIOnOff>();
            SlotManager = FindObjectOfType<BuildItemInventorySlot>();
            CamManager = FindObjectOfType<CameraManager>();
        }

        
        public void BuildModeOn(BuildingBlock buildingBlock, UnityEngine.UI.Button[] buttons,GameObject interactUI)
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

            TheUI.IsBuildMode = true;
            isBuildMode = true;
            TheUI.TurnOffUI(0);
            TheUI.TurnOnUI(1);

            CamManager.ActiveSubCamera(1);

            Player.SetActive(false);

            //SlotManager.MoveInventToRight();

            ViewGuideObject(0);
            ViewGuideObject(2);
            UnViewGuideObject(1);

         
        }

        public void BuildModeOff()
        {
            TheUI.TurnOffUI(1);
            TheUI.IsBuildMode = false;
            isBuildMode = false;
           
            CamManager.DeactiveSubCamera(1);
            CamManager.DeactiveSubCamera(2);
            CamManager.DeactiveSubCamera(3);

            Player.SetActive(true);

            //SlotManager.ResetInventPos();

            UnViewGuideObject(0);
            UnViewGuideObject(2);
        }

        void ViewGuideObject(int ObjNum)
        {
            BuildBlockObjList[ObjNum].SetActive(true);
            Vector3 GuidePos = nowBuildingBlock.HouseBuild.transform.position;
            GuidePos.y =GuidePos.y+GuideObjOffsetY;
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
                //Debug.Log("Mouse is Scrolling up");
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                Vector3 var = curDragObj.transform.localScale;
                var.x -= BuildItemScaleVar;
                var.y -= BuildItemScaleVar;
                curDragObj.SetBuildItemScale(var);
                //Debug.Log("Mouse is Scrolling down");
            }
        }    
        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    //건축자재 생성메서드
            //    GameObject newPrefab = Instantiate(SpawnBuildItem, nowBuildingBlock.HouseBuild.transform.position, Quaternion.identity, nowBuildingBlock.HouseBuild.transform);
            //    newPrefab.name = SpawnBuildItem.name;
            //    nowBuildingBlock.AddBuildItemToList(newPrefab);
            //    nowBuildingBlock.CurFrontItemzPos = nowBuildingBlock.HouseBuild.transform.position.z;
            //}

            if(OnBuildItemDrag)
            {
                ScaleBuildItem();
            }
        }
    }
}
public enum BuildState { NotFinish, Finish }
