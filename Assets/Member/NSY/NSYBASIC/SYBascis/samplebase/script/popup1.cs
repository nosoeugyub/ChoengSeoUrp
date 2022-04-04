using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class popup1 : baseui
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
            case "closebtn":
                {
                    SetClosePopUp();
                }break;
        }
    }


    /*
    // Update is called once per frame
    protected override void Update()
    {
        
    }
    */
}
