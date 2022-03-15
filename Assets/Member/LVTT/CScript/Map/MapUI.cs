using Game.Cam;
using NSY.Player;
using UnityEngine;
namespace TT.MapTravel
{
    public class MapUI : MonoBehaviour
    {
       // [SerializeField] GameObject MapTravelUI;
        //bool MapUIisOn;
        PlayerMoveMent PlayerMovement;
        CameraManager CamManager;
        void Start()
        {
            //MapUIisOn = false;
            PlayerMovement = FindObjectOfType<PlayerMoveMent>();
            CamManager = FindObjectOfType<CameraManager>();
        }
        void Update()
        {
          
        }

        // void OpenandCloseMapUI()
        //{
        //    switch (MapUIisOn)
        //    {
        //        case true:
        //            MapTravelUI.SetActive(false);
        //            MapUIisOn = false;
        //            break;
        //        case false:
        //            MapTravelUI.SetActive(true);
        //            MapUIisOn = true;
        //            break;
        //    }

        //}

        public void BtnTravelToArea(int areaNum)
        {
           // OpenandCloseMapUI();
            print("BtnTravelToOuterArea" + areaNum);
            PlayerData.AddValue(areaNum, (int)LocationBehaviorEnum.Interact, PlayerData.locationData, (int)LocationBehaviorEnum.length);
            PlayerMovement.curAreaNum = areaNum;
            PlayerMovement.Maptravel = true;
        }

        //////Outer////
        //public void BtnTravelToOuterArea1()
        //{
        //   // OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 0;
        //    PlayerMovement.Maptravel = true;
        //}

        //public void BtnTravelToOuterArea2()
        //{
        //   // OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 1;
        //    PlayerMovement.Maptravel = true;
        //}

        //public void BtnTravelToOuterArea3()
        //{
        //    //OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 2;
        //    PlayerMovement.Maptravel = true;
        //}

        //public void BtnTravelToOuterArea4()
        //{
        //   // OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 3;
        //    PlayerMovement.Maptravel = true;
        //}

        //public void BtnTravelToOuterArea5()
        //{
        //   // OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 4;
        //    PlayerMovement.Maptravel = true;
        //}

        //public void BtnTravelToOuterArea6()
        //{
        //   // OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 5;
        //    PlayerMovement.Maptravel = true;
        //}

        //public void BtnTravelToOuterArea7()
        //{
        //    //OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 6;
        //    PlayerMovement.Maptravel = true;
        //}

        //public void BtnTravelToOuterArea8()
        //{
        //   // OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 7;
        //    PlayerMovement.Maptravel = true;
        //}
        //////Inner///////////////////////////////////////////////////////////////////////////////
        //public void BtnTravelToInnerArea6()
        //{
        //    //OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 13;
        //    PlayerMovement.Maptravel = true;
        //}

        //public void BtnTravelToInnerArea3()
        //{
        //   // OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 10;
        //    PlayerMovement.Maptravel = true;
        //}
        //public void BtnTravelToInnerArea12()
        //{
        //   // OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 18;
        //    PlayerMovement.Maptravel = true;
        //}
        //public void BtnTravelToInnerArea4()
        //{
        //   // OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 11;
        //    PlayerMovement.Maptravel = true;
        //}
        //public void BtnTravelToInnerArea9()
        //{
        //   // OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 16;
        //    PlayerMovement.Maptravel = true;
        //}
        //public void BtnTravelToInnerArea10()
        //{
        //   // OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 17;
        //    PlayerMovement.Maptravel = true;
        //}
        //public void BtnTravelToInnerArea7()
        //{
        //    //OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 14;
        //    PlayerMovement.Maptravel = true;
        //}
        //public void BtnTravelToInnerArea8()
        //{
        //    //OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 15;
        //    PlayerMovement.Maptravel = true;
        //}
        //public void BtnTravelToInnerArea2()
        //{
        //   // OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 9;
        //    PlayerMovement.Maptravel = true;
        //}
        //public void BtnTravelToInnerArea5()
        //{
        //   // OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 12;
        //    PlayerMovement.Maptravel = true;
        //}
        //public void BtnTravelToInnerArea1()
        //{
        //    //OpenandCloseMapUI();
        //    PlayerMovement.curAreaNum = 8;
        //    PlayerMovement.Maptravel = true;
        //}
    }
}

