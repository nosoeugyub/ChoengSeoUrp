using Game.Cam;
using System.Collections.Generic;
using TT.Test;
using UnityEngine;

namespace TT.BuildSystem
{
    public class BuildingBlock : MonoBehaviour, IInteractable
    {
        public GameObject Player;
        public Transform HouseBuild;
        //BuildItemObj
        public List<GameObject> BuildItemList;
        //BuildBlock Obj
        public GameObject[] BuildBlockObjList;
        //BuildModeCheck
        bool BuildModeTrigger;
        [HideInInspector]
        public bool BuildMode;
        [HideInInspector]
        public float CurFrontItemzPos;
        [HideInInspector]
        public float MaxBackItemzPos;
        bool Complete;

        CameraManager CamManager;
        UIOnOff TheUI;
        BuildItemInventorySlot SlotManager;
        //BuildAreaObject BuildAreaObj;
        BuildingManager BuildManager;

        [SerializeField] public BuildState buildState;
        [SerializeField] int buildingId;
        [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
        [SerializeField] InItemType toolType;
        void Start()
        {
            CamManager = FindObjectOfType<CameraManager>();
            TheUI = FindObjectOfType<UIOnOff>();
            SlotManager = FindObjectOfType<BuildItemInventorySlot>();
            //BuildAreaObj = GetComponent<BuildAreaObject>();
            BuildManager = FindObjectOfType<BuildingManager>();
            BuildModeTrigger = false;
            BuildMode = false;
            Complete = false;

        }

        // Update is called once per frame
        //void Update()
        //{

        //    var input = Input.inputString;
        //    if (BuildModeTrigger)
        //    {

        //        switch (Complete)
        //        {
        //            case false:
        //                {

        //                    switch (input)
        //                    {
        //                        case "1":
        //                            BuildBuilding();
        //                            break;
        //                        case "2":
        //                            //Comfirm before erase
        //                            //Erase everything
        //                            DemolishBuidling();
        //                            break;
        //                        case "3":
        //                            CompleteBuilding();
        //                            break;
        //                    }
        //                }
        //                break;
        //            case true:
        //                // var input = Input.inputString;
        //                switch (input)
        //                {
        //                    case "1":
        //                        BuildBuilding();
        //                        break;
        //                    case "2":
        //                        DemolishBuidling();
        //                        break;

        //                }
        //                break;

        //        }

        //    }

        //    if (BuildMode)
        //    {
        //        /////OnRightClick-->ExitBuildMode/////
        //        //if (Input.GetMouseButtonDown(1))
        //        //{
        //        //    BuildMode = false;
        //        //    TheUI.IsBuildMode = false;
        //        //    //BuildHouse = false;
        //        //    Player.SetActive(true);
        //        //    TheUI.TurnOffUI(1);
        //        //    CamManager.DeactiveSubCamera(1);
        //        //    CamManager.DeactiveSubCamera(2);
        //        //    CamManager.DeactiveSubCamera(3);
        //        //    UnViewObject(0);
        //        //    UnViewObject(2);
        //        //    ViewObject(1);
        //        //}
        //        ///////////////
        //    }

        //}

        private void FixedUpdate()
        {
            UpdateBuildingState();
        }
        void UpdateBuildingState()
        {
            BuildingBlock CurBlock = gameObject.GetComponent<BuildingBlock>();
            if (CurBlock.Complete)
            {
                SetBuildingState(BuildState.Finish);
            }
            else
            {
                SetBuildingState(BuildState.NotFinish);
            }

        }
        ////////////////////////////////////////////////////////
        private void OnTriggerEnter(Collider col)
        {

            if (col.gameObject.tag == "Player")
            {
                BuildingBlock CurBlock = gameObject.GetComponent<BuildingBlock>();
                TheUI.CurBuilding = gameObject.transform;
                CanInteract();
                BuildModeTrigger = true;
                BuildManager.CurBuilding = gameObject.transform;
                CamManager.ChangeFollowTarger(gameObject.transform, 1);
                CamManager.ChangeFollowTarger(gameObject.transform, 2);
                CamManager.ChangeFollowTarger(gameObject.transform, 3);
                SlotManager.AssignBuildItemSpawnPos(CurBlock.HouseBuild, CurBlock.gameObject.transform);
                TheUI.TurnOnUI(0);
                if (!CurBlock.Complete)
                {
                    TheUI.TurnOnUI(2);
                    TheUI.TurnOffUI(3);
                }
                else if (CurBlock.Complete)
                {
                    TheUI.TurnOnUI(3);
                    TheUI.TurnOffUI(2);
                }

            }
        }


        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.tag == "Player")
            {
                BuildModeTrigger = false;
                TheUI.TurnOffUI(0);
                TheUI.TurnOffUI(2);
            }
        }
        ////////////////////////////////////////////////////////
        void ViewObject(int ObjNum)
        {
            BuildBlockObjList[ObjNum].SetActive(true);
        }
        void UnViewObject(int ObjNum)
        {
            BuildBlockObjList[ObjNum].SetActive(false);
        }
        ////////////////////////////////////////////////////////
        public void ExitBuildMode()
        {
            SlotManager.ResetInventPos();
            BuildMode = false;
            TheUI.IsBuildMode = false;
            Player.SetActive(true);
            TheUI.TurnOffUI(1);
            CamManager.DeactiveSubCamera(1);
            CamManager.DeactiveSubCamera(2);
            CamManager.DeactiveSubCamera(3);
            UnViewObject(0);
            UnViewObject(2);
            ViewObject(1);
        }
        public void DemolishBuidling()
        {
            BuildModeTrigger = false;
            TheUI.TurnOffUI(0);
            Complete = false;
            foreach (Transform child in HouseBuild)
            {
                GameObject.Destroy(child.gameObject);
            }
            ClearBuildItemList();
        }
        public void BuildBuilding()
        {
            SlotManager.MoveInventToRight();
            BuildModeTrigger = false;
            BuildMode = true;
            TheUI.IsBuildMode = true;
            Player.SetActive(false);
            TheUI.TurnOffUI(0);
            TheUI.TurnOnUI(1);
            CamManager.ActiveSubCamera(1);
            ViewObject(0);
            ViewObject(2);
            UnViewObject(1);
        }
        public void CompleteBuilding()
        {
            BuildModeTrigger = false;
            TheUI.TurnOffUI(0);
            Complete = true;
        }
        ////////////////////////////////////////////////////////
        public void AddBuildItemToList(GameObject Item)
        {
            BuildItemList.Add(Item);
        }

