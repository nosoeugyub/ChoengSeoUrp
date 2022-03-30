
using UnityEngine;
using UnityEngine.UI;

namespace TT.BuildSystem
{
    public enum CItemType { BuildItem, DecoItem }
    public class BuildingItemSpawn : MonoBehaviour
    {
        public Button Slotbutton;
        public float SpawnOffsetY;
        public float SpawnOffsetZ;
        // public bool slotEmpty;
        public GameObject SpawnBuildItem;
        public Transform SpawnParent;
        public Transform CurBuilding;

        public Transform ThePlayer;
        public bool isWall;
        [SerializeField] public CItemType ItemType;

        private float BuildItemGap=0.01f ;
        public void BtnSpawnHouseBuildItem()
        {
            BuildingBlock CurBlock = CurBuilding.GetComponent<BuildingBlock>();
            Vector3 spawnPos = SpawnParent.transform.position;
            spawnPos.y =spawnPos.y + SpawnOffsetY;
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
