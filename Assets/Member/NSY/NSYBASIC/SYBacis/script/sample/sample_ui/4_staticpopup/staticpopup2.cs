using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class staticpopup2 : baseui
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
            case "messagebtn":
                {
                    DefaultBaseUtil.Instance.FindObjectScript<popup2>("messageCanvas").SetOpenPopUp();
                }
                break;
            case "beforbtn":
                {
                    LoadScene("staticpopup1");
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
