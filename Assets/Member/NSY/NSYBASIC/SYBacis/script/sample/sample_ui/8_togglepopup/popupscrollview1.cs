using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class popupscrollview1 : baseui
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        CreateScrollView(SCROLLTYPE.vritical_scroll, gameObject, 10);
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
