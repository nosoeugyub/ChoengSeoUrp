using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//#define 처럼 사용
static class Constants
{
    public const int TOTALUNIT = 9;
    public const int TOTALMON = 32;
    public const int TOTALLANGUAGE = 3;
}

public class GlobalData : Singleton<GlobalData>
{    
    //초기화
    public GlobalData()
    {

    }

    public int gameverion = 1;
    public int excutecount = 0;
    //public float mapscroll = 1.2f;
    //////////////////////////////////////////////////////////////////////////////////    

    /*
    //save load 전역
    public void SaveData()
    {
        ES2.Save(gameverion, "gameverion");
        ES2.Save(excutecount, "excutecount");        
        //ES2.Save(mapscroll, "mapscroll");

        UserData.Instance.SaveUserData();
        //ES2.Save(UserData.Instance.userid, "userid");
    }

    public void LoadData()
    {
        gameverion = Load(gameverion, "gameverion");
        excutecount = Load(excutecount, "excutecount");        
        //mapscroll = Load(mapscroll, "mapscroll");
        //excutecount = ES2.Load<int>("excutecount");
        //mapscroll = ES2.Load<float>("mapscroll");

        UserData.Instance.LoadUserData();
        //UserData.Instance.userid = ES2.Load<string>("totalgold");
    }
    */
}
