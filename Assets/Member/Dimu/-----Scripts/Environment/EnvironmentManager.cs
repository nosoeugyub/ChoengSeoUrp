using DM.Dialog;
using NSY.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] float cleanliness; //0~100

    [SerializeField] List<int> cleanLevels;
    int cleanLevel;
    int days = 0;

    [SerializeField] bool canChange;
    [Range(0, 100)]
    [SerializeField] float _cleanliness; //0~100

    [SerializeField] Material skyBox;

    [SerializeField] Color goodFogColor;//183 239 248
    [SerializeField] Color badFogColor;//58 58 58 

    [SerializeField] float goodFogStartDis;
    [SerializeField] float badFogStartDis;

    [SerializeField] float goodFogEndDis;
    [SerializeField] float badFogEndDis;

    [SerializeField] Color _fogColor;
    //[SerializeField] VolumeProfile volume;
    [SerializeField] Camera maincamera;

    NPCManager npcManager;
    DialogueManager dManager;


    //NSY의 추가 코드
    [Header("낮과 밤")]
    [Header("하루 총시간 ")]
    [Range(0.0f, 1.0f)] [Header("현재 시간 1.0이되면 하루 끝")] public float time;
    public float fullDayLength;
    [Header("시작 시간")] public float startTime = 0.35f;
    [Header(" 걸리는 시간")] private float timeRate;

    [Header("낮")]
    [SerializeField] Light d1;
    [SerializeField] Light d2;
    [SerializeField] Light d3;
    [SerializeField] Light d4;
    public Gradient sunColor;
    public AnimationCurve sunintensity;
    public Material DayMat;
    [SerializeField] Color _DaySkyColor;
    [SerializeField] Color _DayEquatorColor;
    [SerializeField] Color _DayEquatorGroundColor;

    [Header("밤")]
    [SerializeField] Color _lerpSkyColor;
    [SerializeField] Color _lerpEquatorColor;
    [SerializeField] Color _lerpEquatorGroundColor;

    [Header("밤")]
    public Material NightMat;
    public float Waittime = 0.5f;
    [SerializeField] Color _NightSkyColor;
    [SerializeField] Color _NightEquatorColor;
    [SerializeField] Color _NightEquatorGroundColor;

    [Header("다른 빛조명")]
    public AnimationCurve lighingIntensityMultipler;
    public AnimationCurve refloectionsIntensityMultipler;
    //반딧불이
    [SerializeField] GameObject FireFlyEffect;

    //[SerializeField] CraftSlot a;
    [SerializeField] HappyUI happyUI;

    [SerializeField] GameObject endingButton;
    [SerializeField] GameObject endingButtoninfo;

    public float Cleanliness
    {
        get
        {
            return cleanliness;
        }
        set
        {
            cleanliness = value;
            if (cleanliness >= 100)
            {
                endingButton.SetActive(true);
                if (endingButtoninfo)
                    endingButtoninfo.SetActive(true);
            }
            else
            {
                endingButton.SetActive(false);
                if (endingButtoninfo)
                    endingButtoninfo.SetActive(false);

            }

            if (cleanLevel < cleanLevels.Count)
            {
                if (cleanLevels[cleanLevel] <= cleanliness)
                    ComeToPort();
            }

            happyUI.HappyUISetting(Mathf.Max(Mathf.Min(cleanliness, 100), 0));
        }
    }
    public string name = "CreditDemo";
    public GameObject Loading;
    public Screenshot[] screenshot;
    bool isShot = false;
    public void TakePictures()
    {

        StartCoroutine(takepictures());
    }

    public void SetEndingButtonNull()
    {
        //endingButton = null;
        endingButtoninfo = null;
    }
    IEnumerator takepictures()
    {
        if (isShot)
        {
            yield return null;
        }

        isShot = true;
        for (int i = 0; i < screenshot.Length; i++)
        {
            screenshot[i].OnSceenShotEvent();
            Loading.SetActive(true);
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(2f);
        Loading.SetActive(false);
        SuperManager.Instance.scenechagemanage.LoadSceneFadeString(name);

    }

    private void Awake()
    {

        npcManager = FindObjectOfType<NPCManager>();
        dManager = FindObjectOfType<DialogueManager>();

        DIalogEventManager.EventActions[(int)EventEnum.DownClean] += DownCleanliness;
        DIalogEventManager.EventActions[(int)EventEnum.DownCleanDouble] += DownCleanlinessDouble;
    }
    private void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;

        Cleanliness = 0;
    }
    private void FixedUpdate()
    {
        //NSY 코드
        time += timeRate * Time.fixedDeltaTime;
        if (time >= 1.0f)
        {
            timeRate *= -1;
        }

        if (time <= 0.02f)
        {
            timeRate *= -1;
        }

        d1.transform.rotation = Quaternion.Euler(d1.transform.eulerAngles.x, maincamera.transform.eulerAngles.y, d1.transform.eulerAngles.z);
        d2.transform.rotation = Quaternion.Euler(d2.transform.eulerAngles.x, maincamera.transform.eulerAngles.y + 180, d2.transform.eulerAngles.z);


        if (Input.GetKeyDown(KeyCode.P))
        {
            //a.isHaveRecipeItem = true;
            Cleanliness += 3;
            //days++;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //a.isHaveRecipeItem = true;
            Cleanliness -= 3;
            //days++;
        }
        if (canChange)
            Cleanliness = _cleanliness;

        //fog
        _fogColor = ((goodFogColor - badFogColor) / 100 * Cleanliness) + badFogColor;
        RenderSettings.fogColor = _fogColor;

        RenderSettings.fogStartDistance = ((goodFogStartDis - badFogStartDis) / 100 * Cleanliness) + badFogStartDis;
        RenderSettings.fogEndDistance = ((goodFogEndDis - badFogEndDis) / 100 * Cleanliness) + badFogEndDis;
    }

    private void ComeToPort()
    {
        if (npcManager.ComeToPort())
        {
            dManager.UpdateNpcsQuestMark();
            AddCleanLevel();
        }
    }

    public void AddCleanLevel()
    {
        ++cleanLevel;
    }

    IEnumerator Delay()
    {
        timeRate *= -1;
        yield return null;
    }

    public void ChangeCleanliness(float cleanAmount)
    {
        Cleanliness += cleanAmount;
    }

    public void DownCleanliness()
    {
        Cleanliness -= 4;
        DebugText.Instance.SetText("행복도가 감소합니다.");
        DIalogEventManager.EventAction -= DIalogEventManager.EventActions[(int)EventEnum.DownClean];
    }
    public void DownCleanlinessDouble()
    {
        Cleanliness -= 8;
        DebugText.Instance.SetText("행복도가 대폭 감소합니다.");
        DIalogEventManager.EventAction -= DIalogEventManager.EventActions[(int)EventEnum.DownCleanDouble];
    }
}

