using DM.Building;
using UnityEngine;

public class SpecialHouse : MonoBehaviour
{
    [SerializeField] HaveItem[] nessBuildItems;
    BuildingBlock buildingBlock;
    [SerializeField] bool ison;
    private void Awake()
    {
        buildingBlock = GetComponent<BuildingBlock>();
    }
    public void CanExist(BuildingItemObj buildingItemObj, bool isAdd)
    {
        if (!isAdd)
        {
            int count = 0;
            foreach (BuildingItemObj item in buildingBlock.GetBuildItemList())
            {
                if (item.GetItem() == buildingItemObj.GetItem())
                {
                    ++count;
                }
            }
            if (count <= 1)
            {
                ison = false;
                print("SPECIALBUILDING OFF");
            }
        }

        else
        {
            foreach (HaveItem item in nessBuildItems)
            {
                if (item.isHave == true) continue; //이미 갖구있는애면
                if (item.nessBuildItem == buildingItemObj.GetItem())//없는애랑 새로들어온애랑 같다면
                {
                    item.isHave = true;
                }
                else
                {
                    print("isonfalse");
                    ison = false;
                    return;
                }
            }
            ison = true;
            print("SPECIALBUILDING ON");

        }
    }
    [System.Serializable]
    public class HaveItem
    {
        public Item nessBuildItem;
        public bool isHave = false;
    }
}
