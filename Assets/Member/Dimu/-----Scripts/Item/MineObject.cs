using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineObject : ItemObject, IMineable
{
    int nowChopCount;
    [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
    [SerializeField] InItemType toolType;

    private void OnEnable()
    {
        nowChopCount = 0;
    }

    void Update()
    {
        
    }
    public string CanInteract()
    {
        return "캐기";
    }
    public void Mine(Item handitem)
    {
        if (handitem.InItemType != toolType)
        {
            print("다른 도구로 시도해주십쇼.");
            return;
        }
        print(nowChopCount);
        if(++nowChopCount == handitem.ChopCount)
        {
            NSY.Player.PlayerInput.OnPressFDown = null;
            Destroy(gameObject);
            DropItems();
        }
    }

    public void DropItems()
    {
        GameObject instantiateItem;
        foreach (DropItem item in item.DropItems)
        {
            print("spawn" + 2);
            for (int i = 0; i < item.count; ++i)
            {
                instantiateItem = Instantiate(item.itemObj) as GameObject;
                Vector3 randVec = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));
                instantiateItem.transform.position = gameObject.transform.position + randVec;
                print("spawn" + instantiateItem.name);
            }
        }
    }

}
[System.Serializable]
public class DropItem
{
    public GameObject itemObj;
    public int count;
}