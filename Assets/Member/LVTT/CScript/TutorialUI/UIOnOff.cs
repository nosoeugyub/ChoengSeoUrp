using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Cam;
using TT.BuildSystem;

namespace TT.Test
{
    public class UIOnOff : MonoBehaviour
    {
        [SerializeField] GameObject[] UIList;

        CameraManager CamManager;
        [HideInInspector]
        public bool IsBuildMode;
        void Start()
        {
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

        public void CloseBuildMenu()
        {
            UIList[1].SetActive(false);
           if(IsBuildMode)
            {
                CamManager.DeactiveSubCamera(1);
                CamManager.ActiveSubCamera(3);
            }
            
        }
    }
}

