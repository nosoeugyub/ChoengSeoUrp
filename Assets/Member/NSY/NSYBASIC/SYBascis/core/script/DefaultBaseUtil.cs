using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
static class FontConstants
{
    public const string TXT_DEFAULT = "Cafe24Dangdanghae"; //����Ʈ (����)
    public const string TXT_KR = "Cafe24Dangdanghae";    //�ѱ���
    public const string TXT_JP = "nishiki-teki";        //�Ϻ���
    public const string TXT_CN = "ZCOOLXiaoWei-Regular"; //�߱��� ��ü
    public const string TXT_TC = "GenSekiGothic-M";     //�߱��� ��ü
    public const string TXT_DE = "Cafe24Dangdanghae";   //���Ͼ�
    public const string TXT_FR = "Cafe24Dangdanghae";   //��������
    public const string TXT_ES = "Cafe24Dangdanghae";   //�����ξ�
    public const string TXT_RU = "Cafe24Dangdanghae";   //���þƾ�

    public const string TMP_DEFAULT = "Cafe24Dangdanghae SDF"; //����Ʈ (����)
    public const string TMP_KR = "Cafe24Dangdanghae SDF";    //�ѱ���
    public const string TMP_JP = "Cafe24Dangdanghae SDF";        //�Ϻ���
    public const string TMP_CN = "Cafe24Dangdanghae SDF"; //�߱��� ��ü
    public const string TMP_TC = "Cafe24Dangdanghae SDF";     //�߱��� ��ü
    public const string TMP_DE = "Cafe24Dangdanghae SDF";   //���Ͼ�
    public const string TMP_FR = "Cafe24Dangdanghae SDF";   //��������
    public const string TMP_ES = "Cafe24Dangdanghae SDF";   //�����ξ�
    public const string TMP_RU = "Cafe24Dangdanghae SDF";   //���þƾ�
}

namespace JMBasic
{
    public class DefaultBaseUtil : Singleton<DefaultBaseUtil>
    {
        public enum LANGUAGETYPE
        {
            lng_en = 0, //����
            lng_kr, //�ѱ��� SystemLanguage.Korean
            lng_jp, //�Ϻ��� SystemLanguage.Japanese
            lng_cn, //��ü SystemLanguage.ChineseSimplified
            lng_tc, //��ü SystemLanguage.ChineseTraditional
            lng_de, //���Ͼ� SystemLanguage.German
            lng_fr, //�������� SystemLanguage.French       
            lng_es, //�����ξ� SystemLanguage.Spanish
            lng_ru, //���þ� SystemLanguage.Russian
            lng_none,
        }

        //��� ����        

        public Font languefont;
        public TMP_FontAsset tmpfont;
        public LANGUAGETYPE ltype;
        public LANGUAGETYPE saveltype;

        //���� ����                
        public sfxmgr.SFXTYPE bgmtype = sfxmgr.SFXTYPE.none;
        public bool issfx = true;
        public bool isbgm = true;
        public string xmlkey;

        //���ö���¡ �ܾ�, �޼���
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
                case LANGUAGETYPE.lng_de: //���Ͼ�
                    {
                        xmlkey = "de";
                    }
                    break;
                case LANGUAGETYPE.lng_fr: //��������        
                    {
                        xmlkey = "fr";
                    }
                    break;
                case LANGUAGETYPE.lng_es: //�����ξ�
                    {
                        xmlkey = "es";
                    }
                    break;
                case LANGUAGETYPE.lng_ru: //���þ�
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

            //�⺻ �ܾ�, ���� �ε�
            //LoadWordDataXml();
            //LoadMessageDataXml();

            //�⺻ �ܾ�, ���� �ε�
            //LoadWordDataXmlDocument();
            //LoadMessageDataXmlDocument();
        }

        //////////////////////////////////////////////////////////////////////////////////    

        //���� �۵� �Լ� ����
        //�ٸ� Ŭ���� ��ũ��Ʈ ���� ����    
        //FindObjectOfType �̰� �ᵵ��
        public T FindObjectScript<T>(string name)
        {
            T script = GameObject.Find(name).GetComponent<T>();
            return script;
        }

        //.ToList();  using System.Linq; �ʿ�
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
