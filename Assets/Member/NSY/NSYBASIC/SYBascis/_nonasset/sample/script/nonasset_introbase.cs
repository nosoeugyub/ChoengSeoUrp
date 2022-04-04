using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
using System.Xml;
using DG.Tweening;
public class nonasset_introbase : baseui
{
    [Header("===== [Custom] =====")]
    public int count = 0;
    // Start is called before the first frame update
    protected override void SetLanguage()
    {
    }
    protected override void Start()
    {
        GlobalUtilNonAsset.Instance.SetFirstDeviceLanguage();

        //언어 체인지는 옵션창등 다른 곳에서 함.
        GlobalUtilNonAsset.Instance.SetChangeLanguage(DefaultBaseUtil.LANGUAGETYPE.lng_en);

        //DataLoad        
        LoadSampleLanguageDataXmlDocument();
        //
        //DontDestroyUI의 경우 폰트 재생성을 위해 삭제 후 재생성
        DestroyDontDestroyUI("questCanvas");
        DestroyDontDestroyUI("topCanvas");
        DestroyDontDestroyUI("messageCanvas");
        //

        //DontDestroyUI 생성 예제(message 창이나 경고창등은 처음부터 생성)
        CreateDontDestroy(objs[0]);  //sound

        base.Start();
    }

    void LoadSampleLanguageDataXmlDocument()
    {
        XmlNodeList nodes = GlobalUtilNonAsset.Instance.LoadXmlDocumentUtil("language/sampleunitnames", "word/entry");     
        string xmlstr = "";
        string totalstr = "";
        foreach (XmlNode node in nodes)
        {
            xmlstr = node.SelectSingleNode(DefaultBaseUtil.Instance.xmlkey).InnerText;

            totalstr += xmlstr + "\n";
            /*
        Debug.Log("Name :: " + node.SelectSingleNode(GlobalUtil.Instance.xmlkey).InnerText);
        Debug.Log("Level :: " + node.SelectSingleNode("Level").InnerText);
        Debug.Log("Exp :: " + node.SelectSingleNode("Experience").InnerText);
            */
        }


        texts[0].DOText(totalstr, 1f).SetLoops(-1, LoopType.Yoyo).SetDelay(1f);
    }
    protected override void ButtonFuntion(string btnname, string tagname)
    {
        switch (btnname)
        {
            case "btn1":
                {
                    SetClosePopUp();
                    LoadScene("main");
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
    // Update is called once per frame
    protected override void Update()
    {

    }
}
