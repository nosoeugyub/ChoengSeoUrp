using UnityEngine;
using NSY.Player;

namespace TT.MapTravel
{
    public class MapUI : MonoBehaviour
    {
        [SerializeField] GameObject MapTravelUI;
        bool MapUIisOn;
        PlayerMoveMent PlayerMovement;
        // Start is called before the first frame update
        void Start()
        {
            MapUIisOn = false;
            PlayerMovement = FindObjectOfType<PlayerMoveMent>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                OpenandCloseMapUI();
            }
        }

       public void OpenandCloseMapUI()
        {
            switch (MapUIisOn)
            {
                case true:
                    MapTravelUI.SetActive(false);
                    MapUIisOn = false;
                    break;
                case false:
                    MapTravelUI.SetActive(true);
                    MapUIisOn = true;
                    break;
            }

        }

        ////Outer////
        public void BtnTravelToOuterArea1()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 0;
            PlayerMovement.Maptravel = true;
        }

        public void BtnTravelToOuterArea2()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 1;
            PlayerMovement.Maptravel = true;
        }

        public void BtnTravelToOuterArea3()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 2;
            PlayerMovement.Maptravel = true;
        }

        public void BtnTravelToOuterArea4()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 3;
            PlayerMovement.Maptravel = true;
        }

        public void BtnTravelToOuterArea5()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 4;
            PlayerMovement.Maptravel = true;
        }

        public void BtnTravelToOuterArea6()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 5;
            PlayerMovement.Maptravel = true;
        }

        public void BtnTravelToOuterArea7()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 6;
            PlayerMovement.Maptravel = true;
        }

        public void BtnTravelToOuterArea8()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 7;
            PlayerMovement.Maptravel = true;
        }
        ////Inner////
        public void BtnTravelToInnerArea1()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 8;
            PlayerMovement.Maptravel = true;
        }

        public void BtnTravelToInnerArea2()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 9;
            PlayerMovement.Maptravel = true;
        }
        public void BtnTravelToInnerArea3()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 10;
            PlayerMovement.Maptravel = true;
        }
        public void BtnTravelToInnerArea4()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 11;
            PlayerMovement.Maptravel = true;
        }
        public void BtnTravelToInnerArea5()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 12;
            PlayerMovement.Maptravel = true;
        }
        public void BtnTravelToInnerArea6()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 13;
            PlayerMovement.Maptravel = true;
        }
        public void BtnTravelToInnerArea7()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 14;
            PlayerMovement.Maptravel = true;
        }
        public void BtnTravelToInnerArea8()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 15;
            PlayerMovement.Maptravel = true;
        }
        public void BtnTravelToInnerArea9()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 16;
            PlayerMovement.Maptravel = true;
        }
        public void BtnTravelToInnerArea10()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 17;
            PlayerMovement.Maptravel = true;
        }
        public void BtnTravelToInnerArea11()
        {
            OpenandCloseMapUI();
            PlayerMovement.curAreaNum = 18;
            PlayerMovement.Maptravel = true;
        }
    }
}

