using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JMBasic;
public class timecalcudata_nonasset
{
    int state = 0;
    long savetime = 0;
    int addtime = 0;
    int autoaddtime = 0;
    float timetick = 1.0f;
    int second = 0;
    int minute = 0;
    public float timevalue = 0f;

    public int index = 0;
    public bool isauto = false;
    public string timestr;
    public Text connecttext;
    public Button connectbtn;

    public delegate void CallbackTimeEnd(int indexnum);
    private CallbackTimeEnd callbacktimeend = null;

    public timecalcudata_nonasset()
    {

    }
    /*
    string SetNowTimeEncoding()
    {
        string str = "";
        str += state.ToString() + "_";
        str += savetime.ToString() + "_";
        str += addtime.ToString() + "_";
        str += isauto.ToString() + "_";
        str += timevalue.ToString() + "_";
        return str;
    }
    void SetNowTimeDecoding(string timestr)
    {
        if (timestr.Length == 0) return;

        char sp = '_';
        string[] spstring = timestr.Split(sp);

        state = int.Parse(spstring[0]);
        savetime = long.Parse(spstring[1]);
        addtime = int.Parse(spstring[2]);
        isauto = bool.Parse(spstring[3]);
        timevalue = float.Parse(spstring[4]);
    }
    */

    public void NowSaveTime()
    {
        savetime = DateTime.Now.ToBinary();

        string str = index.ToString() + "t_time_nonasset";
        //string timestr = SetNowTimeEncoding();
        //ES2.Save(timestr, str);
        GlobalUtilNonAsset.Instance.SaveDataUtil(str, state, savetime, addtime, isauto, timevalue);
    }

    public void LoadTime()
    {
        string str = index.ToString() + "t_time_nonasset";

        if (false == GlobalUtilNonAsset.Instance.isSaveDataCheck(str)) return;

        var items = GlobalUtilNonAsset.Instance.LoadDataUtil(str);
        state = (int)items[0];
        savetime = (long)items[1];
        addtime = (int)items[2];
        isauto = (bool)items[3];
        timevalue = (float)items[4];

        //string timestr = ES2.Load<string>(str);
        //SetNowTimeDecoding(timestr);
    }

    public void PauseCheckTimeCalcu(bool ispause)
    {
        switch (state)
        {
            case 1:
                {
                    if (ispause)
                    {
                        NowSaveTime();
                    }
                    else
                    {
                        InitTimeCalcu();
                    }
                }
                break;
        }
    }
    public void SetCallbackTimeEnd(CallbackTimeEnd cal)
    {
        callbacktimeend = cal;
    }
    void SetState(int s)
    {
        state = s;
        switch (state)
        {
            case 0:
                {
                    timestr = "TIME";

                    if (null != connecttext)
                    {
                        connecttext.text = timestr;
                    }
                    if (null != connectbtn)
                    {
                        connectbtn.enabled = true;
                    }

                    if (isauto)
                    {
                        SetAddTimeCalcu();
                    }

                    //이때 먼가 제스쳐를 취해야 한다.
                    if (null != callbacktimeend)
                    {
                        timevalue += 1f; //test
                        callbacktimeend(index);
                    }
                }
                break;
            case 1:
                {
                    if (null != connectbtn)
                    {
                        connectbtn.enabled = false;
                    }
                }
                break;
        }
    }
    public void SetTimeAuto(int add, bool auto)
    {
        isauto = auto;
        autoaddtime = add;
        if (isauto)
        {
            InitTimeCalcu();
        }
    }
    public void SetAddTimeCalcu(int add = -1)
    {
        if (-1 == add)
        {
            add = autoaddtime;
        }
        switch (state)
        {
            case 0:
                {
                    addtime = add;
                    NowSaveTime();

                    SetState(1);
                    timetick = 1.0f;

                    //connectobj.SendMessage("ApplyDamage", 5.0F);
                }
                break;
        }
    }

    public void InitTimeCalcu()
    {
        switch (state)
        {
            case 0:
                {
                    SetState(0);
                }
                break;
            case 1:
                {
                    TimeSpan ts = DateTime.Now - DateTime.FromBinary(savetime);
                    long ctime = Convert.ToInt64(ts.TotalSeconds);
                    addtime -= (int)ctime;
                    if (addtime <= 0)
                    {
                        addtime = 1;
                    }
                    timetick = 1.0f;
                    NowSaveTime();

                    SetState(1);
                    //globalsingleton.Instance.SaveDefaultTime();
                }
                break;
        }
    }


    public void UpdateTimeCalcu()
    {
        switch (state)
        {
            case 1:
                {
                    timetick += Time.deltaTime;
                    if (timetick >= 1.0f)
                    {
                        timetick = 0.0f;
                        addtime--;

                        second = addtime;
                        if (second > 59)
                        {
                            minute = (second / 60);
                            second -= (minute * 60);
                        }
                        else
                        {
                            minute = 0;
                        }
                        //timestr = string.Format("{0:00}:{1:00}", minute, second);
                        timestr = string.Format("{0:00} : {1:00} : {2:00}", (minute / 60), (minute % 60), second);

                        if (null != connecttext)
                        {
                            connecttext.text = timestr;
                        }

                        if (second <= 0)
                        {
                            minute--;
                            if (minute < 0)
                            {
                                SetState(0);
                            }
                        }
                    }
                }
                break;
        }
    }
}
