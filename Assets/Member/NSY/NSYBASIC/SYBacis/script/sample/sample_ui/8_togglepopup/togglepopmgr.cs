using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class togglepopmgr : baseui
{
    public enum VIEW_TYPE
    {
        view_1 = 0,
        view_2 = 0,
        view_3 = 0,
    }

    public GameObject[] views;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        SetTab(VIEW_TYPE.view_1);
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    void SetTab(VIEW_TYPE type)
    {
        for(int i=0; i< views.Length; i++)
        {
            views[i].SetActive(false);
        }
        views[(int)type].SetActive(true);
    }

    public override void SetOpenPopUp()
    {
        base.SetOpenPopUp();
    }

    protected override void ToggleFuntion(string togglename, string tagname, bool ison)
    {
        int tabnum = int.Parse(togglename);
        SetTab((VIEW_TYPE)tabnum);
        /*
        switch (togglename)
        {
            case "0":
                {
                }
                break;
        }
        */
    }

    protected override void ButtonFuntion(string btnname, string tagname)
    {
        switch (btnname)
        {
            case "closebtn":
                {
                    SetClosePopUp();                    
                }
                break;            
        }
    }
}
