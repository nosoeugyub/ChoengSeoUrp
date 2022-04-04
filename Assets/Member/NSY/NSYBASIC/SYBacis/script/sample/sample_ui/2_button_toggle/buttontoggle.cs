using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class buttontoggle : baseui
{
    int count = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        texts[0].text = count.ToString();
    }

    protected override void ButtonFuntion(string btnname, string tagname)
    {
        switch (btnname)
        {
            case "btn1":
                {
                    count++;
                    texts[0].text = count.ToString();
                }
                break;
        }
    }

    protected override void ToggleFuntion(string togglename, string tagname, bool ison)
    {
        switch (togglename)
        {
            case "toggle1":
                {
                    texts[0].gameObject.SetActive(ison);
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
