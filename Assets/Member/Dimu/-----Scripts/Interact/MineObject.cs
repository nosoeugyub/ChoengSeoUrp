using NSY.Manager;
using System.Collections;
using UnityEngine;
public enum MineState { Normal, Trunk, Gone, }
public class MineObject : ItemObject
{
    protected int nowChopCount;
    [SerializeField] protected float respawnTime = 20;
    protected MineState mineState = MineState.Normal;//0 성장완료 1미완료
    [SerializeField] protected Material nowMat;

    [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
    [SerializeField] protected InItemType toolType;

    [SerializeField] protected Animator animator;
    [SerializeField] protected BoxCollider boxcol;
    protected Item handitem;
    private new void Awake()
    {
        base.Awake();
    }

    protected void OnEnable()
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
    }
    protected void ChangeMineState(MineState state)
    {
        mineState = state;
        if (state == MineState.Normal)
        {
            boxcol.GetComponent<BoxCollider>().enabled = true;
            quad.material = nowMat;
            quad.material.color = new Color(1, 1, 1, 1);
        }
        else
        {

            boxcol = GetComponent<BoxCollider>();
            boxcol.enabled = false;
            animator.SetBool("IsFalling", true);
        }
    }
    public override int CanInteract()
    {
        return (int)CursorType.PickAxe;
    }
    public bool Mine(Item _handitem, Animator playerAnimator)
    {
        handitem = _handitem;
        if (_handitem.InItemType != toolType)
        {
            print("다른 도구로 시도해주세요.");
            return false;
        }
        Interact();

        animator.SetBool("IsFalling", false);

        if (_handitem.InItemType == InItemType.Pickaxe)
            playerAnimator.SetBool("isMining", true);
        else if (_handitem.InItemType == InItemType.Axe)
            playerAnimator.SetBool("isAxing", true);
        SetAnimationEventMethod(playerAnimator);
        return true;
    }

    public void SetAnimationEventMethod(Animator playerAnimator)
    {
        playerAnimator.GetComponent<PlayerAnimator>().Mine = UpdateMineState;
    }

    public virtual void UpdateMineState()
    {
        SuperManager.Instance.soundManager.PlaySFX(handitem.UsingToolSoundName);
        if (++nowChopCount >= item.ChopCount)
        {
            NSY.Player.PlayerInput.OnPressFDown = null;
            DropItems();
            PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.MineItem, PlayerData.ItemData, ((int)ItemBehaviorEnum.length));
            FindObjectOfType<EnvironmentManager>().ChangeCleanliness(item.CleanAmount);

            ChangeMineState(MineState.Gone);
            //StartCoroutine(Respawn());
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
            float randnum = Random.Range(0, 100);//50( 10 30 60)
            float sumtemp = 0;

            if (item.percent < randnum)
                continue;

            randnum = Random.Range(0, 100);

            int i = 0;
            for (i = 0; i < item.itemObjs.Length; i++)
            {
                sumtemp += item.itemObjs[i].percent;
                if (sumtemp >= randnum)
                    break;
            }
            //i = 결정된 오브젝트

            for (int j = 0; j < item.itemObjs[i].count; ++j)
            {
                Vector3 randVec = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));
                instantiateItem = ObjectPooler.SpawnFromPool(item.itemObjs[i].itemObj.name, gameObject.transform.position + randVec);
            }
        }
    }
}
[System.Serializable]
public class DropItem//이 안에서 확률계산해서 itemObjs 중 1개 뽑음.
{
    [Range(0, 100)]
    public float percent; //itemObjs을 획득할지 안할지 결정
    public PercentCalItems[] itemObjs;
}
[System.Serializable]
public class PercentCalItems
{
    public GameObject itemObj;
    [Range(0, 100)]
    public float percent;
    public int count; //당첨 시 개수
}