using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class scrollslot : baseui
{

    //[HideInInspector]
    //string sortstring;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void OnResetScrollSlot()
    {
        texts[0].text = UserData.Instance.myunits[slotindex].name + slotindex.ToString();
        
        //sortvalue = UserData.Instance.myunits[slotindex].defaultatk;
        //sortstring = UserData.Instance.myunits[slotindex].name;
        //texts[0].text = ScrollParentScript<scrollviewsample>().stringlist[slotindex];
        //texts[0].text = UserData.Instance.defaultunit[slotindex].name;
    }

    protected override void ButtonFuntion(string btnname, string tagname)
    {
        //부모에서 처리하는 경우
        //prent script 부분 수정해야함
        ScrollParentScript<scrollviewsample>().SlotButtonFuntion(slotindex, btnname);

        //슬롯이 직접 처리하는 경우
        switch (btnname)
        {
            case "btn1":
                {
                    SetChangeString(slotindex.ToString() + " : BUTTON1");
                    //texts[0].text = "BUTTON1";
                    //slotindex
                    //scrollslots[index].gameObject
                    //SceneManager.LoadScene("sample2");
                }
                break;
            case "btn2":
                {
                    SetChangeString(slotindex.ToString() + UserData.Instance.myunits[slotindex].name);
                    //texts[0].text = "BUTTON2";
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

    //sample
    public void SetChangeString(string content)
    {
        texts[0].text = content;
    }
}
