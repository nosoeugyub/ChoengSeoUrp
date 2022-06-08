using DG.Tweening;
using DM.NPC;
using NSY.Manager;
using NSY.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    [SerializeField] NPCField[] npcTfs;
    [SerializeField] Transform[] teleportPos;
    [SerializeField] Button[] teleportPosButtons;
    [SerializeField] Transform PortPos;
    [SerializeField] Transform WalPos;
    [SerializeField] Transform portInformUI;

    [SerializeField] Transform teleUI;
    [SerializeField] Transform telePickUI;
    [SerializeField] Transform teleFailUI;
    [SerializeField] Button teleUIYesButton;

    [SerializeField] NPCField nowNpcStandAtPort;

    [SerializeField] Item removeitem;
    [SerializeField] int removeCount;

    Coroutine nowCor;

    public NPCField[] NpcTfs
    {
        get { return npcTfs; }
    }

    private void Start()
    {
        EventManager.EventActions[(int)EventEnum.MoveToBearsHouse] += MoveToBearsHouse;
        EventManager.EventActions[(int)EventEnum.MoveToWalPort] += MoveToWalPort;
        EventManager.EventActions[(int)EventEnum.GotoBearsWithSheep] += MoveToBearsHouseWithSheep;
        EventManager.EventActions[(int)EventEnum.GotoBackWithSheep] += MoveToBackSheep;

        //npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().CharacterMove(teleportPos[0].position);

        //teleportPosButtons[0].onClick.AddListener(() => npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(teleportPos[0].position));
        //teleportPosButtons[1].onClick.AddListener(() => npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(teleportPos[1].position));
        //teleportPosButtons[2].onClick.AddListener(() => npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(teleportPos[2].position));
        //teleportPosButtons[3].onClick.AddListener(() => npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(teleportPos[3].position));
        //teleportPosButtons[4].onClick.AddListener(() => npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(teleportPos[4].position));

        for (int i = 0; i < teleportPosButtons.Length; ++i)
        {
            ButtonInteractable(i, false);
        }
    }

    private void MoveToBearsHouseWithSheep()
    {
        MoveToNPCSomewhere(8, npcTfs[1].Npctf.MyHouse.FriendTransform.position);
        Vector3 randPos = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));

        npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(npcTfs[1].Npctf.MyHouse.FriendTransform.position + randPos);
        EventManager.EventAction -= EventManager.EventActions[(int)EventEnum.GotoBearsWithSheep];
    }
    private void MoveToBackSheep()
    {
        MoveToNPCSomewhere(8, npcTfs[8].Npctf.MyHouse.HouseOwnerTransform.position);
        npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(npcTfs[8].Npctf.MyHouse.FriendTransform.position);
        EventManager.EventAction -= EventManager.EventActions[(int)EventEnum.GotoBackWithSheep];

    }
    public void ButtonInteractable(int i, bool interactable)
    {
        teleportPosButtons[i].interactable = interactable;
    }
    public void OnOfftelePickUI(bool isOn)
    {
        telePickUI.gameObject.SetActive(isOn);
    }
    public void OpenTeleportUI(int i)
    {
        teleUI.gameObject.SetActive(true);
        teleUIYesButton.onClick.RemoveAllListeners();
        teleUIYesButton.onClick.AddListener(() =>
        {
            //if (SuperManager.Instance.inventoryManager.RemoveItem(removeitem, removeCount))
            npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(teleportPos[i].position);
            //else
            //teleFailUI.gameObject.SetActive(true);
        });
    }
    public bool ComeToPort()
    {
        if (!npcTfs[2].IsField) return false;

        SuperManager.Instance.soundManager.PlaySFX("NPCShip");

        int randnum = UnityEngine.Random.Range(3, npcTfs.Length);
        while (npcTfs[randnum].IsField == true)
            randnum = UnityEngine.Random.Range(3, npcTfs.Length);

        npcTfs[randnum].Npctf.gameObject.SetActive(true);

        Vector3 randPos = new Vector3(PortPos.position.x + Random.Range(-1.5f, 1.5f), PortPos.position.y, PortPos.position.z + Random.Range(-1.5f, 1.5f));
        MoveToNPCSomewhere(randnum, randPos);

        npcTfs[randnum].IsField = true;
        ComeToPortUIAction(true);

        return true;
    }
    private void ComeToPortUIAction(bool isOn)
    {
        if (isOn)
        {
            portInformUI.DOLocalMoveY(490, 1).SetEase(Ease.OutQuart);
            if (nowCor != null)
                StopCoroutine(nowCor);
            nowCor = StartCoroutine(ComeToPortCor());
        }
        else
        {
            portInformUI.DOLocalMoveY(600, 1).SetEase(Ease.OutQuart);
        }
    }

    IEnumerator ComeToPortCor()
    {
        yield return new WaitForSeconds(3f);
        ComeToPortUIAction(false);
    }

    public void MoveToNPCSomewhere(int npcIdx, Vector3 location)
    {
        npcTfs[npcIdx].Npctf.gameObject.transform.position = location;
        print(npcTfs[npcIdx].Npctf.gameObject.transform.position);
    }
    //////////////event Methods
    public void MoveToBearsHouse()
    {
        MoveToNPCSomewhere(2, npcTfs[1].Npctf.MyHouse.FriendTransform.position);
        EventManager.EventAction -= EventManager.EventActions[3];
    }
    public void MoveToWalPort()
    {
        MoveToNPCSomewhere(2, WalPos.position);
        npcTfs[2].IsField = true;
        EventManager.EventAction -= EventManager.EventActions[5];
    }
    public bool HaveHouse(int npcnum)
    {
        return npcTfs[npcnum].Npctf.IsHaveHouse();
    }
}
[System.Serializable]
public class NPCField
{
    [SerializeField] private bool isField;
    [SerializeField] private HouseNpc npc;
    public bool IsField { get { return isField; } set { isField = value; } }
    public HouseNpc Npctf { get { return npc; } set { npc = value; } }
}