/* 디무의 코드 보관함
    //[SerializeField] Color goodSkyColor;//     = new Color(90, 167, 255); //90 167 255
    //[SerializeField] Color badSkyColor;//    = new Color(25, 30 ,48);//25 30 48
    //                                   //
    //[SerializeField] Color goodEquatorColor;//        = new Color(158, 241, 255); //158 241 255
    //[SerializeField] Color badEquatorColor;//    = new Color(69, 69, 77); //69 69 77
    //                                       //
    //[SerializeField] Color goodGroundColor;//= new Color(90, 167, 255);//90 167 255
    //[SerializeField] Color badGroundColor;//= new Color(173, 186, 202); //173 186 202
    //                                      //
    //[SerializeField] Color goodCloudColor;// = new Color(255, 255, 255);//90 167 255
    //[SerializeField] Color badCloudColor;//= new Color(155, 155, 155); //173 186 202
    [SerializeField] Color _skyColor;
    [SerializeField] Color _equatorColor;
    [SerializeField] Color _groundColor;
    [SerializeField] Color _cloudColor;

    [SerializeField] float goodIntensity_d1;
    [SerializeField] float badIntensity_d1;

    [SerializeField] float goodIntensity_d2;
    [SerializeField] float badIntensity_d2;

    [SerializeField] float goodIntensity_d3;
    [SerializeField] float badIntensity_d3;
 * 
 * 
 *   //    d1.transform.rotation = Quaternion.Euler(d1.transform.eulerAngles.x, maincamera.transform.eulerAngles.y, d1.transform.eulerAngles.z);
     //   d3.transform.rotation = Quaternion.Euler(d3.transform.eulerAngles.x, maincamera.transform.eulerAngles.y + 180, d3.transform.eulerAngles.z);
 * 
 _skyColor = ((goodSkyColor - badSkyColor) / 100 * Cleanliness) + badSkyColor;
        _equatorColor = ((goodEquatorColor - badEquatorColor) / 100 * Cleanliness) + badEquatorColor;
        _groundColor = ((goodGroundColor - badGroundColor) / 100 * Cleanliness) + badGroundColor;
        _cloudColor = ((goodCloudColor - badCloudColor) / 100 * Cleanliness) + badCloudColor;

        d1.intensity = ((goodIntensity_d1 - badIntensity_d1) / 100 * Cleanliness) + badIntensity_d1;
        d2.intensity = ((goodIntensity_d2 - badIntensity_d2) / 100 * Cleanliness) + badIntensity_d2;
        d3.intensity = ((goodIntensity_d3 - badIntensity_d3) / 100 * Cleanliness) + badIntensity_d3;


        //  skyBox.SetColor("_SkyColor", _skyColor);
      //  skyBox.SetColor("_EquatorColor", _equatorColor);
      //  skyBox.SetColor("_GroundColor", _groundColor);
      //  skyBox.SetColor("_CloudsLightColor", _cloudColor);
*/