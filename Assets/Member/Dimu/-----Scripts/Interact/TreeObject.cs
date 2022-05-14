using NSY.Manager;
using System.Collections;
using UnityEngine;

public class TreeObject : MineObject
{
    [SerializeField] Material downMat;
    [SerializeField] GameObject upObj;
    [SerializeField] Tree sadtree;
    Tree origintree;
    [SerializeField] TreeType nowTreeType = TreeType.Sad;

    EnvironmentManager environmentManager;

    static int allSadTreeCount = 0;
    static int nowSadTreeCount = 0;
    private new void Awake()
    {
        base.Awake();
        environmentManager = FindObjectOfType<EnvironmentManager>();
        origintree = new Tree();
    }
    private void Start()
    {
        ++allSadTreeCount;
        nowSadTreeCount = allSadTreeCount;
        ChangeTreeData(TreeType.Sad);
        quad.material = nowMat;
    }
    public void SetDownMat(Material material)
    {
        downMat = material;
    }
    public new void OnEnable()
    {
        base.OnEnable();
    }
    public override int CanInteract()
    {
        return (int)CursorType.Axe;
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        ChangeMineState(MineState.Normal);
        yield return new WaitForSeconds(0.1f);
        quad.transform.SetParent(animator.transform);
    }
    protected new void ChangeMineState(MineState state)
    {
        mineState = state;
        Debug.Log("Tree ChangeMineState");

        if (state == MineState.Normal) //처음으로 초기화
        {
            animator.SetBool("IsFalling", false);
            animator.SetTrigger("Finish");

            //확률계산
            CalculateUpgradePercent();

            upObj.SetActive(false);
            boxcol.enabled = true;

            quad.material = nowMat;

            nowChopCount = 0;
        }
        else if (state == MineState.Trunk) //무너질 때로 초기화
        {
            animator.SetBool("IsFalling", true);
            upObj.SetActive(true);
            boxcol.enabled = false;
            quad.material = downMat;
            nowChopCount = 0;

            quad.transform.SetParent(transform);
        }
        else
        {
            boxcol.enabled = false;
            animator.SetBool("IsFalling", true);
        }
    }
    public override void UpdateMineState()
    {
        Debug.Log("Tree UpdateMineState");
        SuperManager.Instance.soundManager.PlaySFX(handitem.UsingToolSoundName);
        if (++nowChopCount >= item.ChopCount)
        {
            NSY.Player.PlayerInput.OnPressFDown = null;
            DropItems();
            PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.MineItem, PlayerData.ItemData, ((int)ItemBehaviorEnum.length));
            FindObjectOfType<EnvironmentManager>().ChangeCleanliness(item.CleanAmount);

            if (mineState == MineState.Normal)
            {
                ChangeMineState(MineState.Trunk);
                StartCoroutine(Respawn());
            }
        }
        else
        {
            animator.SetTrigger("Shaking");
        }
    }
    public void ChangeTreeData(TreeType treetype)
    {
        if (treetype == TreeType.Sad)
        {
            //기존 거 저장
            origintree.TreeMat = quad.material;
            origintree.TreeUpMat = upObj.GetComponent<Renderer>().material;
            origintree.TreeDownMat = downMat;

            nowMat = sadtree.TreeMat;
            upObj.GetComponent<Renderer>().material = sadtree.TreeUpMat;
            downMat = sadtree.TreeDownMat;
        }
        else
        {
            nowMat = origintree.TreeMat;
            upObj.GetComponent<Renderer>().material = origintree.TreeUpMat;
            downMat = origintree.TreeDownMat;
        }
    }
    public void CalculateUpgradePercent()
    {
        if (nowTreeType == TreeType.Sad)
        {
            int randnum = Random.Range(0, 100);

            float treepercent =(1.0f - (float)nowSadTreeCount / allSadTreeCount)* 100f;
            float percent = environmentManager.Cleanliness - treepercent + treepercent / 5;
            print(treepercent + "  " +  percent +  "  " +  randnum);
            if (randnum < percent)
            {
                nowTreeType = TreeType.Original;
                ChangeTreeData(nowTreeType);
                nowSadTreeCount--;
            }
        }
    }
}
public enum TreeType { Original, Sad, Length }