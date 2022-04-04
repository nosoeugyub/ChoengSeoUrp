using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JMBasic;
using System.Xml;

/*
public class unitlist
{    
    public List<jsonunitdata> units;
    public unitlist()
    {
        //units = new List<jsonunitdata>();
    }
}
*/
public class loaddatafile_nonasset : baseui
{
    public enum LOADTYPE
    {
        json_load_nonasset = 0,
        xml_load_nonasset,
    }

    [Header("===== [Custom] =====")]
    public LOADTYPE loadtype;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        switch (loadtype)
        {
            case LOADTYPE.json_load_nonasset:
                {
                    LoadJsonUtilDefaultUnitData();                    
                }
                break;
            case LOADTYPE.xml_load_nonasset:
                {
                    LoadDefaultUnitDataXml();
                }
                break;
        }

        for (int i = 0; i < UserData.Instance.defaultunit.Count; i++)
        {
            texts[0].text += UserData.Instance.defaultunit[i].name + "\n";
        }
    }

    void LoadDefaultUnitDataXml()
    {
        UserData.Instance.defaultunit.Clear();
        
        XmlNodeList nodes = GlobalUtilNonAsset.Instance.LoadXmlDocumentUtil("common/sampleunits", "units/entry");        
        foreach (XmlNode node in nodes)
        {
            sampleunitdata unit = new sampleunitdata();

            unit.name = node.SelectSingleNode("name").InnerText;
            unit.atktype = node.SelectSingleNode("atktype").InnerText;
            unit.defaulthp = int.Parse( node.SelectSingleNode("defaulthp").InnerText );
            unit.defaultatk = int.Parse(node.SelectSingleNode("defaultatk").InnerText );
            unit.defaultrot = int.Parse(node.SelectSingleNode("defaultrot").InnerText );
            unit.defaultforcus = int.Parse(node.SelectSingleNode("defaultforcus").InnerText );

            unit.name += " : xml";
            UserData.Instance.defaultunit.Add(unit);
        }        
    }    
    void LoadJsonUtilDefaultUnitData()
    {
        UserData.Instance.defaultunit.Clear();

        TextAsset data = Resources.Load<TextAsset>("data/json/common/sampleunits");
        //var ulist = JsonHelper.FromJson<units[]>(data.text); //동일이지만 키가 item
        var ulist = JsonHelper.FromJsonList<jsonunitobj>(data.text); //키가 items

        foreach (var unit in ulist)
        {
            sampleunitdata unitobj = new sampleunitdata();
            
            unitobj.name += unit.name + " : json\n";
            unitobj.atktype = unit.atktype;
            unitobj.defaulthp = int.Parse(unit.defaulthp);
            unitobj.defaultatk = int.Parse(unit.defaultatk);
            unitobj.defaultrot = int.Parse(unit.defaultrot);
            unitobj.defaultforcus = int.Parse(unit.defaultforcus);

            UserData.Instance.defaultunit.Add(unitobj);
        }
    }

    /*
    [ContextMenu("aa")]
    void TTT()
    {
        //unitlist aa = new unitlist();
        //aa.units = new List<jsonunitdata>();
        //jsonunitobj[] unitarray = new units[5];
        /*
        List<jsonunitobj> unitlist = new List<jsonunitobj>();

        for (int i =0;i< 5; i++)
        {
            jsonunitobj unitobj = new jsonunitobj();

            unitobj.name = i.ToString();
            unitobj.atktype = i.ToString();
            unitobj.defaulthp = i.ToString();
            unitobj.defaultatk =i.ToString();
            unitobj.defaultlot = i.ToString();
            unitobj.defaultforcus = i.ToString();

            unitlist.Add(unitobj);
            //aa.units.Add(a);
        }

        print(JsonHelper.ToJsonList(unitlist, prettyPrint: true));
        //print(JsonHelper.ToJson(unitarray, prettyPrint: true));

        //print(JsonUtility.ToJson(aa,true));        
    }
    //*/

    /*
    // Update is called once per frame
    protected override void Update()
    {
        
    }
    */
}
