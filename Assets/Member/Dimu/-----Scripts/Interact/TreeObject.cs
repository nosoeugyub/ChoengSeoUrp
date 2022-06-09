using NSY.Manager;
using System.Collections;
using UnityEngine;

public class TreeObject : MineObject
{
    [SerializeField] MaterialSet finMatSet;//원래애 머테리얼을 저장함.
    [SerializeField] MaterialSet sadMatSet;//시든애 머테리얼을 저장함.
    MaterialSet nowMatSet;//현재 적용중인 머테리얼
    [SerializeField] MeshRenderer upMesh;
    [SerializeField] MeshRenderer downMesh;
    [SerializeField] GameObject upObj;
    [SerializeField] Item sadtree;
    Item origintree;
    [SerializeField] TreeType nowTreeType;

    EnvironmentManager environmentManager;

    static int allSadTreeCount = 0;
    static int nowSadTreeCount = 0;
    private new void Awake()
    {
        base.Awake();
        environmentManager = FindObjectOfType<EnvironmentManager>();
        //upMesh = upObj.GetComponent<MeshRenderer>();
        finMatSet.materials = new Material[3];

        origintree = GetItem();

    }
    private void Start()
    {
        ++allSadTreeCount;
        nowSadTreeCount = allSadTreeCount;
        StartCoroutine(SpawnTreesAfterCounting());
        //quad.material = nowMat;
    }
    IEnumerator SpawnTreesAfterCounting()
    {
        yield return new WaitForEndOfFrame();
        ChangeTreeType(TreeType.Sad);
        ChangeMineState(MineState.Normal);
    }
    public void SetDownMat(Material material)
    {
        finMatSet.materials[2] = material;
    }
    public new void OnEnable()
    {
        base.OnEnable();
        finMatSet.materials[0] = nowMat;
        finMatSet.materials[1] = upMesh.material;
        finMatSet.materials[2] = downMesh.material;
    }
    public override int CanInteract()
    {
        return (int)CursorType.Axe;
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime + Random.Range(0, 50));
        ChangeMineState(MineState.Normal);
        yield return new WaitForSeconds(0.1f);
        quad.transform.SetParent(animator.transform);
    }
    protected new void ChangeMineState(MineState state)
    {
        mineState = state;
        //Debug.Log("Tree ChangeMineState");

        if (state == MineState.Normal) //처음으로 초기화
        {
            animator.SetBool("IsFalling", false);
            animator.SetTrigger("Finish");

            //확률계산
            CalculateUpgradePercent();

            upObj.SetActive(false);
            boxcol.enabled = true;

            quad.material = nowMatSet.materials[0];
            upMesh.material = nowMatSet.materials[1];

            nowChopCount = 0;
        }
        else if (state == MineState.Trunk) //무너질 때로 초기화
        {
            animator.SetBool("IsFalling", true);
            upObj.SetActive(true);
            boxcol.enabled = false;
            quad.material = nowMatSet.materials[2];
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
        SuperManager.Instance.soundManager.PlaySFX(item.UsingToolSoundName);
        if (++nowChopCount >= item.ChopCount)
        {
            DropItems();
            PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.MineItem, PlayerData.ItemData, ((int)ItemBehaviorEnum.length));
            FindObjectOfType<EnvironmentManager>().ChangeCleanliness(item.CleanAmount);

            if (mineState == MineState.Normal)
            {
                ChangeMineState(MineState.Trunk);
                if (!cantRespown)
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
            nowMatSet = sadMatSet;
            SetItem(sadtree);
        }
        else
        {
            nowMatSet = finMatSet;
            SetItem(origintree);
        }
    }
    public void CalculateUpgradePercent()
    {
        if (nowTreeType == TreeType.Sad)
        {
            CalculateSolution_Direct();
            //CalculateSolution_RandomNum();
        }
    }

    private void CalculateSolution_Direct()
    {
        float treepercent = GoodTreePercentage();
        if (treepercent < environmentManager.Cleanliness + 40)
        {
            ChangeTreeType(TreeType.Original);
            nowSadTreeCount--;
        }
        else
        {
            int randnum = Random.Range(0,7);
            if(randnum<1)
            {
                ChangeTreeType(TreeType.Original);
                nowSadTreeCount--;
            }
        }
    }

    private void ChangeTreeType(TreeType treeType)
    {
        nowTreeType = treeType;
        ChangeTreeData(nowTreeType);
    }

    private void CalculateSolution_RandomNum()
    {
        int randnum = Random.Range(0, 100);

        float treepercent = GoodTreePercentage();
        float percent = environmentManager.Cleanliness - treepercent + treepercent / 2 + ((100 - environmentManager.Cleanliness) / 4);
        //print(string.Format("TreePercent: {0} Clean: {1}", treepercent, environmentManager.Cleanliness));

        if (randnum < percent)
        {
            ChangeTreeType(TreeType.Original);
            nowSadTreeCount--;
        }
    }

    private static float GoodTreePercentage()
    {
        //print(string.Format("nowSadTreeCount: {0} allSadTreeCount: {1}", nowSadTreeCount, allSadTreeCount));
        return (1.0f - (float)nowSadTreeCount / allSadTreeCount) * 100f;
    }
}
public enum TreeType { Original, Sad, Length }

[System.Serializable]
public class MaterialSet
{
    public Material[] materials;
}