using Game.Cam;
using NSY.Manager;
using NSY.Player;
using System.Collections.Generic;
using UnityEngine;

namespace DM.Building
{
    public class BuildingManager : MonoBehaviour
    {
        public List<BuildingBlock> buildings = new List<BuildingBlock>();

        private CameraManager CamManager;
        private BuildingDisplay buildingDisplay;

        PlayerInput.InputEvent savedelegate_ESC;
        PlayerInput.InputEvent savedelegate_F;

        public bool isBuildMode { get; set; } = false;
        private BuildingBlock nowBuildingBlock { get; set; } = null; //static에서 private로...

        private void Awake()
        {
            CamManager = FindObjectOfType<CameraManager>();
            buildingDisplay = FindObjectOfType<BuildingDisplay>();
        }

        public void BuildModeOn(BuildingBlock nowBuildingBlock_)
        {
            if (SuperManager.Instance.dialogueManager.IsTalking) return;

            savedelegate_ESC = PlayerInput.OnPressESCDown;
            savedelegate_F = PlayerInput.OnPressFDown;
            PlayerInput.OnPressESCDown = BuildModeOff;
            PlayerInput.OnPressFDown = null;

            isBuildMode = true;

            //NPC Off
            SuperManager.Instance.npcManager.AllNpcActive(false);

            //BuildingBlockSetting
            nowBuildingBlock = nowBuildingBlock_;
            nowBuildingBlock_.SetCancelUIAction(buildingDisplay.CancelUIState);
            nowBuildingBlock_.BuildModeOnSetting();

            //UI
            //buildingDisplay.BuildDisplayOn(true); //UI Display 추가

            //camera
            CamManager.ChangeFollowTarger(nowBuildingBlock.gameObject.transform, 1);
            CamManager.ActiveSubCamera(1);

        }

        public void BuildModeOff()
        {
            if (!isBuildMode) return;

            PlayerInput.OnPressESCDown = savedelegate_ESC;
            PlayerInput.OnPressFDown = savedelegate_F;

            isBuildMode = false;

            //NPC On
            SuperManager.Instance.npcManager.AllNpcActive(true);

            //BuildingBlockSetting
            nowBuildingBlock.BuildModeOffSetting(AddBuilding);
            nowBuildingBlock.InitItemDestroyCount();

            //Data
            PlayerData.AddValue(0, (int)BuildInputBehaviorEnum.EndBuilding, PlayerData.BuildInputData, (int)BuildInputBehaviorEnum.length);

            //UI
            buildingDisplay.CancelUIState(false);
            //buildingDisplay.BuildDisplayOn(false);

            //camera
            Camerazone.camcount--;
            CamManager.DeactiveSubCamera(1);
        }
        public void AddBuilding(BuildingBlock buildingBlock)
        {
            if (!buildings.Contains(buildingBlock))
            {
                buildingBlock.BuildingID = buildings.Count;
                buildings.Add(buildingBlock);
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
            foreach (var item in buildings)
            {
                if (item._livingCharacter == null) continue;
                if ((int)item._livingCharacter.GetCharacterType() == npctype)
                {
                    return item;
                }
            }
            return null;
        }

        internal void BtnSpawnHouseBuildItem(Item item)
        {
            nowBuildingBlock.BtnSpawnHouseBuildItem(item);
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
    }
}
public enum BuildVPos { Top, Mid, Bottom, Length }
public enum BuildHPos { Left, Right, Mid, Length }
public enum BuildSize { Small, Normal, Big, Length }
public enum BuildColor { None, Red, Orange, Yellow, Green, Blue, Mint, Pupple, White, Black, Pink, Length }
public enum BuildMaterial//타이어
{
    Wood, Stone, Paper, Iron, Gold, Silver, Rubber, Wax, Sand, Grass, Honey, Stick,
    Brick, StainedGlass, Herringbone, Plain, Slate, Truss, Kiwa, Thatched, Stroke, Cuty, Length
}
public enum BuildShape
{
    //Wall
    Square_Wall, Square_Width_Wall, Square_Height_Wall, Pentagon_Wall, Circle_Wall, Triangle_Wall,
    //Door
    Circle_Door = 1000, Square_Door,
    //Roof
    Laugh_Roof = 2000, Triangle_Roof, Square_Roof, Line_Roof, circle_Roof,
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
    public BuildItemKind[] buildItemKind;

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
