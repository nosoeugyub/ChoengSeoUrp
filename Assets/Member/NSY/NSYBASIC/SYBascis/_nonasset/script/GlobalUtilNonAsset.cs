using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JMBasic;
using TMPro;
using System.Xml;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace JMBasic
{
    public class GlobalUtilNonAsset : Singleton<GlobalUtilNonAsset>
    {       
        //초기화
        public GlobalUtilNonAsset()
        {
            timedatas = new List<timecalcudata_nonasset>();
        }

        //시간 관련
        public List<timecalcudata_nonasset> timedatas;
        public int timecount = 0;                

        //save관련
        public bool savesecurity = true;        
        //////////////////////////////////////////////////////////////////////////////////    
        /////SAVE, LOAD 디폴트
        public bool isSaveDataCheck(string keystr)
        {
            string path = Application.persistentDataPath;
            return File.Exists(string.Format("{0}/{1}.sav", path, keystr));
        }
        string Decrypt(string toDecrypt)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            // AES-256 key
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            // http://msdn.microsoft.com/en-us/library/system.security.cryptography.ciphermode.aspx
            rDel.Padding = PaddingMode.PKCS7;
            // better lang support
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }

        string Encrypt(string toEncrypt)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            // 256-AES key
            byte[] toEncryptArray  = Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            // http://msdn.microsoft.com/en-us/library/system.security.cryptography.ciphermode.aspx
            rDel.Padding = PaddingMode.PKCS7;
            // better lang support
            ICryptoTransform cTransform  = rDel.CreateEncryptor();
            byte[] resultArray  = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        void DefaultSaveData(string keystr, string datastr)
        {
            //string path = Application.persistentDataPath + "/";
            string path = Application.persistentDataPath;

            FileStream fileStream = new FileStream(string.Format("{0}/{1}.sav", path, keystr), FileMode.Create);

            if(savesecurity)
            {
                string toEncrypt = datastr;
                datastr = Encrypt(toEncrypt);
                //BinaryWriter bw = new BinaryWriter(fileStream);
                //bw.Write(datastr);
                //bw.Close();
            }
            byte[] data = Encoding.UTF8.GetBytes(datastr);
            fileStream.Write(data, 0, data.Length);

            fileStream.Close();            
        }                
        string DefaultLoadData(string keystr)
        {
            string path = Application.persistentDataPath;
            string datastr = "";

            FileStream fileStream = new FileStream(string.Format("{0}/{1}.sav", path, keystr), FileMode.Open);
            byte[] data = new byte[fileStream.Length];//Encoding.UTF8.GetBytes(datastr);
            fileStream.Read(data, 0, data.Length);
            datastr = Encoding.UTF8.GetString(data);

            if (savesecurity)
            {
                string toDecrypt = datastr;
                datastr = Decrypt(toDecrypt);
                //BinaryReader bw = new BinaryReader(fileStream);
                //datastr = bw.ReadString();
                //bw.Close();
            }
            
            fileStream.Close();

            return datastr;
            //return Encoding.UTF8.GetString(data);
        }
        
        //////////////////////////////////////////////////////////////////////////////////    
        /////SAVE, LOAD string 인코딩

        public void SaveClassUtil<T>(string keystr, T c)
        {
            DefaultSaveData(keystr, JsonUtility.ToJson(c));
            //ES2.Save(JsonUtility.ToJson(c), keystr);
        }

        public string LoadClassUtil(string keystr)
        {
            string loadstr = "";
            
            loadstr = DefaultLoadData(keystr);
            //loadstr = Load(loadstr, keystr);
            //c = JsonUtility.FromJson(loadstr);
            return loadstr;
        }
        public void SaveDataUtil(string keystr, params object[] itemlist)
        {
            DefaultSaveData(keystr, SetDataStringEncoding(itemlist));
            //ES2.Save(SetDataStringEncoding(itemlist), keystr);
        }
        public List<object> LoadDataUtil(string keystr)
        {
            string loadstr = "";
            //loadstr = Load(loadstr, keystr);
            loadstr = DefaultLoadData(keystr);

            return GetDataStringDecoding(loadstr);
        }
        string SetDataStringEncoding(params object[] itemlist)
        {
            string totalstr = "";
            string itemstr = "";
            foreach (object item in itemlist)
            {
                Type t = item.GetType();

                if (t.Equals(typeof(int)))
                {
                    itemstr = "i:" + item.ToString();                    
                }
                else if (t.Equals(typeof(float)))
                {
                    itemstr = "f:" + item.ToString();                    
                }
                else if (t.Equals(typeof(double)))
                {
                    itemstr = "d:" + item.ToString();                    
                }
                else if (t.Equals(typeof(long)))
                {
                    itemstr = "l:" + item.ToString();                    
                }
                else if (t.Equals(typeof(bool)))
                {
                    itemstr = "b:" + item.ToString();                    
                }
                
                /*
                else if (t.Equals(typeof(Vector2)))
                {
                    itemstr.Insert(0, "v:");
                }
                else if (t.Equals(typeof(Vector3)))
                {
                    itemstr.Insert(0, "w:");
                }
                */
                else if (t.Equals(typeof(string)))
                {
                    itemstr = "s:" + item.ToString();
                    //itemstr.Insert(0, "s:");
                }
                totalstr += itemstr + "_";
            }
            return totalstr;
        }

        List<object> GetDataStringDecoding(string datastr)
        {
            if (datastr.Length == 0) return null;

            List<object> itemlist = new List<object>();

            char sp = '_';
            string[] spstring = datastr.Split(sp);

            for (int i = 0; i < spstring.Length; i++)
            {
                if (spstring[i].StartsWith("i:"))
                {
                    spstring[i] = spstring[i].Remove(0, 2);
                    itemlist.Add(int.Parse(spstring[i]));
                }
                else if (spstring[i].StartsWith("f:"))
                {
                    spstring[i] = spstring[i].Remove(0, 2);
                    itemlist.Add(float.Parse(spstring[i]));
                }
                else if (spstring[i].StartsWith("d:"))
                {
                    spstring[i] = spstring[i].Remove(0, 2);
                    itemlist.Add(double.Parse(spstring[i]));
                }
                else if (spstring[i].StartsWith("l:"))
                {
                    spstring[i] = spstring[i].Remove(0, 2);
                    itemlist.Add(long.Parse(spstring[i]));
                }
                else if (spstring[i].StartsWith("b:"))
                {
                    spstring[i] = spstring[i].Remove(0, 2);
                    itemlist.Add(bool.Parse(spstring[i]));
                }
                else if (spstring[i].StartsWith("s:"))
                {
                    spstring[i] = spstring[i].Remove(0, 2);
                    itemlist.Add(spstring[i]);
                }
            }

            return itemlist;
        }
        //////////////////////////////////////////////////////////////////////////////////    
        /////JSON 로드
        /*
        //패킷용 string json
        public Hashtable LoadJsonDataUtil(string data)
        {
            Hashtable table = (Hashtable)easy.JSON.JsonDecode(data);
            return table;
        }
        //파일로드용 json
        public Hashtable LoadJsonDataFileUtil(string path)
        {
            TextAsset data = Resources.Load<TextAsset>("data/json/" + path);
            Hashtable table = (Hashtable)easy.JSON.JsonDecode(data.text);
            return table;
        }
        */

        //////////////////////////////////////////////////////////////////////////////////    
        /////XML 로드 및 언어 관련        
        void LoadWordDataXmlDocument()
        {
            DefaultBaseUtil.Instance.defaultwords.Clear();
            XmlNodeList nodes = LoadXmlDocumentUtil("language/words", "word/entry");
            string xmlstr = "";
            foreach (XmlNode node in nodes)
            {
                xmlstr = node.SelectSingleNode(DefaultBaseUtil.Instance.xmlkey).InnerText;
                DefaultBaseUtil.Instance.defaultwords.Add(xmlstr);
            }
            /*
            XMLInStream inStream = LoadXmlDataUtil("language/words");

            string name = "";            
            CheckLanguage();

            inStream.List("entry", delegate (XMLInStream entryStream) {
                entryStream.Content(xmlkey, out name);

                defaultwords.Add(name);
            });
            */
        }
        void LoadMessageDataXmlDocument()
        {
            DefaultBaseUtil.Instance.defaultmessages.Clear();
            XmlNodeList nodes = LoadXmlDocumentUtil("language/messages", "word/entry");
            string xmlstr = "";
            foreach (XmlNode node in nodes)
            {
                xmlstr = node.SelectSingleNode(DefaultBaseUtil.Instance.xmlkey).InnerText;
                DefaultBaseUtil.Instance.defaultmessages.Add(xmlstr);
            }
            /*
            XMLInStream inStream = LoadXmlDataUtil("language/messages");
            string name = "";
            CheckLanguage();

            inStream.List("entry", delegate (XMLInStream entryStream) {
                entryStream.Content(xmlkey, out name);

                defaultmessages.Add(name);
            });
            */
        }        
        public XmlNodeList LoadXmlDocumentUtil(string path, string nodename)
        {
            TextAsset data = Resources.Load<TextAsset>("data/xml/" + path);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(data.text);

            return xmlDoc.SelectNodes(nodename);            
        }

        /*
#if JMBASIC_USE_XMLASSET
        public XMLInStream LoadXmlDataUtil(string path)
        {
            TextAsset data = Resources.Load<TextAsset>("data/xml/" + path);

            XMLInStream inStream = new XMLInStream(data.text); // the XML root (here 'book' is automatically entered to parse the content)            
            return inStream;
        }
#endif
        */        
        public void SetFirstDeviceLanguage()
        {
            int iltype = 0;
            if (false == isSaveDataCheck("ltype_nonasset"))
            {
                switch (Application.systemLanguage)
                {
                    case SystemLanguage.Korean:
                        {
                            DefaultBaseUtil.Instance.ltype = DefaultBaseUtil.LANGUAGETYPE.lng_kr;
                        }
                        break;
                    case SystemLanguage.Japanese:
                        {
                            DefaultBaseUtil.Instance.ltype = DefaultBaseUtil.LANGUAGETYPE.lng_jp;
                        }
                        break;
                        /*
                    case SystemLanguage.ChineseSimplified:
                        {
                            DefaultBaseUtil.Instance.ltype = DefaultBaseUtil.LANGUAGETYPE.lng_cn;
                        }
                        break;
                    case SystemLanguage.ChineseTraditional:
                        {
                            DefaultBaseUtil.Instance.ltype = DefaultBaseUtil.LANGUAGETYPE.lng_tc;
                        }
                        break;
                    case SystemLanguage.German:
                        {
                            DefaultBaseUtil.Instance.ltype = DefaultBaseUtil.LANGUAGETYPE.lng_de;
                        }
                        break;
                    case SystemLanguage.French:
                        {
                            DefaultBaseUtil.Instance.ltype = DefaultBaseUtil.LANGUAGETYPE.lng_fr;
                        }
                        break;
                    case SystemLanguage.Spanish:
                        {
                            DefaultBaseUtil.Instance.ltype = DefaultBaseUtil.LANGUAGETYPE.lng_es;
                        }
                        break;
                    case SystemLanguage.Russian:
                        {
                            DefaultBaseUtil.Instance.ltype = DefaultBaseUtil.LANGUAGETYPE.lng_ru;
                        }
                        break;
                        */
                    default:
                        {
                            DefaultBaseUtil.Instance.ltype = DefaultBaseUtil.LANGUAGETYPE.lng_en;
                        }
                        break;
                }
                iltype = (int)DefaultBaseUtil.Instance.ltype;
                SaveDataUtil("ltype_nonasset", iltype);                
            }
            iltype = (int)LoadDataUtil("ltype_nonasset")[0];
            DefaultBaseUtil.Instance.ltype = (DefaultBaseUtil.LANGUAGETYPE)iltype;
        }
        public void SetChangeLanguage(DefaultBaseUtil.LANGUAGETYPE type)
        {
            DefaultBaseUtil.Instance.ltype = type;
            DefaultBaseUtil.Instance.CheckLanguage();

            if (DefaultBaseUtil.Instance.ltype != type)
            {
                SaveDataUtil("ltype_nonasset", (int)DefaultBaseUtil.Instance.ltype);                
            }
        }
        //////////////////////////////////////////////////////////////////////////////////    
        /////TIME 관련
        ///
        public void CreateTimes(int count)
        {
            timecount = count;
            for (int i = 0; i < timecount; i++)
            {
                timecalcudata_nonasset timedata = new timecalcudata_nonasset();
                timedata.index = i;
                timedatas.Add(timedata);
            }

            if (false == isSaveDataCheck("t_timecount_nonasset"))
            {
                SaveDataUtil("t_timecount_nonasset", timecount);                
            }
            else
            {
                timecount = (int)LoadDataUtil("t_timecount_nonasset")[0];
                for (int i = 0; i < timecount; i++)
                {
                    timedatas[i].LoadTime();
                }
            }
        }

        public void ConnectTimeUI(baseui uisc, int num = -1, int txtnum = -1, int btnnum = -1)
        {
            if (-1 == num)
            {
                for (int i = 0; i < timecount; i++)
                {
                    timedatas[i].connecttext = uisc.texts[i];
                    timedatas[i].connectbtn = uisc.btns[i];
                }
            }
            else
            {
                if (-1 != txtnum)
                {
                    timedatas[num].connecttext = uisc.texts[txtnum];
                }
                if (-1 != btnnum)
                {
                    timedatas[num].connectbtn = uisc.btns[btnnum];
                }
            }
        }
        public void QuitTimeCalcu()
        {
            for (int i = 0; i < timecount; i++)
            {
                timedatas[i].NowSaveTime();
            }
        }
        public void PauseCheckTimeCalcu(bool ispause)
        {
            for (int i = 0; i < timecount; i++)
            {
                timedatas[i].PauseCheckTimeCalcu(ispause);
            }
        }
        public void SetTimeAuto(int num, int addtime, bool isauto)
        {
            timedatas[num].SetTimeAuto(addtime, isauto);
        }
        public void InitTimes()
        {
            for (int i = 0; i < timecount; i++)
            {
                timedatas[i].InitTimeCalcu();
            }
        }
        public void UpdateTimes()
        {
            for (int i = 0; i < timecount; i++)
            {
                timedatas[i].UpdateTimeCalcu();
            }
        }
        public void SetaddTimes(int num, int time)
        {
            timedatas[num].SetAddTimeCalcu(time);
        }

        //////////////////////////////////////////////////////////////////////////////////    
        /////각도 관련
        public Vector2 Get2DPos(Vector3 pos)
        {
            Vector2 pos2d = Vector2.zero;
            Vector3 pos3 = Camera.main.ScreenToWorldPoint(pos);
            pos2d.x = pos3.x;
            pos2d.y = pos3.y;

            return pos2d;
        }

        //dir로 스프라이트 회전값 얻어내기
        public Quaternion GetDirRotationQ(Vector2 dir)
        {
            float anglez = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0.0f, 0.0f, anglez);
        }

        public float GetDirRotationF(Vector2 dir)
        {
            float anglez = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return anglez;
        }

        public Vector2 GetAddDirRotationF(Vector2 dir, float addangle)
        {
            float anglez = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Vector2 vForce = Quaternion.AngleAxis(anglez + addangle, Vector3.forward) * Vector3.right;
            return vForce;
        }
        //////////////////////////////////////////////////////////////////////////////////    
        /////UI와 월드좌표 관련
        public Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
        {
            //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            Vector2 movePos;

            //Convert the screenpoint to ui rectangle local point
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
            //Convert the local point to world point
            return parentCanvas.transform.TransformPoint(movePos);
        }
    }
}
