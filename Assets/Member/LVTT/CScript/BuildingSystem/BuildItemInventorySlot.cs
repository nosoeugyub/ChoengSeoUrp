using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT.BuildSystem
{
    public class BuildItemInventorySlot : MonoBehaviour
    {
        public Transform BuildSpawn;
        public List<Transform>InventoryList;
        public Transform InventoryUI;
        [SerializeField] Transform SlotParent;
        [SerializeField] Vector3 InventBuildPos;
        //Vector3 InventOriginPos;

        // Start is called before the first frame update
        void Start()
        {
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
        }

        void AddSlotToList()
        {
            foreach(Transform child in SlotParent)
            {
                InventoryList.Add(child);
            }    
           
        } 
        public void SetInventoryPos(Vector3 MovePos)
        {
            //InventoryUI.position = Camera.main.WorldToScreenPoint(MovePos);
        }
        public void MoveInventToRight()
        {
 //           InventoryUI.position = Camera.main.WorldToScreenPoint(InventBuildPos);
        }

        public void ResetInventPos()
        {
            //InventoryUI.position = Camera.main.WorldToScreenPoint(Vector3.zero);
        }

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
