using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class basicui : baseui
{

    // Start is called before the first frame update    
    protected override void SetLanguage()
    {
        tmptexts[0].text = "TMP TEXT!";
    }
    protected override void Start()
    {
        tmpoutline = 0f;
        base.Start();
    }

    /*
    // Update is called once per frame
    protected override void Update()
    {
        
    }
    */



    ///test 튜토리얼과 관련없음
    /*
    List<string> testlist;
    Dictionary<int, string> testdic;

    void ListDicTest()
    {
        int count = 0;
        testlist = new List<string>();
        testdic = new Dictionary<int, string>();

        //list dictionary create
        for (int i = 0; i < 10; i++)
        {
            testlist.Add(count.ToString());
            testdic.Add(count, count.ToString());
            count++;
        }

        //list
        int randnum = Random.RandomRange(0, count);

        print("original_list : " + testlist[9]);
        testlist.RemoveAt(randnum);
        testlist.Add(count.ToString());
        print("del_add_list : " + testlist[9]);

        string dicValue;
        testdic.TryGetValue(9, out dicValue);
        print("original_dic : " + dicValue);
        testdic.Remove(randnum);
        testdic.Add(count, count.ToString());
        //testdic.TryGetValue(9, out dicValue);
        if(testdic.TryGetValue(9, out dicValue))
        print("del_add_dic : " + dicValue);

        count++;        
    }
    */
}
