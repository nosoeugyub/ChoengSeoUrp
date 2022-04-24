using System.Collections.Generic;
using TT.BuildSystem;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public List<BuildingBlock> buildings = new List<BuildingBlock>();
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


}

public enum BuildVPos { Top, Mid, Bottom, Length }
public enum BuildHPos { Left, Right, Mid, Length }

public enum BuildSize { Small, Normal, Big, Length }
public enum BuildColor { None, Red, Orange, Yellow, Green, Blue, Navy, Pupple, White, Black, Length }
public enum BuildThema { Wood, Stone, Brick, StainedGlass, Concrete, Emptylot, Herringbone, Plain, Steelplate, Stripe, Tile, Length }
public enum BuildShape
{
    //Wall
    Square, Square_Width, Square_Height, Pentagon,
    //Door
    Circle_Door = 1000,
    //Roof
    Laugh = 2000, Triangle,
    //Window
    Circle_Window = 3000, Square_Window,
    //Etc
    Flower = 4000, Clock, Light, Ribbon, Box,


    Length
}
public enum BuildCount { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Length }
public enum BuildItemKind { Wall, Roof, Door, Window, Signboard, Etc, Length }

[System.Serializable]
public class Condition
{
    public List< BuildItemKind> buildItemKind;

    public BuildVPos[] buildVPos;
    public BuildHPos[] buildHPos;
    public BuildSize[] buildSize;

    public BuildColor[] buildColor;
    public BuildThema[] buildThema;
    public BuildShape[] buildShape;

    public BuildCount[] buildCount;

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
    public BuildThema[] buildThema;
}