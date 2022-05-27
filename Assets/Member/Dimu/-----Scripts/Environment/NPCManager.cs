﻿using DG.Tweening;
using DM.NPC;
using NSY.Manager;
using System.Collections;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] NPCField[] npcTfs;
    [SerializeField] Transform PortPos;
    [SerializeField] Transform WalPos;
    [SerializeField] Transform portInformUI;
    [SerializeField] NPCField nowNpcStandAtPort;
    Coroutine nowCor;
    private void Start()
    {
        EventManager.EventActions[3] += MoveToBearsHouse;
        EventManager.EventActions[5] += MoveToWalPort;
    }
    public void ComeToPort()
    {
        SuperManager.Instance.soundManager.PlaySFX("NPCShip");

        int randnum = UnityEngine.Random.Range(3, npcTfs.Length);
        while (npcTfs[randnum].IsField == true)
            randnum = UnityEngine.Random.Range(3, npcTfs.Length);

        npcTfs[randnum].Npctf.gameObject.SetActive(true);

        Vector3 randPos = new Vector3(PortPos.position.x + Random.Range(-1.5f, 1.5f), PortPos.position.y, PortPos.position.z + Random.Range(-1.5f, 1.5f));
        MoveToNPCSomewhere(randnum, randPos);

        npcTfs[randnum].IsField = true;
        ComeToPortUIAction(true);
    }
    private void ComeToPortUIAction(bool isOn)
    {
        if (isOn)
        {
            portInformUI.DOLocalMoveY(500, 1).SetEase(Ease.OutQuart);
            if (nowCor != null)
                StopCoroutine(nowCor);
            nowCor = StartCoroutine(ComeToPortCor());
        }
        else
        {
            portInformUI.DOLocalMoveY(550, 1).SetEase(Ease.OutQuart);
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
    private bool isField;
    [SerializeField] private HouseNpc npc;
    public bool IsField { get { return isField; } set { isField = value; } }
    public HouseNpc Npctf { get { return npc; } set { npc = value; } }
}