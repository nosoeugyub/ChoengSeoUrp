using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sampleunitdata
{
    public int index = 0;
    public int level = 0;
    public int nowexp = 0;
    public int maxexp = 0;
    public bool isopen = false;

    public string name = "";
    public string atktype = "";
    public int defaulthp = 0;
    public int defaultatk = 0;
    public int defaultrot = 0;
    public int defaultforcus = 0;

    public int maxhp = 0;
    public int nowhp = 0;
    public int nowatk = 0;

    public float expvalue = 0.0f;

    public sampleunitdata()
    {        
    }

    public void LevelCalcu()
    {
        float fcalatk = (float)defaultatk;
        //float fatk = (float)defaultatk;
        for(int i=0; i<level; i++)
        {
            fcalatk += ((fcalatk * 0.2f) + 3.0f);
        }
        nowatk = (int)fcalatk;

        float fcalhp = (float)defaulthp;
        //float fhp = (float)defaulthp;
        for (int i = 0; i < level; i++)
        {
            fcalhp += ((fcalhp * 0.2f) + 3.0f);
        }
        nowhp = maxhp = (int)fcalhp;

        maxexp = (level + 1);

        float fmax = maxexp;
        float fnow = nowexp;

        expvalue = (fnow / fmax);
    }

    public bool AddExp()
    {
        bool islevelup = false;
        nowexp++;
        if(nowexp >= maxexp)
        {
            level++;
            nowexp = 0;
            islevelup = true;
        }
        LevelCalcu();

        return islevelup;
    }
}
