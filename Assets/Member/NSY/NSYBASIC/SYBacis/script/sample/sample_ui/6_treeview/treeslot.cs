using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class treeslot : baseui
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void ButtonFuntion(string btnname, string tagname)
    {
        //부모에서 처리하는 경우
        //prent script 부분 수정해야함
        isunfold = !isunfold;
        ScrollParentScript<treeviewsample>().SlotButtonFuntion(slotindex, btnname, this);

        if (treeparentidx == -1)    //챕터
        {
            //GlobalData.Instance.FindObjectScript<mainmgr>("mainmgr").SetMainPage(GlobalData.MAINPAGETYPE.chapter_p, (int)data.chapterIdx);
        }
        else //part
        {
            //GlobalData.Instance.FindObjectScript<mainmgr>("mainmgr").SetMainPage(GlobalData.MAINPAGETYPE.main_p);
            //GlobalData.Instance.FindObjectScript<mainmgr>("mainmgr").SetSectionSlot((int)data.idx);
        }
    }
    /*
    // Update is called once per frame
    protected override void Update()
    {
        
    }
    */
}
