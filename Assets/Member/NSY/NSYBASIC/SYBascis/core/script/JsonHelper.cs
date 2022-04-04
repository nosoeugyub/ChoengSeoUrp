using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    public static List<T> FromJsonList<T>(string json)
    {
        WrapperList<T> wrapper = JsonUtility.FromJson<WrapperList<T>>(json);
        return wrapper.items;
    }
    public static T FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.item;
    }

    public static string ToJsonList<T>(List<T> list)
    {
        WrapperList<T> wrapper = new WrapperList<T>();
        wrapper.items = list;
        return JsonUtility.ToJson(wrapper);
    }
    public static string ToJson<T>(T item)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.item = item;
        return JsonUtility.ToJson(wrapper);
    }
    public static string ToJsonList<T>(List<T> list, bool prettyPrint)
    {
        WrapperList<T> wrapper = new WrapperList<T>();
        wrapper.items = list;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
    public static string ToJson<T>(T item, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.item = item;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class WrapperList<T>
    {
        public List<T> items;
    }
    private class Wrapper<T>
    {
        public T item;
    }
}