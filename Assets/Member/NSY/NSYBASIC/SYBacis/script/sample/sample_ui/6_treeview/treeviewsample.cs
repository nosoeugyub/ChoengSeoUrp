using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class treeviewsample : baseui
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        AddTreeViewSlot(gameObject, "chpater1"      , 0);
        AddTreeViewSlot(gameObject, "ch1 - part1"   , 1, 0);
        AddTreeViewSlot(gameObject, "ch1 - part2"   , 2, 0);
        AddTreeViewSlot(gameObject, "chpater2"      , 3);
        AddTreeViewSlot(gameObject, "ch2 - part1"   , 4, 3);
        AddTreeViewSlot(gameObject, "ch2 - part2"   , 5, 3);
        AddTreeViewSlot(gameObject, "ch2 - part3"   , 6, 3);
        AddTreeViewSlot(gameObject, "ch2 - part4"   , 7, 3);
        AddTreeViewSlot(gameObject, "chpater3"      , 8);
        AddTreeViewSlot(gameObject, "ch3 - part1"   , 9, 8);
        AddTreeViewSlot(gameObject, "chpater4"      , 10);
        AddTreeViewSlot(gameObject, "ch4 - part1"   , 11, 10);
        AddTreeViewSlot(gameObject, "ch4 - part2"   , 12, 10);
    }

    //parent에서 기능 가져다 쓰기
    public override void SlotButtonFuntion(int index, string btnname, baseui script = null)
    {
        print(index.ToString() + " : " + btnname);
        switch (btnname)
        {
            case "btn1":
                {
                    OnResetTreeView(script);
                    //OnFoldTreeView(script);
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
