using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

using DG.Tweening;


public class SunAndMoon : MonoBehaviour
{
    //반딧불이
    [SerializeField] GameObject FireFlyEffect;

    [Range(0.0f, 1.0f)] [Header("현재 시간 1.0이되면 하루 끝")] public float time;
    [Header("하루 총시간")] public float fullDayLength;
    [Header("시작 시간")] public float startTime = 0.3f;
    [Header(" 걸리는 시간")] private float timeRate;
    public Vector3 SunMoonRotate;
    public Vector3 SunMoonRotate_2;
    [Header("낮")]
    public Light sun;
    public Light sun_2;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;
    [Header("낮 컬러")] [SerializeField] Color _DaySkyColor;
    [SerializeField] Color _DayequatorColor;
    [SerializeField] Color _DaygroundColor;
    [Header("보간  컬러")]
    [SerializeField] Color _DaylerpColor;
    [SerializeField] Color _DaylerptorColor;
    [SerializeField] Color _DaylerpgoundColor;


    [Header("밤")]
    public Light Moon;
    public Light Moon_2;
    public Gradient MoonColor;
    public AnimationCurve MoonIntensity;

    [Header("밤 컬러")] [SerializeField] Color _NightSkyColor;
    [SerializeField] Color _NghitequatorColor;
    [SerializeField] Color _NghitgroundColor;
    [Header("밤 보간 컬러")]
    [SerializeField] Color _NightlerpColor;
    [SerializeField] Color _NightlerptorColor;
    [SerializeField] Color _NightlerpgoundColor;

    [Header("그 외 빛처리")]
    public AnimationCurve LightingIntensityMultiplier;
    public AnimationCurve reflectionsIntensityMultipler;


    [Space(10)]
    [Header("하늘 스카이박스")]
    public Material DaySkyBox;
    
    [Header("하늘 어두워지기 정도")] [SerializeField] AnimationCurve EquatorHeight;
    [Header("하늘 밝아지기 정도")] [SerializeField] AnimationCurve DayEquatorHeight;
    [Header("노을의 지기 정도")] public float startTimes = 0.0f;
    public float DayMayColor;

    private void Start()
    {
        //머테리얼 등록




        timeRate = 1.0f / fullDayLength;
        time = startTime;



    }

    private void FixedUpdate()
    {

        time += timeRate * Time.deltaTime;




        if (time >= 1.0)
        {
            time = 0.0f;

        }
        //해와 달의 회전
        sun.transform.eulerAngles = (time - 0.15f) * SunMoonRotate * 4.0f;
        sun_2.transform.eulerAngles = (time - 0.15f) * SunMoonRotate_2 * 4.0f;

        Moon.transform.eulerAngles = (time - 0.75f) * SunMoonRotate * 4.0f;
        Moon_2.transform.eulerAngles = (time - 0.75f) * SunMoonRotate_2 * 4.0f;


        //빛의세기
        sun.intensity = sunIntensity.Evaluate(time);
        sun_2.intensity = sunIntensity.Evaluate(time);

        Moon.intensity = MoonIntensity.Evaluate(time);
        Moon_2.intensity = MoonIntensity.Evaluate(time);

        sun.color = sunColor.Evaluate(time);
        sun_2.color = sunColor.Evaluate(time);

        Moon.color = MoonColor.Evaluate(time);
        Moon_2.color = MoonColor.Evaluate(time);



        _DaylerpColor = Color.Lerp(_DaySkyColor, _NightSkyColor, time);
        _DaylerptorColor = Color.Lerp(_DayequatorColor, _NghitequatorColor, time);
        _DaylerpgoundColor = Color.Lerp(_DaygroundColor, _NghitgroundColor, time);
        DaySkyBox.SetColor("_SkyColor", _DaylerpColor);
        DaySkyBox.SetColor("_EquatorColor", _DaylerptorColor);
        DaySkyBox.SetColor("_GroundColor", _DaylerpgoundColor);
        DaySkyBox.SetFloat("_EquatorHeight", EquatorHeight.Evaluate(time));

        if (sun.intensity <= 0.13f && sun.gameObject.activeInHierarchy && sun_2.intensity <= 0.13f && sun_2.gameObject.activeInHierarchy)
        {

            sun.gameObject.SetActive(false);
            sun_2.gameObject.SetActive(false);
           

        }
        else if (sun.intensity > 0.3f && !sun.gameObject.activeInHierarchy && sun_2.intensity > 0.3f && !sun_2.gameObject.activeInHierarchy)
        {
            
            sun.gameObject.SetActive(true);
            sun_2.gameObject.SetActive(true);
           
        }


        if (sun.gameObject.activeSelf == false)
        {
           
            Moon.gameObject.SetActive(true);
            Moon_2.gameObject.SetActive(true);
            //밤에서 다시 아침으로
            FireFlyEffect.SetActive(true);
        }
        else
        {
            Moon.gameObject.SetActive(false);
            Moon_2.gameObject.SetActive(false);
            FireFlyEffect.SetActive(false);
        }
         _NightlerpColor = Color.Lerp(_NightSkyColor, _DaySkyColor, time);
        
         _NightlerptorColor = Color.Lerp(_NghitequatorColor, _DayequatorColor, time);
        _NightlerpgoundColor = Color.Lerp(_NghitgroundColor, _DaygroundColor, time);
        DaySkyBox.SetColor("_SkyColor", _NightlerpColor);
        DaySkyBox.SetColor("_EquatorColor", _NightlerptorColor);
        DaySkyBox.SetColor("_GroundColor", _NightlerpgoundColor);
        DaySkyBox.SetFloat("_EquatorHeight", EquatorHeight.Evaluate(time));


        RenderSettings.ambientIntensity = LightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionsIntensityMultipler.Evaluate(time);


    }


}