        void ClearBuildItemList()
        {
            BuildItemList.Clear();
        }
        public void OnBuildMode(UnityEngine.UI.Button[] buttons)
        {
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(true);
            }
            //건축물 상호작용 인덱스 체크
            Interact();
            //Set Event Methods
            if (buildState == BuildState.NotFinish)
            {
                buttons[0].onClick.AddListener(() =>
                {
                    BuildBuilding();
                    print("1. Build Building");
                    //1. Build Building
                    //ex) BuildMode = true; Like your script 'BuildingBlock'
                });
                buttons[1].onClick.AddListener(() =>
                {
                    DemolishBuidling();

                    print("2. break Building");
                    //2. break Building
                });
                buttons[2].onClick.AddListener(() =>
                {
                    CompleteBuilding();
                    print("3. Finish Building");
                    //3. Finish Building
                });
            }
            else if (buildState == BuildState.Finish)
            {
                buttons[0].onClick.AddListener(() =>
                {
                    BuildBuilding();
                    print("1. Repair Building");
                    //1. Repair Building
                });
                buttons[1].onClick.AddListener(() =>
                {
                    DemolishBuidling();
                    print("2. break Building");
                    //2. break Building
                });
                buttons[2].gameObject.SetActive(false);
            }
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

public enum BuildState { NotFinish, Finish }