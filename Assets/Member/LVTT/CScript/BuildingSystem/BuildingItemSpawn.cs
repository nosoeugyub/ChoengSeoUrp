using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingItemSpawn : MonoBehaviour
{
    public float SpawnOffsetZ;
    public float SpawnOffsetY;
    //private float SpawnOffsetY=3.7f;
    public GameObject SpawnBuildItem;
    public Transform SpawnParent; 
    public void BtnSpawnHouseBuildItem()
    {
        //Vector3 spawnPos = SpawnParent.transform.position;
        Vector3 spawnPos = SpawnParent.transform.position;
        spawnPos.y = SpawnOffsetY; 
        spawnPos.z = spawnPos.z +SpawnOffsetZ;
       var newPrefab=Instantiate(SpawnBuildItem, spawnPos, Quaternion.identity, SpawnParent.transform);
        newPrefab.name = SpawnBuildItem.name;
    }

    public void BtnSpawnGardenBuildItem()
    {
        Vector3 spawnPos = SpawnParent.transform.position;
        spawnPos.y = SpawnOffsetY;
        spawnPos.z = spawnPos.z + SpawnOffsetZ;
        var newPrefab = Instantiate(SpawnBuildItem, spawnPos, Quaternion.identity, SpawnParent.transform);
        newPrefab.name = SpawnBuildItem.name;
    }
}
