using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Cam;
using TT.BuildSystem;
using UnityEngine.UI;

namespace TT.Test
{
    public class UIOnOff : MonoBehaviour
    {
        [SerializeField] GameObject[] UIList;
       // public Button[] BuildMenuButtonList;
        [HideInInspector] public Transform CurBuilding;
        [SerializeField] Vector3 SetOffsetPos;
        [HideInInspector]
        public bool IsBuildMode;
        CameraManager CamManager;
        BuildItemInventorySlot InventorySlot;
        void Start()
        {
            InventorySlot = FindObjectOfType<BuildItemInventorySlot>();
            CamManager = FindObjectOfType<CameraManager>();
            IsBuildMode = false;
        }
        void Update()
        {

        }

        public void TurnOnUI(int UINum)
        {
            UIList[UINum].SetActive(true);
        }

        public void TurnOffUI(int UINum)
        {
            UIList[UINum].SetActive(false);
        }

        public void CloseBuildMenu(BuildState curblockbuildState)
        {
            //BuildingBlock CurBlock = CurBuilding.GetComponent<BuildingBlock>();
            //BuildAreaObject CurBuildObj = CurBuilding.GetComponent<BuildAreaObject>();
            UIList[1].SetActive(false);
           if(IsBuildMode)
            {
                if (curblockbuildState == BuildState.NotFinish)
                {
                    TurnOnUI(4);
                    CamManager.DeactiveSubCamera(1);
                    CamManager.ActiveSubCamera(3);
                }
                else
                {
                    CurBlock.ExitBuildMode();
                }
            }
            
        }

        public void BtnBuildBuilding()
        {
            BuildingBlock CurBlock = CurBuilding.GetComponent<BuildingBlock>();
            CurBlock.BuildBuilding();
        }
        public void BtnDemolishBuilding()
        {
            BuildingBlock CurBlock = CurBuilding.GetComponent<BuildingBlock>();
            CurBlock.DemolishBuidling();
        }
        public void BtnCompleteBuilding()
        {
            BuildingBlock CurBlock = CurBuilding.GetComponent<BuildingBlock>();
            CurBlock.CompleteBuilding();
        }
        public void BtnCompleteandExit()
        {
            BuildingBlock CurBlock = CurBuilding.GetComponent<BuildingBlock>();
            TurnOffUI(4);
            CurBlock.CompleteBuilding();
            CurBlock.ExitBuildMode();
        }
        public void BtnReturntoBuildMode()
        {
            BuildingBlock CurBlock = CurBuilding.GetComponent<BuildingBlock>();
            TurnOffUI(4);
            CurBlock.BuildBuilding();
          
            CamManager.DeactiveSubCamera(3);
            CamManager.ActiveSubCamera(1);
            InventorySlot.SetInventoryPos(SetOffsetPos);
        }

    }
}

