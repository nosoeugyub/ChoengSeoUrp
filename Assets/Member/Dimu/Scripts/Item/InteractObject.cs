using UnityEngine;

public class InteractObject : MonoBehaviour
{
    [SerializeField]
    private DropItem[] items;

    public void DropItems()
    {
        GameObject instantiateItem;
        foreach (DropItem item in items)
        {
            print("spawn" + 2);
            for (int i = 0; i < item.count; ++i)
            {
                instantiateItem = Instantiate(item.itemObj) as GameObject;
                Vector3 randVec = new Vector3(Random.Range(-1.5f, 1.5f),0, Random.Range(-1.5f, 1.5f));
                instantiateItem.transform.position = gameObject.transform.position+ randVec;
                print("spawn" + instantiateItem.name);
            }
        }
    }
    [System.Serializable]
    public class DropItem
    {
        public GameObject itemObj;
        public int count;
    }
}
