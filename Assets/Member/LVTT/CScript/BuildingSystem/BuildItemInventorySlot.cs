using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TT.BuildSystem
{
    public class BuildItemInventorySlot : MonoBehaviour
    {
       
        public List<Transform>InventoryList;
        public Transform InventoryUI;
        [SerializeField] Transform SlotParent;
        [SerializeField] Vector3 InventBuildPos;

        BuildingManager BuildManager;

        // Start is called before the first frame update
        void Start()
        {
            BuildManager = FindObjectOfType<BuildingManager>();
            //InventOriginPos = InventoryUI.position;
            AddSlotToList();
        }

        // Update is called once per frame
        void Update()
        {
           //if(Input.GetMouseButtonDown(0))
           // { MoveInventToRight(); }
           //if (Input.GetMouseButtonDown(1))
           // { ResetInventPos(); }

            if(BuildManager.isBuildMode)
            {
                
                CheckBuildItemList();
            }
        }

        void AddSlotToList()
        {
            foreach(Transform child in SlotParent)
            {
                InventoryList.Add(child);
            }    
           
        } 

        void CheckBuildItemList()
        {
            foreach (Transform child in SlotParent)
            {
                BuildingItemSpawn Slot = child.GetComponent<BuildingItemSpawn>();
                if (BuildManager.OnBuildItemDrag)
                {
                    Slot.Slotbutton.interactable = false;
                }
                else
                {if (BuildManager.nowBuildingBlock.hasWall)
                    {
                        if (Slot.ItemType == CItemType.BuildItem)
                        { Slot.Slotbutton.interactable = true; }
                        else
                        { Slot.Slotbutton.interactable = false; }    
                    }
                    else
                    {
                        if (!Slot.isWall)
                        { Slot.Slotbutton.interactable = false; }
                    }
                }
             
            }
        }
 //       public void SetInventoryPos(Vector3 MovePos)
 //       {
 //           //InventoryUI.position = Camera.main.WorldToScreenPoint(MovePos);
 //       }
 //       public void MoveInventToRight()
 //       {
 ////           InventoryUI.position = Camera.main.WorldToScreenPoint(InventBuildPos);
 //       }

 //       public void ResetInventPos()
 //       {
 //           //InventoryUI.position = Camera.main.WorldToScreenPoint(Vector3.zero);
 //       }

        public void AssignBuildItemSpawnPos(Transform SpawnParent,Transform CurBuilding)             
        {
            foreach (Transform Slot in InventoryList)
            {
                BuildingItemSpawn BuildItemSpawn = Slot.GetComponent<BuildingItemSpawn>();
                BuildItemSpawn.SpawnParent = SpawnParent;
                BuildItemSpawn.CurBuilding = CurBuilding;
            }


        }
    }

}
