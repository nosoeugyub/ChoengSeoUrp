using System;
using System.Collections.Generic;
using TT.BuildSystem;
using UnityEngine;
using UnityEngine.UI;

namespace DM.Building
{
    public class BuildingManager : MonoBehaviour
    {
        public List<BuildingBlock> buildings = new List<BuildingBlock>();
        [Space]
        public KeyCode scaleUpKey = KeyCode.W;
        public KeyCode scaleDownKey = KeyCode.S;
        public KeyCode rotateLeftKey = KeyCode.A;
        public KeyCode rotateRightKey = KeyCode.D;

        [SerializeField] Button[] buildingButtons;
        [SerializeField] Transform player;

        [SerializeField] GameObject cancleUi;
        [SerializeField] GameObject buildingTutorialImg;

        public void AddBuilding(BuildingBlock buildingBlock)
        {
            if (!buildings.Contains(buildingBlock))
            {
                buildingBlock.BuildingID = buildings.Count;
                buildings.Add(buildingBlock);
                Debug.Log(buildingBlock.BuildingID);
            }
        }
        public int GetId(BuildingBlock buildingBlock)
        {
            if (!buildings.Contains(buildingBlock))
            {
                return buildingBlock.BuildingID;

            }
            return -1;
        }
        public BuildingBlock GetNPCsHouse(int npctype)
        {
            foreach (var item in buildings)//
            {
                if (item.GetLivingChar() == null) continue;
                if ((int)item.GetLivingChar().GetCharacterType() == npctype)
                {
                    return item;
                }
            }
            return null;
        }
        public List<BuildingBlock> GetCompleteBuildings()
        {
            List<BuildingBlock> buildingBlocks = new List<BuildingBlock>();
            foreach (var block in FindObjectsOfType<BuildingBlock>())
            {
                if (block.IsCompleteBuilding())
                    buildingBlocks.Add(block);
            }
            return buildingBlocks;
        }
        public void BuildingInteractButtonOnOff(bool isOn)
        {
            foreach (var button in buildingButtons)
            {
                button.gameObject.SetActive(isOn);
            }
        }
        public void ResetButtonEvents()
        {
            foreach (var button in buildingButtons)
            {
                button.gameObject.SetActive(false);
                button.onClick.RemoveAllListeners();
            }
        }
        public void SetBuildButtonEvents(Action buildmodeOn, Action demomodeOn)
        {
            buildingButtons[0].onClick.AddListener(() =>
            {
                buildmodeOn();
                PlayerOnOff(false);
                ResetButtonEvents();
            });
            buildingButtons[1].onClick.AddListener(() =>
            {
                demomodeOn();
                PlayerOnOff(false);
                ResetButtonEvents();
            });
        }
        public void PlayerOnOff(bool isOn)
        {
            player.gameObject.SetActive(isOn);
        }
        public void CancleUIState(bool isOn)
        {
            cancleUi.SetActive(isOn);
        }
        public void TutoUIState(bool isOn)
        {
            buildingTutorialImg.SetActive(isOn);
        }
    }
}
public enum BuildVPos { Top, Mid, Bottom, Length }
public enum BuildHPos { Left, Right, Mid, Length }

public enum BuildSize { Small, Normal, Big, Length }
public enum BuildColor { None, Red, Orange, Yellow, Green, Blue, Mint, Pupple, White, Black, Pink, Length }
public enum BuildMaterial//타이어
{
    Wood, Stone, Paper, Iron, Gold, Silver, Rubber, Wax, Sand, Grass, Honey, Stick,
    Brick, StainedGlass, Herringbone, Plain, Slate, Truss, Kiwa, Thatched,Stroke,Cuty, Length
}
public enum BuildShape
{
    //Wall
    Square_Wall, Square_Width_Wall, Square_Height_Wall, Pentagon_Wall, Circle_Wall, Triangle_Wall,
    //Door
    Circle_Door = 1000, Square_Door,
    //Roof
    Laugh_Roof = 2000, Triangle_Roof, Square_Roof, Line_Roof,
    //Window
    Circle_Window = 3000, CircleTwo_Window, Square_Window, SquareTwo_Window, Rectangle_Width_Window, Rectangle_Height_Window,
    Triangle_Window, TriangleTwo_Window, Half_Circle_Window, Half_CircleTwo_Window, Ellipse_Window, Sunting_Window,
    //Etc
    Flower = 4000, Fence, PostBox, Fountain, Flowerpot, Balloon, Bell, Alphabet, Clock, Light, Ribbon, GiftBox,
    Terrace, DreamCatcher, Moss, Vine, Stairs, Button, EggPlate, Snack, MiniBulb,


    Length
}
public enum BuildCount { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Length }
public enum BuildItemKind { Wall, Roof, Door, Window, Signboard, Etc, Length }

[System.Serializable]
public class Condition
{
    public List<BuildItemKind> buildItemKind;

    public BuildVPos[] buildVPos;
    public BuildHPos[] buildHPos;
    public BuildSize[] buildSize;

    public BuildColor[] buildColor;
    public BuildMaterial[] buildThema;
    public BuildShape[] buildShape;

    public int buildCount;
    public int buildPerfactCount;

    public float likeable;
}
[System.Serializable]
public class BuildObjAttribute
{
    public BuildItemKind buildItemKind;

    public BuildVPos buildVPos;
    public BuildHPos buildHPos;
    public BuildSize buildSize;

    public BuildColor buildColor;
    public BuildShape buildShape;
    public BuildMaterial[] buildThema;
}
