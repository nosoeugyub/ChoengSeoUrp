using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT.BuildSystem
{
    public class BuildItemInventorySlot : MonoBehaviour
    {
        public Transform BuildSpawn;
        public GameObject[] InventorySlotList;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        
        }


    
        public void AssignBuildItemSpawnPos(Transform SpawnParent)             
        {
            //BuildingItemSpawn BuildItemSpawn = InventorySlotList[0].GetComponent<BuildingItemSpawn>();
            //BuildItemSpawn.SpawnParent = SpawnParent;
            foreach (GameObject Slot in InventorySlotList)
            {
                BuildingItemSpawn BuildItemSpawn = Slot.GetComponent<BuildingItemSpawn>();
                BuildItemSpawn.SpawnParent = SpawnParent;
            }

            //for (int i=0;i<InventorySlotList.Length;i++)
            //{
            //    BuildingItemSpawn BuildItemSpawn = InventorySlotList[i].GetComponent<BuildingItemSpawn>();
            //    BuildItemSpawn.SpawnParent = SpawnParent;
            //}
        }
    }

}
