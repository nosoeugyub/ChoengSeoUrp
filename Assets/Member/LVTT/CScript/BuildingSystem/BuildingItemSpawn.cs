

using UnityEngine;
using UnityEngine.UI;

namespace TT.BuildSystem
{
    public enum CItemType { BuildItem, DecoItem }
    public class BuildingItemSpawn : MonoBehaviour
    {
        //[HideInInspector]
        public Button Slotbutton;
        public float SpawnOffsetY;
        public float SpawnOffsetZ;
        // public bool slotEmpty;
        public GameObject SpawnBuildItem;
        public Transform SpawnParent;
        public Transform CurBuilding;

        Transform ThePlayer;
        public bool isWall;
        [SerializeField] public CItemType ItemType;

        private float BuildItemGap = 0.000001f;
        BuildingManager BuildManager;

        private void Start()
        {
            BuildManager = FindObjectOfType<BuildingManager>();
         
            ThePlayer = BuildManager.Player.transform;
        }
        public void BtnSpawnHouseBuildItem()
        {
            BuildingBlock CurBlock = CurBuilding.GetComponent<BuildingBlock>();
            Vector3 spawnPos = SpawnParent.transform.position;
            spawnPos.y = spawnPos.y + SpawnOffsetY;
            if (this.isWall)
            {
                spawnPos.z = spawnPos.z - SpawnOffsetZ;// when the building is facing South
                CurBlock.CurWallItemzPos = spawnPos.z;
                CurBlock.hasWall = true;

            }
            else
            {
                spawnPos.z = spawnPos.z - SpawnOffsetZ - (BuildItemGap * CurBlock.BuildItemList.Count);// when the building is facing South
                if (CurBlock.BuildItemList.Count == 1)
                {
                    CurBlock.MaxBackItemzPos = spawnPos.z;
                }
            }
            GameObject newPrefab = Instantiate(SpawnBuildItem, spawnPos, Quaternion.identity, SpawnParent.transform);
            newPrefab.name = SpawnBuildItem.name;
            CurBlock.AddBuildItemToList(newPrefab);
            CurBlock.CurFrontItemzPos = spawnPos.z;
        }

        public void BtnSpawnHouseBuildItem(Item spawnObj)
        {
            BuildingBlock CurBlock = FindObjectOfType<BuildingManager>().nowBuildingBlock;
            Vector3 spawnPos = CurBlock.HouseBuild.transform.position;
            spawnPos.y = spawnPos.y + SpawnOffsetY;
            if (spawnObj.OutItemType == OutItemType.BuildWall)
            {
                spawnPos.z = spawnPos.z - SpawnOffsetZ;// when the building is facing South
                CurBlock.CurWallItemzPos = spawnPos.z;
                CurBlock.hasWall = true;
                BuildManager.inventoryNSY.CheckCanBuildItem(BuildManager.nowBuildingBlock);

            }
            else
            {
                spawnPos.z = spawnPos.z - SpawnOffsetZ - (BuildItemGap * CurBlock.BuildItemList.Count);// when the building is facing South
                if (CurBlock.BuildItemList.Count == 1)
                {
                    CurBlock.MaxBackItemzPos = spawnPos.z;
                }
            }
            Debug.Log(spawnPos);
            GameObject newPrefab = Instantiate(spawnObj.ItemPrefab, spawnPos, Quaternion.identity, CurBlock.HouseBuild.transform);
            newPrefab.name = spawnObj.name;
            CurBlock.AddBuildItemToList(newPrefab);
            CurBlock.CurFrontItemzPos = spawnPos.z;
        }


        public void BtnSpawnGardenBuildItem()
        {
            Vector3 spawnPos = ThePlayer.transform.position;
            spawnPos.y = spawnPos.y - SpawnOffsetY;
            //spawnPos.z = spawnPos.z + SpawnOffsetZ;
            var newPrefab = Instantiate(SpawnBuildItem, spawnPos, Quaternion.identity, SpawnParent.transform);
            newPrefab.name = SpawnBuildItem.name;
        }
    }

}

