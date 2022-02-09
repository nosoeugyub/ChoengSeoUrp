using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/new PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public static Dictionary<int, Behavior> BuildBuildingData = new Dictionary<int, Behavior>();//
    public static Dictionary<int, Behavior> ItemData = new Dictionary<int, Behavior>();
    public static Dictionary<int, Behavior> npcData = new Dictionary<int, Behavior>();
    public static Dictionary<int, Behavior> locationData = new Dictionary<int, Behavior>();
    //public static Dictionary<int, BuildingBehavior> BuildBuildingData = new Dictionary<int, BuildingBehavior>();//
    //public static Dictionary<int, ItemBehavior> ItemData = new Dictionary<int, ItemBehavior>();
    //public static Dictionary<int, NpcBehavior> npcData = new Dictionary<int, NpcBehavior>();
    //public static Dictionary<int, LocationBehavior> loactionData = new Dictionary<int, LocationBehavior>();

    public static void AddValueInDictionary<T>(int dataid, int behav, Dictionary<int, T> pairs) where T : Behavior, new()
    {
        if (!pairs.ContainsKey(dataid))
        {
            Debug.Log("add dictionary");
            pairs.Add(dataid, new T());
        }
        pairs[dataid].amounts[behav]++;
    }

    public static void AddValue<T>(int dataid, int behav, Dictionary<int, T> pairs) where T : Behavior, new()
    {
        pairs[dataid].amounts[behav]++;
    }

    public void AddAmountTestBuilding(int dataid)
    {
        AddValueInDictionary(dataid, 2, BuildBuildingData);


        BuildBuildingData[dataid].amounts[2]++;
        Debug.Log(BuildBuildingData[dataid].amounts[2]);
    }
    public void AddAmountItem(int dataid, int btype)
    {
        AddValueInDictionary(dataid, btype, ItemData);

        ItemData[dataid].amounts[btype]++;
        Debug.Log(ItemData[dataid].amounts[btype] + " " + btype);
    }
}
public enum ItemBehavior
{
    GetItem, DropItem, EatItem
    //public int[] amounts = new int[3]; //아이템획득 횟수//아이템버리기 횟수//아이템먹기 횟수
}
public enum BuildingBehavior
{
    Interact, StartBuild, CompleteBuild
    //public int[] amounts = new int[3]; //상호작용 횟수, 건축 시작 횟수, 건축완료 횟수
}
public enum NpcBehavior
{
    Interact
    //public int[] amounts = new int[1]; //상호작용 횟수
}
public enum LocationBehavior
{
    Interact
    //public int[] amounts = new int[1]; //상호작용 횟수
}

public class Behavior
{
    public int id; //종류 id
    public int[] amounts = new int[3]; //상호작용 횟수
}