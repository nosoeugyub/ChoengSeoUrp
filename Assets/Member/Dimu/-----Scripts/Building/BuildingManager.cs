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
