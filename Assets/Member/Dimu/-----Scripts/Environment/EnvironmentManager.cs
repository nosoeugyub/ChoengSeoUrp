﻿using DG.Tweening;
using DM.Building;
using NSY.Iven;
using NSY.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[ExecuteInEditMode]
public class EnvironmentManager : MonoBehaviour
{
    float cleanliness; //0~100

    [SerializeField] List<int> cleanLevels;
    int cleanLevel;
    public NPCField[] npcTfs;
    public Transform PortPos;
    public Transform portInformUI;
    public NPCField nowNpcStandAtPort;
    int days = 0;

    [SerializeField] bool canChange;
    [Range(0, 100)]
    [SerializeField] float _cleanliness; //0~100
    [SerializeField] Image cleanGageImage;
    [SerializeField] Material skyBox;

    [SerializeField] Color goodFogColor;//183 239 248
    [SerializeField] Color badFogColor;//58 58 58 

    [SerializeField] float goodFogStartDis;
    [SerializeField] float badFogStartDis;

    [SerializeField] float goodFogEndDis;
    [SerializeField] float badFogEndDis;

    [SerializeField] Color goodSkyColor;//     = new Color(90, 167, 255); //90 167 255
    [SerializeField] Color badSkyColor;//    = new Color(25, 30 ,48);//25 30 48
                                       //
    [SerializeField] Color goodEquatorColor;//        = new Color(158, 241, 255); //158 241 255
    [SerializeField] Color badEquatorColor;//    = new Color(69, 69, 77); //69 69 77
                                           //
    [SerializeField] Color goodGroundColor;//= new Color(90, 167, 255);//90 167 255
    [SerializeField] Color badGroundColor;//= new Color(173, 186, 202); //173 186 202
                                          //
    [SerializeField] Color goodCloudColor;// = new Color(255, 255, 255);//90 167 255
    [SerializeField] Color badCloudColor;//= new Color(155, 155, 155); //173 186 202

    [SerializeField] Color _fogColor;
    [SerializeField] Color _skyColor;
    [SerializeField] Color _equatorColor;
    [SerializeField] Color _groundColor;
    [SerializeField] Color _cloudColor;

    [SerializeField] Light d1;
    [SerializeField] float goodIntensity_d1;
    [SerializeField] float badIntensity_d1;
    [SerializeField] Light d2;
    [SerializeField] float goodIntensity_d2;
    [SerializeField] float badIntensity_d2;
    [SerializeField] Light d3;
    [SerializeField] float goodIntensity_d3;
    [SerializeField] float badIntensity_d3;

    [SerializeField] VolumeProfile volume;
    [SerializeField] Camera maincamera;
    Vector3 lookForward;

    Coroutine nowCor;

    [SerializeField] CraftSlot a;
    //[SerializeField] Image fatiGageImage;
    public float Cleanliness
    {
        get
        {
            return cleanliness;
        }
        set
        {
            cleanliness = value;
            cleanGageImage.fillAmount = cleanliness / 100;
        }
    }
    private void Start()
    {
        //fogColor = RenderSettings.fogColor;
        Cleanliness = 0;
    }
    private void Update()
    {
        d1.transform.rotation = Quaternion.Euler(d1.transform.eulerAngles.x, maincamera.transform.eulerAngles.y, d1.transform.eulerAngles.z);
        d3.transform.rotation = Quaternion.Euler(d3.transform.eulerAngles.x, maincamera.transform.eulerAngles.y + 180, d3.transform.eulerAngles.z);


        //RenderSettings.fogColor = fogColor;
        if (Input.GetKeyDown(KeyCode.P))
        {
            //a.isHaveRecipeItem = true;
            Cleanliness +=3;
            //days++;
            //print(days);

        }
        ComeToPort();

        if (canChange)
            Cleanliness = _cleanliness;
        //Cleanliness +=Time.deltaTime*20;

        _fogColor = ((goodFogColor - badFogColor) / 100 * Cleanliness) + badFogColor;
        RenderSettings.fogColor = _fogColor;

        RenderSettings.fogStartDistance = ((goodFogStartDis - badFogStartDis) / 100 * Cleanliness) + badFogStartDis;
        RenderSettings.fogEndDistance = ((goodFogEndDis - badFogEndDis) / 100 * Cleanliness) + badFogEndDis;

        _skyColor = ((goodSkyColor - badSkyColor) / 100 * Cleanliness) + badSkyColor;
        _equatorColor = ((goodEquatorColor - badEquatorColor) / 100 * Cleanliness) + badEquatorColor;
        _groundColor = ((goodGroundColor - badGroundColor) / 100 * Cleanliness) + badGroundColor;
        _cloudColor = ((goodCloudColor - badCloudColor) / 100 * Cleanliness) + badCloudColor;

        d1.intensity = ((goodIntensity_d1 - badIntensity_d1) / 100 * Cleanliness) + badIntensity_d1;
        d2.intensity = ((goodIntensity_d2 - badIntensity_d2) / 100 * Cleanliness) + badIntensity_d2;
        d3.intensity = ((goodIntensity_d3 - badIntensity_d3) / 100 * Cleanliness) + badIntensity_d3;

        skyBox.SetColor("_SkyColor", _skyColor);
        skyBox.SetColor("_EquatorColor", _equatorColor);
        skyBox.SetColor("_GroundColor", _groundColor);
        skyBox.SetColor("_CloudsLightColor", _cloudColor);

        UnityEngine.Rendering.Universal.ColorAdjustments colorAdjustments;
    }

    public void ChangeCleanliness(float cleanAmount)
    {
        Cleanliness += cleanAmount;
    }

    private void ComeToPort()//아침이 왔다
    {
        //if (BuildingBlock.isBuildMode) return;
        if (cleanLevels[cleanLevel] <= Cleanliness)//0레벨기준 10 >= 현재클린 10
        {
            //if (nowNpcStandAtPort != null) //널문제
            //{
            //    if (nowNpcStandAtPort.Npctf != null)
            //    {
            //        nowNpcStandAtPort.Npctf.position = new Vector3(0, 0, 0);
            //        nowNpcStandAtPort.IsField = false;
            //    }
            //}
            SuperManager.Instance.soundManager.PlaySFX("NPCShip");
            int randnum = UnityEngine.Random.Range(0, npcTfs.Length);
            while (npcTfs[randnum].IsField == true)
                randnum = UnityEngine.Random.Range(0, npcTfs.Length);
            ComeToPortUIAction(true);
            //nowNpcStandAtPort = npcTfs[randnum];
            cleanLevel++;
            npcTfs[randnum].Npctf.gameObject.SetActive(true);
            npcTfs[randnum].Npctf.position = PortPos.position;// * Random.Range(1f, 3f) ;
            npcTfs[randnum].IsField = true;

        }
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
    public void PortToHouse()
    {
        //nowNpcStandAtPort = null;
        print("Leave"); ++cleanLevel;
    }
}
[System.Serializable]
public class NPCField
{
    private bool isField;
    [SerializeField] private Transform npctf;
    public bool IsField { get { return isField; } set { isField = value; } }
    public Transform Npctf { get { return npctf; } set { npctf = value; } }
}