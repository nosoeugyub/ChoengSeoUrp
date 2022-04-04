using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class basepopup : baseui
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void ButtonFuntion(string btnname, string tagname)
    {
        switch (btnname)
        {
            case "optionbtn":
                {
                    DefaultBaseUtil.Instance.FindObjectScript<popup1>("popup1").SetOpenPopUp();
                }
                break;
        }
    }
    
    /*
    // Update is called once per frame
    protected override void Update()
    {
        
    }
    */
}

