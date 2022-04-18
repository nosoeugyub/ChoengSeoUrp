using System.Collections;
using UnityEngine;
public enum MineState { Normal, Trunk, Gone, }
public class MineObject : ItemObject, IMineable
{
    public bool haveTruckState;
    int nowChopCount;
    int truckChopCount;
    [SerializeField] float respawnTime = 20;
    //[SerializeField] float time = 0;
    MineState mineState = MineState.Normal;//0 성장완료 1미완료
    [SerializeField] Material nowMat;

    [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
    [SerializeField] InItemType toolType;

    [Header("If haveTruckState True, Set Plz")]

    [SerializeField] Material downMat;
    [SerializeField] GameObject upObj;

    [SerializeField] Animator animator;

    [SerializeField] BoxCollider boxcol;



    private new void Awake()
    {
        base.Awake();

    }
    public void SetDownMat(Material material)
    {
        downMat = material;
    }
    private void OnEnable()
    {
        nowChopCount = 0;
        nowMat = quad.material;
        animator = quad.transform.parent.GetComponent<Animator>();//transform.Find("Quad").
        boxcol = GetComponent<BoxCollider>();
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        ChangeMineState(MineState.Normal);
        if (!haveTruckState) yield break;
        yield return new WaitForSeconds(0.1f);
        quad.transform.SetParent(animator.transform);
    }
    private void ChangeMineState(MineState state)
    {
        if (state == MineState.Normal)
        {
            Debug.Log("초기화");
            if (haveTruckState)
            {
                animator.SetBool("IsFalling", false);
                animator.SetTrigger("Finish");
                upObj.SetActive(false);
                nowChopCount = 0;
            }
            boxcol.enabled = true;
            //time = 0;
            quad.material = nowMat;
            quad.material.color = new Color(1, 1, 1, 1);

        }
        else if (state == MineState.Trunk)
        {
            quad.material = downMat;
            quad.transform.SetParent(transform);
            upObj.SetActive(true);
            nowChopCount = 0;
            //quad.material.color = new Color(1,1,1,0);
            animator.SetBool("IsFalling", true);
        }
        else
        {
            boxcol.enabled = false;
            animator.SetBool("IsFalling", true);

        }
    }
    public new string CanInteract()
    {
        return "캐기";
    }
    public bool Mine(Item handitem, Animator playerAnimator)
    {
        if (handitem.InItemType != toolType)
        {
            print("다른 도구로 시도해주세요.");
            return false;
        }
        print(nowChopCount);
        Interact();

        animator.SetBool("IsFalling", false);

        if (handitem.InItemType == InItemType.Pickaxe)
            playerAnimator.SetBool("isMining", true);
        else if (handitem.InItemType == InItemType.Axe)
            playerAnimator.SetBool("isAxing", true);

        playerAnimator.GetComponent<PlayerAnimator>().Mine = UpdateMineState;
        return true;
    }

    private void UpdateMineState()
    {
        if (++nowChopCount >= item.ChopCount)
        {
            NSY.Player.PlayerInput.OnPressFDown = null;
            DropItems();
            PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.MineItem, PlayerData.ItemData, ((int)ItemBehaviorEnum.length));

            if (!haveTruckState || mineState == MineState.Trunk)
            {
                ChangeMineState(MineState.Gone);
            }
            else if (mineState == MineState.Normal)
            {
                ChangeMineState(MineState.Trunk);
                StartCoroutine(Respawn());
            }
        }
        else
        {
            animator.SetTrigger("Shaking");
            //내구도 하락...
        }
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