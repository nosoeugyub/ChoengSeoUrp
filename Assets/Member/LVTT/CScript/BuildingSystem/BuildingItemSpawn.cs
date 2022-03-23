﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT.BuildSystem
{
    public class BuildingItemSpawn : MonoBehaviour
    {
        public float SpawnOffsetZ;
        public float SpawnOffsetY;
       // public bool slotEmpty;
        public GameObject SpawnBuildItem;
        public Transform SpawnParent;
        [HideInInspector]
        public Transform CurBuilding;
       
       

        private float BuildItemGap=0.01f ;
        public void BtnSpawnHouseBuildItem()
        {
            BuildingBlock CurBlock = CurBuilding.GetComponent<BuildingBlock>();
            BuildMaterialObject ItemObj = SpawnBuildItem.GetComponent<BuildMaterialObject>();
            Vector3 spawnPos = SpawnParent.transform.position;
            spawnPos.y = SpawnOffsetY;
            if (ItemObj.buildMaterialState == BuildMaterialState.Wall)
            {
                spawnPos.z = spawnPos.z + SpawnOffsetZ;// when the building is facing South
            }
            else
            {
                spawnPos.z = spawnPos.z + SpawnOffsetZ - (BuildItemGap * CurBlock.BuildItemList.Count);// when the building is facing South
                if(CurBlock.BuildItemList.Count==1)
                {
                    CurBlock.MaxBackItemzPos = spawnPos.z;
                }
            }  
            GameObject newPrefab = Instantiate(SpawnBuildItem, spawnPos, Quaternion.identity, SpawnParent.transform);
            newPrefab.name = SpawnBuildItem.name;
            CurBlock.AddBuildItemToList(newPrefab);
            CurBlock.CurFrontItemzPos = spawnPos.z;
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

}
