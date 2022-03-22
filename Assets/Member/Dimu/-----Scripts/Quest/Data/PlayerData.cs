using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/new PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public static Dictionary<int, Behavior> BuildBuildingData = new Dictionary<int, Behavior>();//
    public static Dictionary<int, Behavior> ItemData = new Dictionary<int, Behavior>();
    public static Dictionary<int, Behavior> npcData = new Dictionary<int, Behavior>();
    public static Dictionary<int, Behavior> locationData = new Dictionary<int, Behavior>();

    public static void AddValue(int dataid, int behav, Dictionary<int,Behavior> pairs, int listCount)
    {
        AddDictionary(dataid, pairs, listCount);
        pairs[dataid].amounts[behav]++;
    }

    public static void AddDictionary(int dataid, Dictionary<int, Behavior> pairs, int listCount)
    {
        if (!pairs.ContainsKey(dataid))
        {
            Debug.Log("add dictionary");
            pairs.Add(dataid, new Behavior(listCount));
        }
    }

    //public void AddAmountTestBuilding(int dataid)
    //{
    //    AddValue(dataid, (int)BuildingBehaviorEnum.Interact, BuildBuildingData);
    //    Debug.Log(BuildBuildingData[dataid].amounts[2]);
    //}
    //public void AddAmountItem(int dataid, int btype)
    //{
    //    AddValue(dataid, btype, ItemData);
    //    Debug.Log(ItemData[dataid].amounts[btype] + " " + btype);
    //}
}

public enum ItemBehaviorEnum//아이템에는 드랍아이템, 맵 아이템이 있다. (나뭇가지, 쓰레기, 쓰레기통, 나무, 돌, 음식, 건축자재)
{
    GetItem, DropItem, EatItem, InteractItem, length
}
public enum BuildingBehaviorEnum //건축 지역
{
    Interact, StartBuild, CompleteBuild, length
}
public enum NpcBehaviorEnum
{
    Interact, length
}
public enum LocationBehaviorEnum
{
    Interact, length
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
public enum OutItemType
{
    Talk, Tool, Mineral, Food, Collect, Build, Etc
}


public enum InItemType
{
    //1차 재료
    None = 0, Twigs, Cutgrass, Rocks, Petal, leaf, Mud, Trash,
    //1차 식량
    Seed = 10000, Apple,
    //2차재료
    Woodplank = 20000, Cutstone, String, torch,
    //2차 식량

    //3차재료
    Rope = 30000,

    //도구
    Axe = 40000, Pickaxe, MagnifyingGlass, Hammer,
    //미네랄
    tree = 50000, Stone,
    //이벤트오브젝트
    Trashcan = 60000, Mailbox,
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
