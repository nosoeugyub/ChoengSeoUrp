using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class sfxmgr : MonoBehaviour
{
    //ui 음 계속 추가
    public enum SFXTYPE
    {
        none = 0,
        titlebgm,
        mainbgm,
        worldbgm,
        gamebgm1,
        gamebgm2,
        gamebgm3,
        gamebgm4,
        touch,
        cancle,
        gameover,
        gamewin,
        ready,
        skillready,
        skillon,
        cri,
        die,
        waterfall,
        splash,
        blind,
        fire,
        ice,
        stun,
        poison,
        confusion,
        haste,
        lvup,
        bronze,
        silver,
        gold,
        atkup,
        atkbuff,
        heal,
        aeheal,
        step,
        talk1,
        talk2,
        talk3,
        talk4,
        female_open,
        male_open,
        Fight_Female1,
        ItsOver_Female1,
        Victory_Female1,
        Combo_Female1,
        ComboBreaker_Female1,
        Special_Female1,
        Perfect_Female1,
        OnTop_Female1,
        Nice_Female1,
        KnockOut_Female1,
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void LoadSFX(Transform sfxtransform, SFXTYPE type, int num)
    {
        string typename = "touch";
        string pathname = "";

        switch (type)
        {
            case SFXTYPE.gamebgm1:
                {
                    typename = "gamebgm";
                    pathname = "bgm/" + (num + 1).ToString();
                }
                break;
        }

        string path = "sound/" + pathname;
        Transform sfx = sfxtransform.Find(typename);

        AudioSource asc = sfx.gameObject.GetComponent<AudioSource>();
        asc.clip = Resources.Load<AudioClip>(path) as AudioClip;
    }
    public static void PlaySound(SFXTYPE type, bool isbgm = false, Transform sfxtransform = null, bool overlap = true)
    {
        string typename = "button1";
        typename = GetTypeString(type);

        if (null == sfxtransform)
        {
            GameObject soundmgr = GameObject.Find("SoundMgr");
            if (null == soundmgr) return;
            sfxtransform = soundmgr.transform;
        }
        Transform sfx = sfxtransform.Find(typename);        

        if (isbgm)
        {
            if (DefaultBaseUtil.Instance.isbgm)
            {
                if (sfx.gameObject.GetComponent<AudioSource>().isPlaying) return;

                sfx.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            if (DefaultBaseUtil.Instance.issfx)
            {
                if (!overlap)
                {
                    if (sfx.gameObject.GetComponent<AudioSource>().isPlaying) return;
                }
                sfx.gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }
    public static void UnPauseSound(SFXTYPE type, Transform sfxtransform = null)
    {
        string typename = "button1";
        typename = GetTypeString(type);

        if (null == sfxtransform)
        {
            sfxtransform = GameObject.Find("SoundMgr").transform;
        }
        Transform sfx = sfxtransform.Find(typename);

        //if(GlobalData.Instance.issfx)
        {
            sfx.gameObject.GetComponent<AudioSource>().UnPause();
        }
    }

    public static void PauseSound(SFXTYPE type, Transform sfxtransform = null)
    {
        string typename = "button1";
        typename = GetTypeString(type);

        if (null == sfxtransform)
        {
            sfxtransform = GameObject.Find("SoundMgr").transform;
        }
        Transform sfx = sfxtransform.Find(typename);

        //if(GlobalData.Instance.issfx)
        {
            sfx.gameObject.GetComponent<AudioSource>().Pause();
        }
    }

    public static void StopSound(SFXTYPE type, Transform sfxtransform = null)
    {
        string typename = "button1";
        typename = GetTypeString(type);

        if (null == sfxtransform)
        {
            sfxtransform = GameObject.Find("SoundMgr").transform;
        }
        Transform sfx = sfxtransform.Find(typename);

        //if(isbgm)
        {
            //if (GlobalData.Instance.isbgm)
            {
                sfx.gameObject.GetComponent<AudioSource>().Stop();
            }
        }
        //else
        //{
        //if (GlobalData.Instance.issfx)
        //{
        //sfx.gameObject.GetComponent<AudioSource>().Stop();
        //}
        //}        
    }
    static string GetTypeString(SFXTYPE type)
    {
        string typename = "button1";
        switch (type)
        {
            case SFXTYPE.titlebgm:
                {
                    typename = "bgm1";
                }
                break;
            case SFXTYPE.mainbgm:
                {
                    typename = "bgm1";
                }
                break;
            case SFXTYPE.worldbgm:
                {
                    typename = "bgm2";
                }
                break;
            case SFXTYPE.gamebgm1:
                {
                    typename = "bgm3";
                }
                break;
            case SFXTYPE.gamebgm2:
                {
                    typename = "bgm4";
                }
                break;
            case SFXTYPE.gamebgm3:
                {
                    typename = "bgm5";
                }
                break;
            case SFXTYPE.gamebgm4:
                {
                    typename = "bgm6";
                }
                break;
            case SFXTYPE.touch:
                {
                    typename = "button1";
                }
                break;
            case SFXTYPE.cancle:
                {
                    typename = "button2";
                }
                break;
            case SFXTYPE.gameover:
                {
                    typename = "lose";
                }
                break;
            case SFXTYPE.gamewin:
                {
                    typename = "win";
                }
                break;
            case SFXTYPE.ready:
                {
                    typename = "ready";
                }
                break;
            case SFXTYPE.skillready:
                {
                    typename = "activeready";
                }
                break;
            case SFXTYPE.skillon:
                {
                    typename = "active";
                }
                break;
            case SFXTYPE.cri:
                {
                    typename = "cri";
                }
                break;
            case SFXTYPE.die:
                {
                    typename = "die";
                }
                break;
            case SFXTYPE.waterfall:
                {
                    typename = "waterfall";
                }
                break;
            case SFXTYPE.splash:
                {
                    typename = "splash";
                }
                break;
            case SFXTYPE.blind:
                {
                    typename = "blind";
                }
                break;
            case SFXTYPE.fire:
                {
                    typename = "fire";
                }
                break;
            case SFXTYPE.ice:
                {
                    typename = "ice";
                }
                break;
            case SFXTYPE.stun:
                {
                    typename = "stun";
                }
                break;
            case SFXTYPE.poison:
                {
                    typename = "poison";
                }
                break;
            case SFXTYPE.confusion:
                {
                    typename = "confusion";
                }
                break;
            case SFXTYPE.haste:
                {
                    typename = "haste";
                }
                break;
            case SFXTYPE.lvup:
                {
                    typename = "lvup";
                }
                break;
            case SFXTYPE.bronze:
                {
                    typename = "bronze";
                }
                break;
            case SFXTYPE.silver:
                {
                    typename = "silver";
                }
                break;
            case SFXTYPE.gold:
                {
                    typename = "gold";
                }
                break;
            case SFXTYPE.atkup:
                {
                    typename = "atkup";
                }
                break;
            case SFXTYPE.atkbuff:
                {
                    typename = "atkbuff";
                }
                break;
            case SFXTYPE.heal:
                {
                    typename = "heal";
                }
                break;
            case SFXTYPE.aeheal:
                {
                    typename = "aeheal";
                }
                break;
            case SFXTYPE.step:
                {
                    typename = "step";
                }
                break;
            case SFXTYPE.talk1:
                {
                    typename = "talk1";
                }
                break;
            case SFXTYPE.talk2:
                {
                    typename = "talk2";
                }
                break;
            case SFXTYPE.talk3:
                {
                    typename = "talk3";
                }
                break;
            case SFXTYPE.talk4:
                {
                    typename = "talk4";
                }
                break;
            case SFXTYPE.female_open:
                {
                    typename = "female_open";
                }
                break;
            case SFXTYPE.male_open:
                {
                    typename = "male_open";
                }
                break;
            case SFXTYPE.Fight_Female1:
                {
                    typename = "Fight_Female1";
                }
                break;
            case SFXTYPE.ItsOver_Female1:
                {
                    typename = "ItsOver_Female1";
                }
                break;
            case SFXTYPE.Victory_Female1:
                {
                    typename = "Victory_Female1";
                }
                break;
            case SFXTYPE.Nice_Female1:
                {
                    typename = "Nice_Female1";
                }
                break;
            case SFXTYPE.Special_Female1:
                {
                    typename = "Special_Female1";
                }
                break;
            case SFXTYPE.Perfect_Female1:
                {
                    typename = "Perfect_Female1";
                }
                break;
            case SFXTYPE.OnTop_Female1:
                {
                    typename = "OnTop_Female1";
                }
                break;
            case SFXTYPE.Combo_Female1:
                {
                    typename = "Combo_Female1";
                }
                break;
            case SFXTYPE.ComboBreaker_Female1:
                {
                    typename = "ComboBreaker_Female1";
                }
                break;
            case SFXTYPE.KnockOut_Female1:
                {
                    typename = "KnockOut_Female1";
                }
                break;
        }

        return typename;
    }
}
