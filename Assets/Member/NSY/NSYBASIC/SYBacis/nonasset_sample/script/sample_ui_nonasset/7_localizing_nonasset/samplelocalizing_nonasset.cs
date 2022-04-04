using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
using System.Xml;
public class samplelocalizing_nonasset : baseui
{
    public enum SAMPLELANGUAGETYPE
    {
        lng_en_nonasset = 0,
        lng_kr_nonasset,
        lng_jp_nonasset,
    }

    [Header("===== [Custom] =====")]
    public SAMPLELANGUAGETYPE sampletype;
    protected override void SetLanguage()
    {
        /*
        texts[0].text = GlobalUtil.Instance.defaultmessages[1];
        texts[1].text = GlobalUtil.Instance.defaultmessages[3];
        texts[2].text = GlobalUtil.Instance.defaultmessages[5];
        /*/
        texts[0].text = UserData.Instance.defaultunitnames[1];
        texts[1].text = UserData.Instance.defaultunitnames[3];
        texts[2].text = UserData.Instance.defaultunitnames[5];
        //*/
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        GlobalUtilNonAsset.Instance.SetFirstDeviceLanguage();

        GlobalUtilNonAsset.Instance.SetChangeLanguage((DefaultBaseUtil.LANGUAGETYPE)sampletype);

        LoadDefaultUnitNameDataXml();

        base.Start();

        /*
        LoadDeaaultUnitNameDataXml();
        LoadDefaultUnitDataXml();

        for (int i = 0; i < UserData.Instance.defaultunit.Count; i++)
        {
            texts[0].text += UserData.Instance.defaultunit[i].name + "\n";
        }
        */
    }
    void LoadDefaultUnitNameDataXml()
    {
        UserData.Instance.defaultunitnames.Clear();

        XmlNodeList nodes = GlobalUtilNonAsset.Instance.LoadXmlDocumentUtil("language/sampleunitnames", "word/entry");
        string xmlstr = "";
        
        foreach (XmlNode node in nodes)
        {
            xmlstr = node.SelectSingleNode(DefaultBaseUtil.Instance.xmlkey).InnerText;

            UserData.Instance.defaultunitnames.Add(xmlstr);            
        }
    }
    protected override void ButtonFuntion(string btnname, string tagname)
    {
        switch (btnname)
        {
            case "closebtn":
                {
                    SetClosePopUp();
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
