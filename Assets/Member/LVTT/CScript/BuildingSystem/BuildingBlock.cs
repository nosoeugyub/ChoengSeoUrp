using System.Collections;
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
        //BuildBlock Obj
        public GameObject [] BuildBlockObjList;
        //BuildModeCheck
        bool BuildModeTrigger;
        [HideInInspector]
        public bool BuildMode;
        bool Complete;
        
        CameraManager CamManager;
        UIOnOff TheUI;
        BuildItemInventorySlot SlotManager;
        void Start()
        {
            CamManager = FindObjectOfType<CameraManager>();
            TheUI = FindObjectOfType<UIOnOff>();
            SlotManager = FindObjectOfType<BuildItemInventorySlot>();
            BuildModeTrigger = false;
            BuildMode = false;
            Complete = false;
        }

        // Update is called once per frame
        void Update()
        {
            
            if(BuildModeTrigger)
            {
                var input = Input.inputString;
                switch (Complete)
                {
                    case false:
                        {
                           
                            switch (input)
                            {
                                case "1":
                                    BuildModeTrigger = false;
                                    BuildMode = true;
                                    TheUI.IsBuildMode = true;
                                    //BuildHouse = true;
                                    //Deactive Player
                                    Player.SetActive(false);
                                    //TurnOff CanInteractUI
                                    TheUI.TurnOffUI(0);
                                    //TurnOn Inventory UI
                                    TheUI.TurnOnUI(1);
                                    //Change to BuildHouseView
                                    CamManager.ActiveSubCamera(1);
                                    //TurnOn Guide Square
                                    ViewObject(0);
                                    UnViewObject(1);
                                    break;
                                case "2":
                                    BuildModeTrigger = false;
                                    //TurnOff CanInteractUI
                                    TheUI.TurnOffUI(0);
                                    //Comfirm before erase
                                    //Erase everything
                                    DemolishBuidling();
                                    break;
                                case "3":
                                    BuildModeTrigger = false;
                                    //TurnOff CanInteractUI
                                    TheUI.TurnOffUI(0);
                                    //Change BuildingState to Complete
                                    Complete = true;
                                    break;
                            }
                        }
                        break;
                            
                        
                        //if (Input.GetMouseButtonDown(0))
                        //{
                        //    BuildModeTrigger = false;
                        //    BuildMode = true;
                        //    //BuildHouse = true;
                        //    //Deactive Player
                        //    Player.SetActive(false);
                        //    //TurnOff CanInteractUI
                        //    TheUI.TurnOffUI(0);
                        //    //TurnOn Inventory UI
                        //    TheUI.TurnOnUI(1);
                        //    //Change to BuildHouseView
                        //    CamManager.ActiveSubCamera(1);
                        //    //TurnOn Guide Square
                        //    ViewObject(0);
                        //    UnViewObject(1);
                        //}
                       
                    case true:
                       // var input = Input.inputString;
                        switch (input)
                        {
                            case "1":
                                BuildModeTrigger = false;
                                BuildMode = true;
                                //BuildHouse = true;
                                //Deactive Player
                                Player.SetActive(false);
                                //TurnOff CanInteractUI
                                TheUI.TurnOffUI(0);
                                //TurnOn Inventory UI
                                TheUI.TurnOnUI(1);
                                //Change to BuildHouseView
                                CamManager.ActiveSubCamera(1);
                                //TurnOn Guide Square
                                ViewObject(0);
                                UnViewObject(1);
                                break;
                            case "2":
                                BuildModeTrigger = false;
                                //TurnOff CanInteractUI
                                TheUI.TurnOffUI(1);
                                Complete = false;
                                //Comfirm before erase
                                //Erase everything
                                DemolishBuidling();
                                break;

                        }
                        break;

                }

            }
           
            if(BuildMode)
            {
                //if(BuildHouse)
                //{
                    //if (Input.GetKeyDown("2"))
                    //{
                    //    BuildHouse = false;
                    //    CamManager.ActiveSubCamera(2);
                    //    CamManager.DeactiveSubCamera(1);
                    //    ViewObject(2);
                    //    ViewObject(3);
                    //   // UnViewObject(0);
                    //    UnViewObject(1);
                    //    UnViewObject(4);
                    //}
                //}
                //else if(!BuildHouse)
                //{
                //    if (Input.GetKeyDown("1"))
                //    {
                //        BuildHouse = true;
                //        CamManager.ActiveSubCamera(1);
                //        CamManager.DeactiveSubCamera(2);
                //        ViewObject(1);
                //        //ViewObject(4);
                //        UnViewObject(0);
                //        UnViewObject(2);
                //        UnViewObject(3);
                //    }
                //}
                if (Input.GetMouseButtonDown(1))
                {
                    BuildMode = false;
                    TheUI.IsBuildMode = false;
                    //BuildHouse = false;
                    Player.SetActive(true);
                    TheUI.TurnOffUI(1);
                    CamManager.DeactiveSubCamera(1);
                    CamManager.DeactiveSubCamera(2);
                    CamManager.DeactiveSubCamera(3);
                    UnViewObject(0);
                    ViewObject(1);
                }
            }
            

            
        }

       void DemolishBuidling()
        {
           foreach(Transform child in HouseBuild)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        private void OnTriggerEnter(Collider col)
        {

            if (col.gameObject.tag == "Player")
            {
                BuildingBlock CurBlock = gameObject.GetComponent<BuildingBlock>();
                BuildModeTrigger=true;
                CamManager.ChangeFollowTarger(gameObject.transform, 1);
                CamManager.ChangeFollowTarger(gameObject.transform, 2);
                CamManager.ChangeFollowTarger(gameObject.transform, 3);
                SlotManager.AssignBuildItemSpawnPos(CurBlock.HouseBuild);
                if (!CurBlock.Complete)
                { TheUI.TurnOnUI(0); }
                else if(CurBlock.Complete)
                {
                    TheUI.TurnOnUI(2);
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

        void ViewObject(int ObjNum)
        {
            BuildBlockObjList[ObjNum].SetActive(true);
        }

        void UnViewObject(int ObjNum)
        {
            BuildBlockObjList[ObjNum].SetActive(false);
        }
    }
}

