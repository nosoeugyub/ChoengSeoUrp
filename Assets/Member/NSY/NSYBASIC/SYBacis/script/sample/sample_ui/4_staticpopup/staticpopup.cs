using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class staticpopup : baseui
{
    public GameObject popupcanvas;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        if( !GameObject.Find(popupcanvas.name) )
        {
            GameObject canvas = Instantiate(popupcanvas);
            canvas.name = popupcanvas.name;
        }
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
            case "nextbtn":
                {
                    LoadScene("staticpopup2");
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
