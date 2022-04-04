using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
static class FontConstants
{
    public const string TXT_DEFAULT = "Cafe24Dangdanghae"; //디폴트 (영어)
    public const string TXT_KR = "Cafe24Dangdanghae";    //한국어
    public const string TXT_JP = "nishiki-teki";        //일본어
    public const string TXT_CN = "ZCOOLXiaoWei-Regular"; //중국어 간체
    public const string TXT_TC = "GenSekiGothic-M";     //중국어 번체
    public const string TXT_DE = "Cafe24Dangdanghae";   //독일어
    public const string TXT_FR = "Cafe24Dangdanghae";   //프랑스어
    public const string TXT_ES = "Cafe24Dangdanghae";   //스페인어
    public const string TXT_RU = "Cafe24Dangdanghae";   //러시아어

    public const string TMP_DEFAULT = "Cafe24Dangdanghae SDF"; //디폴트 (영어)
    public const string TMP_KR = "Cafe24Dangdanghae SDF";    //한국어
    public const string TMP_JP = "Cafe24Dangdanghae SDF";        //일본어
    public const string TMP_CN = "Cafe24Dangdanghae SDF"; //중국어 간체
    public const string TMP_TC = "Cafe24Dangdanghae SDF";     //중국어 번체
    public const string TMP_DE = "Cafe24Dangdanghae SDF";   //독일어
    public const string TMP_FR = "Cafe24Dangdanghae SDF";   //프랑스어
    public const string TMP_ES = "Cafe24Dangdanghae SDF";   //스페인어
    public const string TMP_RU = "Cafe24Dangdanghae SDF";   //러시아어
}

namespace JMBasic
{
    public class DefaultBaseUtil : Singleton<DefaultBaseUtil>
    {
        public enum LANGUAGETYPE
        {
            lng_en = 0, //영어
            lng_kr, //한국어 SystemLanguage.Korean
            lng_jp, //일본어 SystemLanguage.Japanese
            lng_cn, //간체 SystemLanguage.ChineseSimplified
            lng_tc, //번체 SystemLanguage.ChineseTraditional
            lng_de, //독일어 SystemLanguage.German
            lng_fr, //프랑스어 SystemLanguage.French       
            lng_es, //스페인어 SystemLanguage.Spanish
            lng_ru, //러시아 SystemLanguage.Russian
            lng_none,
        }

        //언어 관련        

        public Font languefont;
        public TMP_FontAsset tmpfont;
        public LANGUAGETYPE ltype;
        public LANGUAGETYPE saveltype;

        //사운드 관련                
        public sfxmgr.SFXTYPE bgmtype = sfxmgr.SFXTYPE.none;
        public bool issfx = true;
        public bool isbgm = true;
        public string xmlkey;

        //로컬라이징 단어, 메세지
        public List<string> defaultwords;
        public List<string> defaultmessages;

