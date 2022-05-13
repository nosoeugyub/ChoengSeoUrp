using NSY.Manager;
using System.Collections;
using UnityEngine;

public class TreeObject : MineObject
{

    [SerializeField] Material downMat;
    [SerializeField] GameObject upObj;


    [SerializeField] Tree sadtree;
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
    protected override void ChangeMineState(MineState state)
    {
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
}
