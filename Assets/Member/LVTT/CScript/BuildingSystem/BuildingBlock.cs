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
        //BuildBlock Obj
        public GameObject [] BuildBlockObjList;
        //BuildModeCheck
        bool BuildModeTrigger;
        bool BuildMode;
        bool BuildHouse;
       
        
        CameraManager CamManager;
        UIOnOff TheUI;
       
        void Start()
        {
            CamManager = FindObjectOfType<CameraManager>();
            TheUI = FindObjectOfType<UIOnOff>();
            BuildModeTrigger = false;
            BuildMode = false;
            BuildHouse = false;
        }

        // Update is called once per frame
        void Update()
        {
            
            if(BuildModeTrigger)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    BuildModeTrigger = false;
                    BuildMode = true;
                    BuildHouse = true;
                    //Deactive Player
                    Player.SetActive(false);
                    //TurnOff CanInteractUI
                    TheUI.TurnOffUI(0);
                    //TurnOn Inventory UI
                    TheUI.TurnOnUI(1);
                    //Change to BuildHouseView
                    CamManager.ActiveSubCamera(1);
                    //TurnOn Guide Square
                    ViewObject(1);
                    //TurnOff Fence
                    UnViewObject(0);
                }
            }
            //Obj List
            //0 = Fence, 1/2=GuideHouseObj Blue/Red,3/4=GuideGradenObj Blue/Red
            if(BuildMode)
            {
                if(BuildHouse)
                {
                    if (Input.GetKeyDown("2"))
                    {
                        BuildHouse = false;
                        CamManager.ActiveSubCamera(2);
                        CamManager.DeactiveSubCamera(1);
                        ViewObject(2);
                        ViewObject(3);
                        UnViewObject(0);
                        UnViewObject(1);
                        UnViewObject(4);
                    }
                }
                else if(!BuildHouse)
                {
                    if (Input.GetKeyDown("1"))
                    {
                        BuildHouse = true;
                        CamManager.ActiveSubCamera(1);
                        CamManager.DeactiveSubCamera(2);
                        ViewObject(1);
                        //ViewObject(4);
                        UnViewObject(0);
                        UnViewObject(2);
                        UnViewObject(3);
                    }
                }
                if (Input.GetMouseButtonDown(1))
                {
                    BuildMode = false;
                    BuildHouse = false;
                    Player.SetActive(true);
                    TheUI.TurnOffUI(1);
                    CamManager.DeactiveSubCamera(1);
                    CamManager.DeactiveSubCamera(2);
                    ViewObject(0);
                    UnViewObject(1);
                    UnViewObject(2);
                    UnViewObject(3);
                   // UnViewObject(4);
                }
            }
            

            
        }

        private void OnTriggerEnter(Collider col)
        {

            if (col.gameObject.tag == "Player")
            {
                BuildModeTrigger=true;
                TheUI.TurnOnUI(0);
                CamManager.ChangeFollowTarger(gameObject.transform, 1);
                CamManager.ChangeFollowTarger(gameObject.transform, 2);
            }
        }
        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.tag == "Player")
            {
                BuildModeTrigger = false;
                TheUI.TurnOffUI(0);
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

