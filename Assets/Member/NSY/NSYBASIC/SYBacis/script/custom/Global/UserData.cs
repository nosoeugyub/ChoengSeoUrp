using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : Singleton<UserData>
{
    //초기화
    public UserData()
    {
        defaultunit = new List<sampleunitdata>();
        myunits = new List<sampleunitdata>();
        defaultunitnames = new List<string>();        
    }

    public List<sampleunitdata> defaultunit; //class data list sample lor2 140
    public List<sampleunitdata> myunits;  //1,1
    public List<string> defaultunitnames;
    
    public string userid = "testid";
    /*
    public void SaveUserData()
    {        
        ES2.Save(userid, "userid");        
    }

    public void LoadUserData()
    {
        userid = Load(userid, "userid");
        //userid = ES2.Load<string>("totalgold");        
    }
    */
}
