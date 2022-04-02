using System.Collections;
using UnityEngine;

public class MineObject : ItemObject, IMineable
{
    int nowChopCount;
    [SerializeField] float respawnTime = 5;
    [SerializeField] float time = 0;
    int state = 0;//0 성장완료 1미완료

    BoxCollider boxcol;

    [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
    [SerializeField] InItemType toolType;

    private void Awake()
    {
        base.Awake();
        boxcol = GetComponent<BoxCollider>();
    }
    private void OnEnable()
    {
        nowChopCount = 0;
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        ChangeMineState(0);
    }
    private void ChangeMineState(int state)
    {
        if (state == 0)
        {
            Debug.Log("초기화");
            boxcol.enabled = true;
            nowChopCount = 0;
            time = 0;
            quad.material.color = new Color(1,1,1,1);
        }
        else if(state == 1)
        {
            quad.material.color = new Color(1,1,1,0);
            boxcol.enabled = false;

        }
    }
    public string CanInteract()
    {
        return "캐기";
    }
    public bool Mine(Item handitem, Animator animator)
    {
        if (handitem.InItemType != toolType)
        {
            print("다른 도구로 시도해주십쇼.");
            return false;
        }
        //print(nowChopCount);
        Interact();

        if (handitem.InItemType == InItemType.Pickaxe)
            animator.SetBool("isMining", true);
        else if (handitem.InItemType == InItemType.Axe)
            animator.SetBool("isAxing", true);

        if (++nowChopCount >= item.ChopCount)
        {
            NSY.Player.PlayerInput.OnPressFDown = null;
            DropItems();
            ChangeMineState(1);
            StartCoroutine(Respawn());
            //Destroy(gameObject);
        }
        else
        {
            //내구도 하락...
        }
        return true;
    }

    public void DropItems()
    {
        GameObject instantiateItem;
        foreach (DropItem item in item.DropItems)
        {
            //print("spawn" + 2);
            for (int i = 0; i < item.count; ++i)
            {
                instantiateItem = Instantiate(item.itemObj) as GameObject;
                Vector3 randVec = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));
                instantiateItem.transform.position = gameObject.transform.position + randVec;
                //print("spawn" + instantiateItem.name);
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