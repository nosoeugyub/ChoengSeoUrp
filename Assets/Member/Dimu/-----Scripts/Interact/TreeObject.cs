using NSY.Manager;
using System.Collections;
using UnityEngine;

public class TreeObject : MineObject
{
    [SerializeField] Material downMat;
    [SerializeField] GameObject upObj;
    [SerializeField] Tree sadtree;
    Tree origintree;

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

    public void ChangeTreeData(int treetype)
    {
        if(treetype == (int)TreeType.Original)
        {
            origintree.TreeMat = quad.material;
            origintree.TreeUpMat = upObj.GetComponent<Renderer>().material;
            origintree.TreeDownMat = downMat;

            nowMat = sadtree.TreeMat;
            upObj.GetComponent<Renderer>().material = sadtree.TreeUpMat;
            downMat = sadtree.TreeDownMat;

            if(mineState == MineState.Trunk)
            {
                quad.material = downMat;
            }
        }
    }
}
enum TreeType { Original, Sad, Length}