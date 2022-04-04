using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class timecaluemgr_nonasset : baseui
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //intro씬에서 만들어주면됨.
        GlobalUtilNonAsset.Instance.CreateTimes(5);

        //texts[5].text = time4count.ToString();
        //texts[6].text = time5count.ToString();

        //UI연결 예제
        for (int i = 0; i < 5; i++)
        {
            switch (i)
            {
                case 0:
                case 1:
                case 2:
                    {
                        GlobalUtilNonAsset.Instance.ConnectTimeUI(this, i, i, i);
                    }
                    break;
                default:
                    {
                        GlobalUtilNonAsset.Instance.ConnectTimeUI(this, i, i);

                        switch (i)
                        {
                            case 3:
                                {
                                    //GlobalData.Instance.SetTimeAuto(i, 10);
                                }
                                break;
                            case 4:
                                {
                                    GlobalUtilNonAsset.Instance.SetTimeAuto(i, 25, true);
                                }
                                break;
                        }
                    }
                    break;

            }

        }
        GlobalUtilNonAsset.Instance.InitTimes();

        SetTimeCountTxt(3);
        SetTimeCountTxt(4);
        /*
        //방법 1
        GlobalData.Instance.timedatas[3].SetCallbackTimeEnd(SetAddTimeCount);
        GlobalData.Instance.timedatas[4].SetCallbackTimeEnd(SetAddTimeCount);
        /*/
        //방법 2 선택
        for (int i = 0; i < GlobalUtilNonAsset.Instance.timedatas.Count; i++)
        {
            GlobalUtilNonAsset.Instance.timedatas[i].SetCallbackTimeEnd(SetTimeCountTxt);
        }
        //*/

        toggles[0].isOn = GlobalUtilNonAsset.Instance.timedatas[3].isauto;
    }

    void SetTimeCountTxt(int index)
    {
        switch (index)
        {
            case 3:
            case 4:
                {
                    texts[2 + index].text = GlobalUtilNonAsset.Instance.timedatas[index].timevalue.ToString("N0");
                }
                break;
        }
    }
    private void OnApplicationQuit()
    {
        GlobalUtilNonAsset.Instance.QuitTimeCalcu();
    }

    private void OnApplicationPause(bool pause)
    {
        GlobalUtilNonAsset.Instance.PauseCheckTimeCalcu(pause);
    }

    // Update is called once per frame
    protected override void Update()
    {
        GlobalUtilNonAsset.Instance.UpdateTimes();
    }

    protected override void ToggleFuntion(string togglename, string tagname, bool ison)
    {
        switch (togglename)
        {
            case "Toggle1":
                {
                    GlobalUtilNonAsset.Instance.SetTimeAuto(3, 10, ison);
                }
                break;
        }
    }
    protected override void ButtonFuntion(string btnname, string tagname)
    {
        switch (btnname)
        {
            case "btn1":
                {
                    GlobalUtilNonAsset.Instance.SetaddTimes(0, 10);
                }
                break;
            case "btn2":
                {
                    GlobalUtilNonAsset.Instance.SetaddTimes(1, 30);
                }
                break;
            case "btn3":
                {
                    GlobalUtilNonAsset.Instance.SetaddTimes(2, 90);
                }
                break;
        }
    }

}
