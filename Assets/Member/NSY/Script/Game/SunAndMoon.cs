using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunAndMoon : MonoBehaviour
{
    [Range(0.0f, 1.0f)]  [Header("현재 시간 1.0이되면 하루 끝")] public float time; 
    [Header("하루 총시간")] public float fullDayLength;
    [Header("시작 시간")] public float startTime = 0.3f;
    [Header(" 걸리는 시간")]  private float timeRate;
    public Vector3 SunMoonRotate;
    public Vector3 SunMoonRotate_2;
    [Header("낮")]
    public Light sun;
    public Light sun_2;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("밤")]
    public Light Moon;
    public Light Moon_2;
    public Gradient MoonColor;
    public AnimationCurve MoonIntensity;

    [Header("그 외 빛처리")]
    public AnimationCurve LightingIntensityMultiplier;
    public AnimationCurve reflectionsIntensityMultipler;


    [Space(10)]
    [Header("하늘 스카이박스")]
    public Material DaySkyBox;
    public Material NightSkyBox;


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

        if (sun.intensity < 0.25f && sun.gameObject.activeInHierarchy && sun_2.intensity < 0.25f && sun_2.gameObject.activeInHierarchy)
        {
            sun.gameObject.SetActive(false);
            sun_2.gameObject.SetActive(false);
        }
        else if(sun.intensity > 0.25f && !sun.gameObject.activeInHierarchy  && sun_2.intensity > 0.25f && !sun_2.gameObject.activeInHierarchy)
        {
            sun.gameObject.SetActive(true);
            sun_2.gameObject.SetActive(true);
            RenderSettings.skybox = DaySkyBox;

          
          
      
        }
        DaySkyBox.GetFloat("EquatorHeight");
        DaySkyBox.SetFloat("EquatorHeight", DayMayColor);
        RenderSettings.skybox.GetFloat("EquatorHeight");
        RenderSettings.skybox.SetFloat("EquatorHeight", DayMayColor);
        Debug.Log(DaySkyBox.GetFloat("EquatorHeight"));

        if (Moon.intensity < 0.25f && Moon.gameObject.activeInHierarchy && Moon_2.intensity < 0.25f && Moon_2.gameObject.activeInHierarchy)
        {
            Moon.gameObject.SetActive(false);
            Moon_2.gameObject.SetActive(false);
        }
        else if (Moon.intensity >= 0.25f && !Moon.gameObject.activeInHierarchy && Moon_2.intensity >= 0.25f && !Moon_2.gameObject.activeInHierarchy)
        {
            Moon.gameObject.SetActive(true);
            Moon_2.gameObject.SetActive(true);
            RenderSettings.skybox = NightSkyBox;
            
        }

        RenderSettings.ambientIntensity = LightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionsIntensityMultipler.Evaluate(time);


    }

 
}
