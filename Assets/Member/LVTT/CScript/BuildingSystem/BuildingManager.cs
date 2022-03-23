using Game.Cam;
using TT.Test;
using UnityEngine;


namespace TT.BuildSystem
{
    public class BuildingManager : MonoBehaviour
    {
        //[SerializeField] public Transform CurBuilding;
        public GameObject Player;

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

        public GameObject SpawnBuildItem;


        private void Awake()
        {
            TheUI = FindObjectOfType<UIOnOff>();
            SlotManager = FindObjectOfType<BuildItemInventorySlot>();
            CamManager = FindObjectOfType<CameraManager>();
        }
        public void BuildModeOn(BuildingBlock buildingBlock)
        {
            nowBuildingBlock = buildingBlock;

            TheUI.CurBuilding = nowBuildingBlock.gameObject.transform;

            CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 1);
            CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 2);
            CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 3);

            SlotManager.AssignBuildItemSpawnPos(nowBuildingBlock.HouseBuild, nowBuildingBlock.gameObject.transform);

            TheUI.IsBuildMode = true;
            TheUI.TurnOffUI(0);
            TheUI.TurnOnUI(1);

            CamManager.ActiveSubCamera(1);

            Player.SetActive(false);

            SlotManager.MoveInventToRight();

            ViewObject(0);
            ViewObject(2);
            UnViewObject(1);
        }

        public void BuildModeOff()
        {
            TheUI.IsBuildMode = false;
            TheUI.TurnOffUI(1);

            CamManager.DeactiveSubCamera(1);
            CamManager.DeactiveSubCamera(2);
            CamManager.DeactiveSubCamera(3);

            Player.SetActive(true);

            SlotManager.ResetInventPos();

            UnViewObject(0);
            UnViewObject(2);
            ViewObject(1);
        }

        void ViewObject(int ObjNum)
        {
            BuildBlockObjList[ObjNum].SetActive(true);
            BuildBlockObjList[ObjNum].transform.position = nowBuildingBlock.transform.position;
        }
        void UnViewObject(int ObjNum)
        {
            BuildBlockObjList[ObjNum].SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //건축자재 생성메서드
                GameObject newPrefab = Instantiate(SpawnBuildItem, nowBuildingBlock.HouseBuild.transform.position, Quaternion.identity, nowBuildingBlock.HouseBuild.transform);
                newPrefab.name = SpawnBuildItem.name;
                nowBuildingBlock.AddBuildItemToList(newPrefab);
                nowBuildingBlock.CurFrontItemzPos = nowBuildingBlock.HouseBuild.transform.position.z;
            }
        }
    }
}
public enum BuildState { NotFinish, Finish }
