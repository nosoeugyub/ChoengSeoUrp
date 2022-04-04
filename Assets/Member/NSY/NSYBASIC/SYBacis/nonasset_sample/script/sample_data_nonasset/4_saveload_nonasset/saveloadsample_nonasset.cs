using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;

public class SampleSaveClassNonAsset
{
    public int index = 0;
    public string name = "";
    public Vector3 pos = Vector3.zero;
}
public class saveloadsample_nonasset : baseui
{
    public enum SAVELOADTYPE
    {
        data_save_nonasset = 0,
        data_load_nonasset,
        class_save_nonasset,
        class_load_nonasset,
    }

    [Header("===== [Custom] =====")]
    public SAVELOADTYPE loadtype;
    // Start is called before the first frame update
    protected override void Start()
    {
        //GlobalUtilNonAsset.Instance.savesecurity = false;

        base.Start();

        switch (loadtype)
        {
            case SAVELOADTYPE.data_save_nonasset:
                {
                    SetSampleDataSave();
                }
                break;
            case SAVELOADTYPE.data_load_nonasset:
                {
                    SetSampleDataLoad();
                }
                break;
            case SAVELOADTYPE.class_save_nonasset:
                {
                    SetSampleClassSave();
                }
                break;
            case SAVELOADTYPE.class_load_nonasset:
                {
                    SetSampleClassLoad();
                }
                break;
        }
    }

    void SetSampleDataSave()
    {
        int hp = 10;
        float atk = 0.5f;
        double bosshp = 9999999999;
        string name = "name_nonasset2";
        string id = "testid";

        //GlobalUtil.Instance.SaveDataUtil("testdata", id, hp, atk, bosshp, name);
        GlobalUtilNonAsset.Instance.SaveDataUtil("testdata_nonasset", id, hp, atk, bosshp, name);

        //변수 1개 사용시
        //GlobalUtilNonAsset.Instance.SaveDataUtil("testdata_nonasset2", hp);

        texts[0].text = "SetSampleDataSave_NonAsset OK\n";
        //print(enstr);   
    }

    void SetSampleDataLoad()
    {
        int aa = -1;
        float bb = 0f;
        double cc = 0;
        string dd = "";
        string ee = "";

        if (!GlobalUtilNonAsset.Instance.isSaveDataCheck("testdata_nonasset"))
        {
            texts[0].text = "SetSampleDataLoad_NonAsset FAILD\n";
            return;
        }

        //변수 1개 사용시
        /*
        int hp = (int)GlobalUtilNonAsset.Instance.LoadDataUtil("testdata_nonasset2")[0];
        texts[0].text = "HP\n" + hp.ToString();
        return;
        */

        var decoding = GlobalUtilNonAsset.Instance.LoadDataUtil("testdata_nonasset");
        if (null == decoding) return;

        ee = (string)decoding[0];
        aa = (int)decoding[1];
        bb = (float)decoding[2];
        cc = (double)decoding[3];
        dd = (string)decoding[4];

        texts[0].text = "SetSampleDataLoad_NonAsset OK\n" + dd;

        print(aa.ToString());
        print(bb.ToString());
        print(cc.ToString());
        print(dd.ToString());
        print(ee.ToString());
    }

    void SetSampleClassSave()
    {
        SampleSaveClassNonAsset sclass = new SampleSaveClassNonAsset();
        sclass.index = 11;
        sclass.name = "sample_nonasset";
        sclass.pos = new Vector3(10f, 100f, 1000f);

        GlobalUtilNonAsset.Instance.SaveClassUtil("testclass_nonasset", sclass);

        texts[0].text = "SetSampleClassSave_NonAsset OK\n";
    }

    void SetSampleClassLoad()
    {
        SampleSaveClassNonAsset sclass = new SampleSaveClassNonAsset();
        sclass = JsonUtility.FromJson<SampleSaveClassNonAsset>(GlobalUtilNonAsset.Instance.LoadClassUtil("testclass_nonasset"));

        texts[0].text = "SetSampleClassLoad_NonAsset OK\n" + sclass.name;
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
