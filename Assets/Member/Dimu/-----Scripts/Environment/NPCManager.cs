using DG.Tweening;
using DM.Event;
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
    [SerializeField] Transform StartPos;
    [SerializeField] Transform portInformUI;

    [SerializeField] Transform teleUI;
    [SerializeField] Transform telePickUI;
    [SerializeField] Transform teleFailUI;
    [SerializeField] Button teleUIYesButton;
    AreaType nowSeason = AreaType.Fallter;

    [SerializeField] NPCField nowNpcStandAtPort;

    [SerializeField] GameObject chick;
    [SerializeField] Fader fader;

    [SerializeField] Item removeitem;
    [SerializeField] int removeCount;

    public int NowInteractNPCIndex { get; set; }

    Coroutine nowCor;

    EventContainer eventContainer;

    public NPCField[] NpcTfs
    {
        get { return npcTfs; }
    }
    private void Awake()
    {
        eventContainer = FindObjectOfType<EventContainer>();
    }
    private void Start()
    {
        for (int i = 1; i < npcTfs.Length; i++)
        {
            npcTfs[i].Npctf.GoHomeEvent += GoNPCsHouse;
        }

        DIalogEventManager.BackEventActions[(int)EventEnum.MoveToBearsHouse] += MoveToBearsHouse;
        DIalogEventManager.EventActions[(int)EventEnum.MoveToWalPort] += MoveToWalPort;
        DIalogEventManager.EventActions[(int)EventEnum.GotoBearsWithSheep] += MoveToBearsHouseWithSheep;
        DIalogEventManager.EventActions[(int)EventEnum.GotoBackWithSheep] += MoveToBackSheep;
        DIalogEventManager.BackEventActions[(int)EventEnum.GotoStartPos] += GotoStartPos;

        DIalogEventManager.BackEventActions[(int)EventEnum.ChickenGOBearHOuse] += ChickenGOBearHOuse;
        DIalogEventManager.BackEventActions[(int)EventEnum.ChickenGoSheepHouse] += ChickenGoSheepHouse;
        DIalogEventManager.EventActions[(int)EventEnum.ChickSuddenlyAppear] += ChickSuddenlyAppear;
        DIalogEventManager.EventActions[(int)EventEnum.ChickenAppearAndGetChick] += ChickenAppearAndGetChick;
        DIalogEventManager.BackEventActions[(int)EventEnum.ChickenGone] += ChickenGone;
        DIalogEventManager.EventActions[(int)EventEnum.DearAppear] += DearAppear;
        DIalogEventManager.BackEventActions[(int)EventEnum.SheepDearGone] += SheepDearGone;

        DIalogEventManager.BackEventActions[(int)EventEnum.BearGoSheepHouse] += BearGoSheepHouse;
        DIalogEventManager.BackEventActions[(int)EventEnum.BearGoHisHouse] += BearGoHisHouse;
        //텔포 따로 나눠야 할듯
        //for (int i = 0; i < teleportPosButtons.Length - 1; ++i)
        //{
        //    ButtonInteractable(i, false);
        //}
    }

    public void PlayNPCDialogSound(int npcidx)
    {
        npcTfs[npcidx].Npctf.PlayDialogSound();
    }

    public void ButtonInteractable(int i, bool interactable)
    {
        teleportPosButtons[i].interactable = interactable;
    }
    public void MoveToNPCSomewhere(int npcIdx, Vector3 location)
    {
        npcTfs[npcIdx].Npctf.gameObject.transform.position = location;
        print(npcTfs[npcIdx].Npctf.gameObject.transform.position);
    }
    public void NpcSetActive(int npcIdx, bool isActive)
    {
        npcTfs[npcIdx].Npctf.gameObject.SetActive(isActive);
    }
    public void AllNpcActive(bool isActive)
    {
        for (int i = 0; i < npcTfs.Length; i++)
        {
            NpcSetActive(i, isActive);
        }
    }
    public bool HaveHouse(int npcnum)
    {
        return npcTfs[npcnum].Npctf.IsHaveHouse();
    }

    //ComeToPort
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
            portInformUI.DOLocalMoveY(480, 1).SetEase(Ease.OutQuart);
            if (nowCor != null)
                StopCoroutine(nowCor);
            nowCor = StartCoroutine(ComeToPortCor());
        }
        else
        {
            portInformUI.DOLocalMoveY(620, 1).SetEase(Ease.OutQuart);
        }
    }
    IEnumerator ComeToPortCor()
    {
        yield return new WaitForSeconds(3f);
        ComeToPortUIAction(false);
    }

    public void IOnUI()
    {
    }

    //teleport
    public void OnOfftelePickUI(bool isOn, AreaType areaType)
    {
        nowSeason = areaType;
        telePickUI.gameObject.SetActive(isOn);
    }
    public void OpenTeleportUI(int i)
    {
        if (i == (int)nowSeason)
        {
            teleFailUI.gameObject.SetActive(true);
            teleUI.gameObject.SetActive(false);
        }
        else
        {
            teleFailUI.gameObject.SetActive(false);
            teleUI.gameObject.SetActive(true);
        }
        teleUIYesButton.onClick.RemoveAllListeners();
        teleUIYesButton.onClick.AddListener(() =>
        {
           
            StartCoroutine(TeleportWithFader(i));
        });
    }
    IEnumerator TeleportWithFader(int i)
    {
        eventContainer.RaiseEvent(GameEventType.playerMoveOffEvent);
        eventContainer.RaiseEvent(GameEventType.playerCantInteractEvent);
        yield return fader.IFadeOut(Color.white,0.3f);
        Vector3 randPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(teleportPos[i].position);
        for (int j = 1; j < npcTfs.Length; j++)
        {
            npcTfs[j].Npctf.TeleportToPlayer(teleportPos[i].position + randPos);
        }
        yield return new WaitForSeconds(1);
        npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().InitForward();
        yield return new WaitForSeconds(1);
        yield return fader.IFadeIn(Color.white, 0.3f);
        eventContainer.RaiseEvent(GameEventType.playerMoveOnEvent);
        eventContainer.RaiseEvent(GameEventType.playerCanInteractEvent);
    }
    //////////////event Methods
    public void GoNPCsHouse()
    {
        Vector3 MovePos = npcTfs[NowInteractNPCIndex].Npctf.MyHouse.FriendTransform.position + npcTfs[NowInteractNPCIndex].Npctf.transform.right * 2;
        npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(MovePos);
    }
    public void MoveToBearsHouse()
    {
        MoveToNPCSomewhere(2, npcTfs[1].Npctf.MyHouse.FriendTransform.position);

        Vector3 randPos = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));
        npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(npcTfs[1].Npctf.MyHouse.FriendTransform.position + randPos);

        DIalogEventManager.EventAction -= DIalogEventManager.BackEventActions[(int)EventEnum.MoveToBearsHouse];
    }
    public void MoveToWalPort()
    {
        MoveToNPCSomewhere(2, WalPos.position);
        npcTfs[2].IsField = true;
        npcTfs[2].Npctf.UIOnEvent(2);
        
        DIalogEventManager.EventAction -= DIalogEventManager.EventActions[(int)EventEnum.MoveToWalPort];
    }
    private void MoveToBearsHouseWithSheep()
    {
        MoveToNPCSomewhere(8, npcTfs[1].Npctf.MyHouse.FriendTransform.position);
        Vector3 randPos = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));

        npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(npcTfs[1].Npctf.MyHouse.FriendTransform.position + randPos);
        DIalogEventManager.EventAction -= DIalogEventManager.EventActions[(int)EventEnum.GotoBearsWithSheep];
    }
    private void MoveToBackSheep()
    {
        MoveToNPCSomewhere(8, npcTfs[8].Npctf.MyHouse.HouseOwnerTransform.position);
        npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(npcTfs[8].Npctf.MyHouse.FriendTransform.position);
        DIalogEventManager.EventAction -= DIalogEventManager.EventActions[(int)EventEnum.GotoBackWithSheep];

    }

    private void GotoStartPos()
    {
        DIalogEventManager.EventAction -= DIalogEventManager.BackEventActions[(int)EventEnum.GotoStartPos];
        //npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(StartPos.position);
        StartCoroutine(DelayMove());
    }
    IEnumerator DelayMove()
    {
        eventContainer.RaiseEvent(GameEventType.playerMoveOffEvent);
        eventContainer.RaiseEvent(GameEventType.playerCantInteractEvent);
        yield return fader.IFadeOut(Color.white, 2);
        npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(StartPos.position);
        yield return new WaitForSeconds(1);
        npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().InitForward();
        yield return new WaitForSeconds(1);
        DIalogEventManager.EventAction += DIalogEventManager.EventActions[(int)EventEnum.StartTalk];
        yield return fader.IFadeIn(Color.white, 2);
        eventContainer.RaiseEvent(GameEventType.playerMoveOnEvent);
        eventContainer.RaiseEvent(GameEventType.playerCanInteractEvent);

    }

    private void ChickenGOBearHOuse()
    {
        MoveToNPCSomewhere(3, npcTfs[1].Npctf.MyHouse.FriendTransform.position);
        MoveToNPCSomewhere(1, npcTfs[1].Npctf.MyHouse.HouseOwnerTransform.position);
        MoveToNPCSomewhere(6, npcTfs[6].Npctf.MyHouse.HouseOwnerTransform.position);
        MoveToNPCSomewhere(8, npcTfs[8].Npctf.MyHouse.HouseOwnerTransform.position);
        Vector3 randPos = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));
        npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(npcTfs[1].Npctf.MyHouse.FriendTransform.position + randPos);
        DIalogEventManager.EventAction -= DIalogEventManager.BackEventActions[(int)EventEnum.ChickenGOBearHOuse];
    }
    private void BearGoSheepHouse()
    {
        MoveToNPCSomewhere(1, npcTfs[8].Npctf.MyHouse.FriendTransform.position);
        DIalogEventManager.EventAction -= DIalogEventManager.BackEventActions[(int)EventEnum.BearGoSheepHouse];

    }
    private void ChickenGoSheepHouse()
    {
        Vector3 MovePos = npcTfs[8].Npctf.MyHouse.FriendTransform.position + npcTfs[NowInteractNPCIndex].Npctf.transform.forward * -5;
        Vector3 randPos = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));

        MoveToNPCSomewhere(3, MovePos);

        npcTfs[0].Npctf.GetComponent<PlayerMoveMent>().MoveTowardsTarget(npcTfs[8].Npctf.MyHouse.FriendTransform.position + randPos);
        DIalogEventManager.EventAction -= DIalogEventManager.BackEventActions[(int)EventEnum.ChickenGoSheepHouse];
    }
    private void ChickSuddenlyAppear()
    {
        float chicktpos = chick.transform.position.y;
        chick.transform.position = npcTfs[8].Npctf.MyHouse.FriendTransform.position + npcTfs[1].Npctf.transform.right * 2;
        chick.transform.position = new Vector3(chick.transform.position.x, chicktpos, chick.transform.position.z);
        DIalogEventManager.EventAction -= DIalogEventManager.EventActions[(int)EventEnum.ChickSuddenlyAppear];
    }
    private void ChickenAppearAndGetChick()
    {
        MoveToNPCSomewhere(3, npcTfs[8].Npctf.MyHouse.FriendTransform.position + npcTfs[1].Npctf.transform.right * 4);
        float chicktpos = chick.transform.position.y;
        chick.transform.position = npcTfs[3].Npctf.transform.position + npcTfs[3].Npctf.transform.right * 1.5f;
        chick.transform.position = new Vector3(chick.transform.position.x, chicktpos, chick.transform.position.z);

        DIalogEventManager.EventAction -= DIalogEventManager.EventActions[(int)EventEnum.ChickenAppearAndGetChick];
    }
    private void ChickenGone()
    {
        MoveToNPCSomewhere(3, npcTfs[3].Npctf.MyHouse.HouseOwnerTransform.position);
        float chicktpos = chick.transform.position.y;
        chick.transform.position = npcTfs[3].Npctf.MyHouse.HouseOwnerTransform.position + npcTfs[3].Npctf.transform.right * 1.5f;
        chick.transform.position = new Vector3(chick.transform.position.x, chicktpos, chick.transform.position.z);
        DIalogEventManager.EventAction -= DIalogEventManager.BackEventActions[(int)EventEnum.ChickenGone];
    }
    private void DearAppear()
    {
        MoveToNPCSomewhere(6, npcTfs[8].Npctf.MyHouse.HouseOwnerTransform.position + npcTfs[8].Npctf.transform.right * 2);
        DIalogEventManager.EventAction -= DIalogEventManager.EventActions[(int)EventEnum.DearAppear];
    }
    private void SheepDearGone()
    {
        MoveToNPCSomewhere(6, npcTfs[6].Npctf.MyHouse.HouseOwnerTransform.position);
        MoveToNPCSomewhere(8, npcTfs[6].Npctf.MyHouse.FriendTransform.position);
        DIalogEventManager.EventAction -= DIalogEventManager.BackEventActions[(int)EventEnum.SheepDearGone];
    }

    private void BearGoHisHouse()
    {
        MoveToNPCSomewhere(1, npcTfs[1].Npctf.MyHouse.HouseOwnerTransform.position);
        DIalogEventManager.EventAction -= DIalogEventManager.BackEventActions[(int)EventEnum.BearGoHisHouse];
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