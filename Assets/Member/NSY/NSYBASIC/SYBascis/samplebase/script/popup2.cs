using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class popup2 : baseui
{
    int count = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        DontDestroyOnLoad(gameObject);

        texts[0].text = "STATIC";
    }

    public override void SetOpenPopUp()
    {
        base.SetOpenPopUp();

        count++;
        texts[1].text = count.ToString();
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
    /*
    // Update is called once per frame
    protected override void Update()
    {
        
    }
    */
}
