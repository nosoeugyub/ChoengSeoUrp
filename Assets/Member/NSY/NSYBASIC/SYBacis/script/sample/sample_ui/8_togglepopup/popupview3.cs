using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class popupview3 : baseui
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    protected override void ButtonFuntion(string btnname, string tagname)
    {
        switch (btnname)
        {
            case "nextbtn":
                {                    
                    //SceneManager.LoadScene("sample2");
                }
                break;
        }
    }
}
