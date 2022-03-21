using System.Collections.Generic;
using UnityEngine;
using TT.Test;
using Game.Cam;

namespace TT.BuildSystem
{
    public class BuildingBlock : MonoBehaviour
    {
        public GameObject Player;
        public Transform HouseBuild;
        //BuildItemObj
        public List <GameObject> BuildItemList;
        //BuildBlock Obj
        public GameObject [] BuildBlockObjList;
        //BuildModeCheck
        bool BuildModeTrigger;
        [HideInInspector]
        public bool BuildMode;
        [HideInInspector]
        public float CurFrontItemzPos ;
        [HideInInspector]
        public float MaxBackItemzPos;
        bool Complete;
        
        CameraManager CamManager;
        UIOnOff TheUI;
        BuildItemInventorySlot SlotManager;
        BuildAreaObject BuildAreaObj;
        BuildingManager BuildManager;

        
        void Start()
        {
            CamManager = FindObjectOfType<CameraManager>();
            TheUI = FindObjectOfType<UIOnOff>();
            SlotManager = FindObjectOfType<BuildItemInventorySlot>();
            BuildAreaObj = GetComponent<BuildAreaObject>();
            BuildManager = FindObjectOfType<BuildingManager>();
            BuildModeTrigger = false;
            BuildMode = false;
            Complete = false;
            
        }

        // Update is called once per frame
        void Update()
        {
            
            var input = Input.inputString;
            if (BuildModeTrigger)
            {
               
                switch (Complete)
                {
                    case false:
                        {

                            switch (input)
                            {
                                case "1":
                                    BuildBuilding();
                                    break;
                                case "2":
                                    //Comfirm before erase
                                    //Erase everything
                                    DemolishBuidling();
                                    break;
                                case "3":
                                    CompleteBuilding();
                                    break;
                            }
                        }
                        break;
                    case true:
                        // var input = Input.inputString;
                        switch (input)
                        {
                            case "1":
                                BuildBuilding();
                                break;
                            case "2":
                                DemolishBuidling();
                                break;

                        }
                        break;

                }

            }
           
            if(BuildMode)
            {
                /////OnRightClick-->ExitBuildMode/////
                //if (Input.GetMouseButtonDown(1))
                //{
                //    BuildMode = false;
                //    TheUI.IsBuildMode = false;
                //    //BuildHouse = false;
                //    Player.SetActive(true);
                //    TheUI.TurnOffUI(1);
                //    CamManager.DeactiveSubCamera(1);
                //    CamManager.DeactiveSubCamera(2);
                //    CamManager.DeactiveSubCamera(3);
                //    UnViewObject(0);
                //    UnViewObject(2);
                //    ViewObject(1);
                //}
                ///////////////
            }
             
        }

        private void FixedUpdate()
        {
            UpdateBuildingState();
        }
        void UpdateBuildingState()
        {
            BuildingBlock CurBlock = gameObject.GetComponent<BuildingBlock>();
            if(CurBlock.Complete)
            {
                BuildAreaObj.SetBuildingState(BuildState.Finish);
            }
            else
            {
                BuildAreaObj.SetBuildingState(BuildState.NotFinish);
            }
        
        }
        ////////////////////////////////////////////////////////
        private void OnTriggerEnter(Collider col)
        {

            if (col.gameObject.tag == "Player")
            {
                BuildingBlock CurBlock = gameObject.GetComponent<BuildingBlock>();
                TheUI.CurBuilding = gameObject.transform;
                BuildAreaObj.CanInteract();
                BuildModeTrigger=true;
                BuildManager.CurBuilding = gameObject.transform;
                CamManager.ChangeFollowTarger(gameObject.transform, 1);
                CamManager.ChangeFollowTarger(gameObject.transform, 2);
                CamManager.ChangeFollowTarger(gameObject.transform, 3);
                SlotManager.AssignBuildItemSpawnPos(CurBlock.HouseBuild,CurBlock.gameObject.transform);
                TheUI.TurnOnUI(0);
                if (!CurBlock.Complete)
                { TheUI.TurnOnUI(2);
                    TheUI.TurnOffUI(3);
                }
                else if(CurBlock.Complete)
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
        ////////////////////////////////////////////////////////
    }
}

