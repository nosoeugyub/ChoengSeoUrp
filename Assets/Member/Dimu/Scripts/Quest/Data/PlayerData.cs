using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/new PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public static Dictionary<int, int> BuildBuildingData
        = new Dictionary<int, int>();
    public static Dictionary<int, ItemBehavior> ItemData
    = new Dictionary<int, ItemBehavior>();


    public void AddAmountTestBuilding(int dataid)
    {
        if (!BuildBuildingData.ContainsKey(dataid))
        {
            Debug.Log("add dictionary");
            BuildBuildingData.Add(dataid, new int());
        }
        BuildBuildingData[dataid]++;
            Debug.Log(BuildBuildingData[dataid]);
    }

    public void AddAmountItem(int dataid, int btype)
    {
        if (!ItemData.ContainsKey(dataid))
        {
            Debug.Log("add dictionary");
            ItemData.Add(dataid, new ItemBehavior());
        }
        ItemData[dataid].amounts[btype]++;
        Debug.Log(ItemData[dataid].amounts[btype]+" "+ btype);
    }
}
public class ItemBehavior
{
    public int id; //건물의 종류 id
    public int[] amounts = new int[3]; //아이템획득 횟수//아이템버리기 횟수//아이템먹기 횟수
}