        public DefaultBaseUtil()
        {
            ltype = LANGUAGETYPE.lng_en;
            saveltype = LANGUAGETYPE.lng_none;
            xmlkey = "en";
            defaultwords = new List<string>();
            defaultmessages = new List<string>();
        }
        public void CheckLanguage()
        {
            switch (ltype)
            {
                case LANGUAGETYPE.lng_kr:
                    {
                        xmlkey = "ko";
                    }
                    break;
                case LANGUAGETYPE.lng_jp:
                    {
                        xmlkey = "jp";
                    }
                    break;
                case LANGUAGETYPE.lng_cn:
                    {
                        xmlkey = "cn";
                    }
                    break;
                case LANGUAGETYPE.lng_tc:
                    {
                        xmlkey = "tc";
                    }
                    break;
                case LANGUAGETYPE.lng_de: //독일어
                    {
                        xmlkey = "de";
                    }
                    break;
                case LANGUAGETYPE.lng_fr: //프랑스어        
                    {
                        xmlkey = "fr";
                    }
                    break;
                case LANGUAGETYPE.lng_es: //스페인어
                    {
                        xmlkey = "es";
                    }
                    break;
                case LANGUAGETYPE.lng_ru: //러시아
                    {
                        xmlkey = "ru";
                    }
                    break;
                default:
                    {
                        xmlkey = "en";
                    }
                    break;
            }
        }
        public void LoadFont()
        {
            if (ltype == saveltype) return;
            saveltype = ltype;

            switch (ltype)
            {
                case DefaultBaseUtil.LANGUAGETYPE.lng_kr:
                    {
                        languefont = Resources.Load<Font>("font/" + FontConstants.TXT_KR);
                        tmpfont = Resources.Load<TMP_FontAsset>("tmpfont/" + FontConstants.TMP_KR);
                    }
                    break;
                case DefaultBaseUtil.LANGUAGETYPE.lng_jp:
                    {
                        languefont = Resources.Load<Font>("font/" + FontConstants.TXT_JP);
                        tmpfont = Resources.Load<TMP_FontAsset>("tmpfont/" + FontConstants.TMP_JP);
                    }
                    break;
                case DefaultBaseUtil.LANGUAGETYPE.lng_cn:
                    {
                        languefont = Resources.Load<Font>("font/" + FontConstants.TXT_CN);
                        tmpfont = Resources.Load<TMP_FontAsset>("tmpfont/" + FontConstants.TMP_CN);
                    }
                    break;
                case DefaultBaseUtil.LANGUAGETYPE.lng_tc:
                    {
                        languefont = Resources.Load<Font>("font/" + FontConstants.TXT_TC);
                        tmpfont = Resources.Load<TMP_FontAsset>("tmpfont/" + FontConstants.TMP_TC);
                    }
                    break;
                case DefaultBaseUtil.LANGUAGETYPE.lng_de:
                    {
                        languefont = Resources.Load<Font>("font/" + FontConstants.TXT_DE);
                        tmpfont = Resources.Load<TMP_FontAsset>("tmpfont/" + FontConstants.TMP_DE);
                    }
                    break;
                case DefaultBaseUtil.LANGUAGETYPE.lng_fr:
                    {
                        languefont = Resources.Load<Font>("font/" + FontConstants.TXT_FR);
                        tmpfont = Resources.Load<TMP_FontAsset>("tmpfont/" + FontConstants.TMP_FR);
                    }
                    break;
                case DefaultBaseUtil.LANGUAGETYPE.lng_es:
                    {
                        languefont = Resources.Load<Font>("font/" + FontConstants.TXT_ES);
                        tmpfont = Resources.Load<TMP_FontAsset>("tmpfont/" + FontConstants.TMP_ES);
                    }
                    break;
                case DefaultBaseUtil.LANGUAGETYPE.lng_ru:
                    {
                        languefont = Resources.Load<Font>("font/" + FontConstants.TXT_RU);
                        tmpfont = Resources.Load<TMP_FontAsset>("tmpfont/" + FontConstants.TMP_RU);
                    }
                    break;
                default:
                    {
                        languefont = Resources.Load<Font>("font/" + FontConstants.TXT_DEFAULT);
                        tmpfont = Resources.Load<TMP_FontAsset>("tmpfont/" + FontConstants.TMP_DEFAULT);
                    }
                    break;
            }

            CheckLanguage();

            //기본 단어, 문장 로드
            //LoadWordDataXml();
            //LoadMessageDataXml();

            //기본 단어, 문장 로드
            //LoadWordDataXmlDocument();
            //LoadMessageDataXmlDocument();
        }

        //////////////////////////////////////////////////////////////////////////////////    

        //전역 작동 함수 선언
        //다른 클래스 스크립트 실행 쉽게    
        //FindObjectOfType 이거 써도됨
        public T FindObjectScript<T>(string name)
        {
            T script = GameObject.Find(name).GetComponent<T>();
            return script;
        }

        //.ToList();  using System.Linq; 필요
        public List<T> CopyList<T>(List<T> originallist)
        {
            //List<T> copylist = originallist.ToList();
            List<T> copylist = new List<T>(originallist);
            return copylist;
        }
        public void SetDebugPrint(string beforestr, object message, int value = 0)
        {
            switch (value)
            {
                case 4:
                    {
                        Debug.Log("[" + beforestr + "]" + " == " + message);
                    }
                    break;
            }
        }
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com");
#else
        Application.Quit(0);
#endif
        }

    }
}
