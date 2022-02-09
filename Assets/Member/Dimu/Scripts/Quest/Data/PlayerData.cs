using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/new PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public static Dictionary<int, Behavior> BuildBuildingData = new Dictionary<int, Behavior>();//
    public static Dictionary<int, Behavior> ItemData = new Dictionary<int, Behavior>();
    public static Dictionary<int, Behavior> npcData = new Dictionary<int, Behavior>();
    public static Dictionary<int, Behavior> locationData = new Dictionary<int, Behavior>();

    public static void AddValue(int dataid, int behav, Dictionary<int,Behavior> pairs)
    {
        AddDictionary(dataid, pairs);
        pairs[dataid].amounts[behav]++;
    }

    public static void AddDictionary(int dataid, Dictionary<int, Behavior> pairs)
    {
        if (!pairs.ContainsKey(dataid))
        {
            Debug.Log("add dictionary");
            pairs.Add(dataid, new Behavior(3));
        }
    }

    public void AddAmountTestBuilding(int dataid)
    {
        AddValue(dataid, (int)BuildingBehaviorEnum.Interact, BuildBuildingData);
        Debug.Log(BuildBuildingData[dataid].amounts[2]);
    }
    public void AddAmountItem(int dataid, int btype)
    {
        AddValue(dataid, btype, ItemData);
        Debug.Log(ItemData[dataid].amounts[btype] + " " + btype);
    }
}

public enum ItemBehaviorEnum
{
    GetItem, DropItem, EatItem
}
public enum BuildingBehaviorEnum
{
    Interact, StartBuild, CompleteBuild
}
public enum NpcBehaviorEnum
{
    Interact
}
public enum LocationBehaviorEnum
{
    Interact
}

public class Behavior
{
    public int id; //종류 id
    public int[] amounts;// = new int[3]; //상호작용 횟수
    public Behavior(int i)
    {
        amounts = new int[i];
    }
}

//public static Dictionary<int, BuildingBehavior> BuildBuildingData = new Dictionary<int, BuildingBehavior>();//
//public static Dictionary<int, ItemBehavior> ItemData = new Dictionary<int, ItemBehavior>();
//public static Dictionary<int, NpcBehavior> npcData = new Dictionary<int, NpcBehavior>();
//public static Dictionary<int, LocationBehavior> locationData = new Dictionary<int, LocationBehavior>();

//public static void AddValue<T>(int dataid, int behav, Dictionary<int, T> pairs) where T : Behavior, new()
//{
//    AddDictionary(dataid, pairs);
//    pairs[dataid].amounts[behav]++;
//}

//public static void AddDictionary<T>(int dataid, Dictionary<int, T> pairs) where T : Behavior, new()
//{
//    if (!pairs.ContainsKey(dataid))
//    {
//        Debug.Log("add dictionary");
//        T t = new T();
//        t.EBehavior(3);
//        pairs.Add(dataid, t);
//    }
//} 

//public class ItemBehavior : Behavior
//{
//}
//public class BuildingBehavior : Behavior
//{
//}
//public class NpcBehavior : Behavior
//{
//}
//public class LocationBehavior : Behavior
//{
//}
