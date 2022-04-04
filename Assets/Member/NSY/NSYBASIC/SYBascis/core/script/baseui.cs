
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

//ui 관련 관리 및 자동화
//GlobalData와 연계

//1. btn, toggle, txt, img 등 관리
//2. popup 등 관리
//3. scrollview 자동화
//4. multi language 자동화(폰트 및 txt)
//이정민 version 0.80

//version 0.81 추가
//1. scroll slot 중간 아이템 삭제 및 추가시 rect, posion 관리
//3. treeview 추가

//version 0.82 추가
//1. textmeshpro 추가
//2. scroll firstpos 추가

//version 0.83
//1. core 정리
//2. 사운드, 언어 등 util로 이동
//3. GlobalData, UserData 등은 필수요소에서 제외
//4. Assets 폴더 정리
//5. 종속적이 않은 sound 관리(sound는 util과 결합)

//이후 기능 개선 사항

//2. scroll slot 상태에 따른 정렬(value1, value2 등)

static class UIConstants
{
    public const float TREESECONDGAP = 50.0f;
}

namespace JMBasic
{
    public class baseui : MonoBehaviour
    {
        public enum SCROLLTYPE
        {
            vritical_scroll = 0,
            horizontal_scroll,
        }

        [Header ("===== [Commons] =====")]
        //ui Element
        public Sprite[] pics;
        public Image[] images;
        public SpriteRenderer[] sprites;

        [Space(10f)]
        public Button[] btns;
        public Toggle[] toggles;
        public Slider[] sliders;

        [Space(10f)]
        public Text[] texts;
        public InputField[] inputs;
        public TMP_Text[] tmptexts;
        public TMP_InputField[] tmpinputs;

        [Space(10f)]
        public GameObject[] objs;
        //

        [Header("===== [Panel] =====")]
        //popup
        public GameObject popuppanel = null;
        //

        //scroll
        public RectTransform scrollcontent = null;
        public GameObject slotpre = null;        
        
        //[HideInInspector]
        //public SCROLLTYPE scrolltype = SCROLLTYPE.vritical_scroll;
        protected List<GameObject> scrollslots;

        [HideInInspector]
        public GameObject scrollparent;
        [HideInInspector]
        public int slotindex;
        //[HideInInspector]
        //public int sortvalue;

        SCROLLTYPE sctype;
        GameObject scpa;
        int starray;
        float scrollxgap = 10f;
        float scrollygap = 10f;
        float firstscrollpos = 10f;
        //
        //treeview
        [HideInInspector]
        public GameObject treeparent;
        [HideInInspector]
        public bool isunfold = true;
        protected int treeidx = 0;
        protected int treeparentidx = -1;
        //
        //button
        bool istouch = false;
        //
        //numti lng

        protected float tmpoutline = 0f;
        //
        // Start is called before the first frame update
        protected virtual void Start()
        {
            scrollslots = new List<GameObject>();

            if (null != btns)
            {
                foreach (Button btn in btns)
                {
                    btn.onClick.AddListener(() => ButtonClicked(btn.gameObject));
                }
            }
            if (null != sliders)
            {
                foreach (Slider sl in sliders)
                {
                    sl.onValueChanged.AddListener((value) => { SliderClicked(sl.gameObject); });
                }
            }
            if (null != toggles)
            {
                foreach (Toggle toggle in toggles)
                {
                    toggle.onValueChanged.AddListener((value) => { ToggleClicked(toggle.gameObject); });
                }
            }

            SetFontLanguage();
        }

        public void SetFontLanguage()
        {
            DefaultBaseUtil.Instance.LoadFont();

            if (null != texts)
            {
                foreach (Text txt in texts)
                {
                    txt.font = DefaultBaseUtil.Instance.languefont;
                    //txt.fontStyle = FontStyle.Bold;
                }
            }
            if (null != tmptexts)
            {
                foreach (TMP_Text tmptxt in tmptexts)
                {
                    tmptxt.font = DefaultBaseUtil.Instance.tmpfont;
                }
            }
            SetLanguage();
            SetFontShadow();
        }
        protected virtual void SetFontShadow()
        {
            if (null != tmptexts)
            {
                foreach (TMP_Text tmptxt in tmptexts)
                {
                    //tmptxt.GetComponent<TextMeshPro>().outlineWidth = tmpoutline;
                    tmptxt.outlineWidth = tmpoutline;
                }
            }
        }
        public virtual void SetActivePopUpPanel(bool isactive)
        {
            popuppanel.SetActive(isactive);
            istouch = isactive;
        }

        protected void PauseBGM()
        {
            sfxmgr.PauseSound(DefaultBaseUtil.Instance.bgmtype);
        }
        protected void StopBGM()
        {
            sfxmgr.StopSound(DefaultBaseUtil.Instance.bgmtype);
        }
        protected void PlayBGM(sfxmgr.SFXTYPE type)
        {
            //*
            //GameObject soundobj = GameObject.Find("SoundMgr");

            if (DefaultBaseUtil.Instance.isbgm)
            {
                //if (isplay)
                //{
                //if(bgmtype != type)
                //{
                //sfxmgr.StopSound(bgmtype);
                sfxmgr.PlaySound(type, true);
                DefaultBaseUtil.Instance.bgmtype = type;
                //}

                //}
                //else
                //{
                //    sfxmgr.StopSound(soundobj.transform, type);
                //}
            }
            else
            {
                sfxmgr.StopSound(type);
            }
            //*/
        }

        protected void PlaySFX(sfxmgr.SFXTYPE type)
        {
            //*
            //GameObject soundobj = GameObject.Find("SoundMgr");
            if (DefaultBaseUtil.Instance.issfx)
            {
                //if (isplay)
                //{
                sfxmgr.PlaySound(type);
                //}
                //else
                //{
                //    sfxmgr.StopSound(soundobj.transform, type);
                //}
            }
            //*/
            /*
            if (null == GameObject.Find(name))
                return;

            //if (userdata.Instance.issfx)
            {
                if (isplay)
                {
                    GameObject.Find(name).GetComponent<AudioSource>().Play();
                }
                else
                {
                    GameObject.Find(name).GetComponent<AudioSource>().Stop();
                }
            }
            //*/
        }

        protected void StopSFX(sfxmgr.SFXTYPE type)
        {
            GameObject soundobj = GameObject.Find("SoundMgr");
            sfxmgr.StopSound(type);
            /*
            if (null == GameObject.Find(name))
                return;

            //if (userdata.Instance.issfx)
            {
                if (isplay)
                {
                    GameObject.Find(name).GetComponent<AudioSource>().Play();
                }
                else
                {
                    GameObject.Find(name).GetComponent<AudioSource>().Stop();
                }
            }
            */
        }

        protected void ButtonClicked(GameObject btnobj)
        {
            Animation btnani = btnobj.GetComponent<Animation>();
            float delaytime = 0.0f;
            if (btnani != null)
            {
                btnani.Play();
                delaytime = 0.1f;
            }

            if (!istouch)
            {
                istouch = true;
                StartCoroutine(TouchButton(btnobj.name, btnobj.tag, delaytime));
            }

            //GameObject soundobj = GameObject.Find("SoundMgr");
            //if (soundobj)
            {
                switch (btnobj.name)
                {
                    case "closebtn":
                        {
                            PlaySFX(sfxmgr.SFXTYPE.touch);
                        }
                        break;
                    case "canclebtn":
                        {
                            PlaySFX(sfxmgr.SFXTYPE.cancle);
                        }
                        break;
                    case "activeskillbtn":  //예외 버튼들 등록
                    case "talkbtn":
                        break;
                    default:
                        {
                            PlaySFX(sfxmgr.SFXTYPE.touch);
                        }
                        break;
                }
            }
        }
        IEnumerator TouchButton(string btnname, string tagname, float delaytime)
        {
            yield return new WaitForSeconds(delaytime);
            istouch = false;
            ButtonFuntion(btnname, tagname);
        }
        protected void ToggleClicked(GameObject toggleobj)
        {
            Toggle t = toggleobj.GetComponent<Toggle>();
            ToggleFuntion(toggleobj.name, toggleobj.tag, t.isOn);

            Transform checkmarkchild = t.transform.Find("Background").transform.Find("Checkmark").transform.Find("checkpic");
            if (checkmarkchild != null)
            {
                checkmarkchild.gameObject.SetActive(t.isOn);
            }

            GameObject soundobj = GameObject.Find("SoundMgr");
            if (soundobj)
            {
                PlaySFX(sfxmgr.SFXTYPE.touch);
            }
        }

        public virtual void SetOpenPopUp()
        {
            popuppanel.SetActive(true);

            if (null != scrollcontent)
            {
                scrollcontent.transform.localPosition = Vector3.zero;
            }

            Transform childgb = popuppanel.transform.Find("bg");
            if (null != childgb)
            {
                childgb.gameObject.SetActive(true);
                /*
                TweenScale twsc = childgb.gameObject.GetComponent<TweenScale>();
                twsc.enabled = false;
                */
                childgb.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Animation btnani = childgb.gameObject.GetComponent<Animation>();
                btnani.Play();

                //PlaySFX(sfxmgr.SFXTYPE.popupopen);
            }
        }

        public virtual void SetClosePopUp(int state = 0)
        {
            if (istouch) return;

            istouch = true;

            float closetime = 0.0f;
            Transform childgb = popuppanel.transform.Find("bg");
            if (null != childgb)
            {                
                closetime = 0.2f;
                childgb.DOScale(Vector3.zero, closetime).SetEase(Ease.InBack).OnComplete(() => ClosepopupDoTween());
                /*
                TweenScale twsc = childgb.gameObject.GetComponent<TweenScale>();
                twsc.enabled = true;
                twsc.from = new Vector3(1.0f, 1.0f, 1.0f);
                twsc.to = Vector3.zero;
                twsc.ResetToBeginning();
                */                                  
                //StartCoroutine(ClosePopUpIE(closetime));
            }
            else
            {
                popuppanel.SetActive(false);
                istouch = false;
            }
            //*/

        }
        void ClosepopupDoTween()
        {
            popuppanel.SetActive(false);
            istouch = false;
        }

        /*
        IEnumerator ClosePopUpIE(float time)
        {
            yield return new WaitForSeconds(time);
            popuppanel.SetActive(false);
            istouch = false;
        }
        */
        GameObject Find1TreeSlotNum(int idx)
        {
            for (int i = 0; i < scrollslots.Count; i++)
            {
                baseui treeslotsc = scrollslots[i].GetComponent<baseui>();
                if (treeslotsc.treeidx == idx)
                {
                    return scrollslots[i];
                }
            }
            return null;
        }

        //트리뷰
        protected void OnResetTreeView(baseui script)
        {
            for (int i = 0; i < scrollslots.Count; i++)
            {
                baseui slotsc = scrollslots[i].GetComponent<baseui>();
                if (script.gameObject == slotsc.treeparent)
                {
                    scrollslots[i].SetActive(script.isunfold);
                }
            }

            int slotnum = 0;
            for (int i = 0; i < scrollslots.Count; i++)
            {
                if (!scrollslots[i].activeSelf)
                {
                    continue;
                }
                slotnum++;
            }
            ScrollRect scrollsc = scrollcontent.transform.parent.transform.parent.GetComponent<ScrollRect>();
            scrollsc.vertical = true;
            scrollsc.horizontal = false;

            float ygap = 10.0f;

            Vector2 csize = scrollcontent.sizeDelta;
            RectTransform slotrect = scrollslots[0].GetComponent<RectTransform>();
            Vector2 ssize = slotrect.sizeDelta;
            Vector2 spos = Vector2.zero;

            float firstgapy = 10.0f;
            float firstgapx = 0.0f;
            float ymax = firstgapy + ((ssize.y + ygap) * (slotnum + 1));
            csize.y = ymax;
            scrollcontent.sizeDelta = csize;

            int count = 0;
            for (int i = 0; i < scrollslots.Count; i++)
            {
                if (!scrollslots[i].activeSelf)
                {
                    continue;
                }

                firstgapx = 10.0f;
                baseui slotsc = scrollslots[i].GetComponent<baseui>();
                if (slotsc.treeparent != null)
                {
                    firstgapx = UIConstants.TREESECONDGAP;
                }

                slotrect = scrollslots[i].GetComponent<RectTransform>();
                ssize = slotrect.sizeDelta;         //크기
                spos = slotrect.anchoredPosition;   //x,y

                spos.x = firstgapx + (slotrect.sizeDelta.x / 2);
                spos.y = -(firstgapy + (count * (slotrect.sizeDelta.y + ygap)) + ((slotrect.sizeDelta.y / 2)));

                slotrect.anchoredPosition = spos;

                count++;
            }
        }

        protected void InitFold()
        {
            for (int i = 0; i < scrollslots.Count; i++)
            {
                baseui slotsc = scrollslots[i].GetComponent<baseui>();
                if (slotsc.treeparentidx == -1)
                {
                    slotsc.isunfold = !isunfold;
                    OnResetTreeView(slotsc);
                }
            }
        }

        protected void AddTreeViewSlot(GameObject pa, string name, int idx, int parentidx = -1)
        {
            int slotnum = scrollslots.Count;
            //생성
            GameObject slotobj = Instantiate(slotpre, scrollcontent);
            slotobj.name = slotnum.ToString();

            baseui slotsc = slotobj.GetComponent<baseui>();
            slotsc.scrollparent = pa;
            slotsc.treeparent = null;
            slotsc.slotindex = slotnum;
            slotsc.texts[0].text = name;

            scrollslots.Add(slotobj);

            slotsc.treeidx = idx;
            slotsc.treeparentidx = parentidx;
            //        
            if (parentidx != -1)
            {
                slotsc.treeparent = Find1TreeSlotNum(parentidx);
                //secondetreenum = 0;
                //slotsc.secondetreenum = -1;
                //firsttreeslotnum++;
            }
            //slotsc.secondetreenum = secondetreenum;

            //scrollview 재정의
            ScrollRect scrollsc = scrollcontent.transform.parent.transform.parent.GetComponent<ScrollRect>();
            scrollsc.vertical = true;
            scrollsc.horizontal = false;

            float ygap = 10.0f;

            Vector2 csize = scrollcontent.sizeDelta;
            RectTransform slotrect = slotobj.GetComponent<RectTransform>();
            Vector2 ssize = slotrect.sizeDelta;
            Vector2 spos = Vector2.zero;
            float firstgapy = 10f;
            float firstgapx = 10f;
            if (slotsc.treeparent != null)
                firstgapx = UIConstants.TREESECONDGAP;

            float ymax = firstgapy + ((ssize.y + ygap) * (slotnum + 1));
            csize.y = ymax;
            scrollcontent.sizeDelta = csize;
            //
            slotrect = slotobj.GetComponent<RectTransform>();
            ssize = slotrect.sizeDelta;         //크기
            spos = slotrect.anchoredPosition;   //x,y

            spos.x = firstgapx + (slotrect.sizeDelta.x / 2);
            spos.y = -(firstgapy + (slotnum * (slotrect.sizeDelta.y + ygap)) + ((slotrect.sizeDelta.y / 2)));

            slotrect.anchoredPosition = spos;
        }
        protected void SetScrollViewLock()
        {
            ScrollRect scrollsc = scrollcontent.transform.parent.transform.parent.GetComponent<ScrollRect>();
            scrollsc.movementType = ScrollRect.MovementType.Clamped;
        }

        protected void CreateCustomScrollView(GameObject pa)
        {
            scpa = pa;
            starray = 1;
            scrollxgap = 0f;
            scrollygap = 0f;
            firstscrollpos = 0f;
        }
        //스크롤뷰
        protected void CreateScrollView(SCROLLTYPE scrolltype, GameObject pa, int slotnum, int slotarray = 1, float xgap = 10.0f, float ygap = 10.0f, float firstpos = 10f)
        {
            sctype = scrolltype;
            scpa = pa;
            starray = slotarray;
            scrollxgap = xgap;
            scrollygap = ygap;
            firstscrollpos = firstpos;

            ScrollRect scrollsc = scrollcontent.transform.parent.transform.parent.GetComponent<ScrollRect>();
            scrollsc.scrollSensitivity = 64;

            switch (sctype)
            {
                case SCROLLTYPE.vritical_scroll:
                    {
                        CreateScrollViewVirtical(slotnum);
                    }
                    break;
                case SCROLLTYPE.horizontal_scroll:
                    {
                        CreateScrollViewHorizontal(slotnum);
                    }
                    break;
            }
        }

        protected void AddNormalSlot(GameObject pa, Vector2 pos, int slotnum)
        {
            //생성
            GameObject slotobj = Instantiate(slotpre, scrollcontent);
            slotobj.name = slotnum.ToString();

            slotobj.GetComponent<RectTransform>().anchoredPosition = pos;

            baseui slotsc = slotobj.GetComponent<baseui>();
            slotsc.scrollparent = pa;//popuppanel.transform.GetChild(0)
            slotsc.treeparent = null;
            slotsc.slotindex = slotnum;
            scrollslots.Add(slotobj);
        }

        void OnResetScrollIndex()
        {
            //slotindex 다시 정렬
            for (int i = 0; i < scrollslots.Count; i++)
            {
                baseui slotsc = scrollslots[i].GetComponent<baseui>();
                slotsc.slotindex = i;
            }
        }
        protected void ClearScollSlot()
        {
            for (int i = 0; i < scrollslots.Count; i++)
            {
                Destroy(scrollslots[i]);
            }
            scrollslots.Clear();
        }

        protected void DelScrollSlot(int delnum)
        {
            Destroy(scrollslots[delnum]);
            scrollslots.RemoveAt(delnum);

            switch (sctype)
            {
                case SCROLLTYPE.vritical_scroll:
                    {
                        OnResetScrollViewVirtical();
                    }
                    break;
                case SCROLLTYPE.horizontal_scroll:
                    {
                        OnResetScrollViewHorizontal();
                    }
                    break;
            }
            OnResetScrollIndex();
        }
        protected void DelCustomScrollSlot(int delnum)
        {
            Destroy(scrollslots[delnum]);
            scrollslots.RemoveAt(delnum);

            OnResetScrollIndex();
        }
        protected void AddCustomScrollSlot()
        {
            int nowslotnum = scrollslots.Count;

            //생성
            GameObject slotobj = Instantiate(slotpre, scrollcontent);
            slotobj.name = slotpre.name;
            //slotobj.name = nowslotnum.ToString();
            scrollslots.Add(slotobj);
            int slotnum = scrollslots.Count;

            baseui slotsc = slotobj.GetComponent<baseui>();
            slotsc.scrollparent = scpa;
            slotsc.slotindex = nowslotnum;
        }
        protected void AddScrollSlot()
        {
            int nowslotnum = scrollslots.Count;

            //생성
            GameObject slotobj = Instantiate(slotpre, scrollcontent);
            slotobj.name = nowslotnum.ToString();
            scrollslots.Add(slotobj);
            int slotnum = scrollslots.Count;

            baseui slotsc = slotobj.GetComponent<baseui>();
            slotsc.scrollparent = scpa;
            slotsc.slotindex = nowslotnum;

            switch (sctype)
            {
                case SCROLLTYPE.vritical_scroll:
                    {
                        AddScrollViewVirtical(slotobj, nowslotnum, slotnum);
                    }
                    break;
                case SCROLLTYPE.horizontal_scroll:
                    {
                        AddScrollViewHorizontal(slotobj, nowslotnum, slotnum);
                    }
                    break;
            }
            OnResetScrollIndex();
        }

        //세로
        void OnResetScrollViewVirtical()
        {
            int slotnum = scrollslots.Count;
            Vector2 csize = scrollcontent.sizeDelta;

            if (slotnum == 0)
            {
                csize.y = 0.0f;
                scrollcontent.sizeDelta = csize;
                return;
            }

            RectTransform slotrect = scrollslots[0].GetComponent<RectTransform>();
            Vector2 ssize = slotrect.sizeDelta;

            Vector2 spos = Vector2.zero;
            //
            float firstgapy = 10.0f;
            float firstgapx = (csize.x - ((ssize.x + scrollxgap) * starray)) / 2;
            int addy = 0;
            if (((slotnum) % starray) != 0)
            {
                addy = 1;
            }
            float ymax = firstgapy + ((ssize.y + scrollygap) * ((slotnum / starray) + addy));
            csize.y = ymax;
            scrollcontent.sizeDelta = csize;

            for (int i = 0; i < slotnum; i++)
            {
                scrollslots[i].name = i.ToString();

                baseui slotsc = scrollslots[i].GetComponent<baseui>();
                slotsc.scrollparent = scpa;
                slotsc.slotindex = i;
                slotrect = scrollslots[i].GetComponent<RectTransform>();
                ssize = slotrect.sizeDelta;         //크기
                spos = slotrect.anchoredPosition;   //x,y

                spos.x = firstgapx + ((i % starray) * (slotrect.sizeDelta.x + scrollxgap)) + (slotrect.sizeDelta.x / 2);
                spos.y = -(firstgapy + ((i / starray) * (slotrect.sizeDelta.y + scrollygap)) + ((slotrect.sizeDelta.y / 2)));

                slotrect.anchoredPosition = spos;
            }
        }

        //가로
        void OnResetScrollViewHorizontal()
        {
            int slotnum = scrollslots.Count;
            Vector2 csize = scrollcontent.sizeDelta;

            if (slotnum == 0)
            {
                csize.x = 0.0f;
                scrollcontent.sizeDelta = csize;
                return;
            }

            RectTransform slotrect = scrollslots[0].GetComponent<RectTransform>();
            Vector2 ssize = slotrect.sizeDelta;

            Vector2 spos = Vector2.zero;
            //
            float firstgapx = 10.0f;
            float firstgapy = (csize.y - ((ssize.y + scrollygap) * starray)) / 2;
            int addx = 0;
            if ((slotnum % starray) != 0)
            {
                addx = 1;
            }
            float xmax = firstgapx + ((ssize.x + scrollxgap) * ((slotnum / starray) + addx));
            csize.x = xmax;
            scrollcontent.sizeDelta = csize;

            for (int i = 0; i < slotnum; i++)
            {
                scrollslots[i].name = i.ToString();

                baseui slotsc = scrollslots[i].GetComponent<baseui>();
                slotsc.scrollparent = scpa;
                slotsc.slotindex = i;
                slotrect = scrollslots[i].GetComponent<RectTransform>();
                ssize = slotrect.sizeDelta;         //크기
                spos = slotrect.anchoredPosition;   //x,y

                spos.x = firstgapx + ((i / starray) * (slotrect.sizeDelta.x + scrollxgap)) + (slotrect.sizeDelta.x / 2);
                spos.y = -(firstgapy + ((i % starray) * (slotrect.sizeDelta.y + scrollygap)) + ((slotrect.sizeDelta.y / 2)));

                slotrect.anchoredPosition = spos;
            }
        }

        //세로
        void AddScrollViewVirtical(GameObject slotobj, int nowslotnum, int slotnum)
        {
            Vector2 csize = scrollcontent.sizeDelta;
            RectTransform slotrect = slotobj.GetComponent<RectTransform>();
            Vector2 ssize = slotrect.sizeDelta;

            Vector2 spos = Vector2.zero;
            //
            float firstgapy = 10.0f;
            float firstgapx = (csize.x - ((ssize.x + scrollxgap) * starray)) / 2;
            int addy = 0;
            if (((slotnum) % starray) != 0)
            {
                addy = 1;
            }
            float ymax = firstgapy + ((ssize.y + scrollygap) * ((slotnum / starray) + addy));
            csize.y = ymax;
            scrollcontent.sizeDelta = csize;

            spos.x = firstgapx + ((nowslotnum % starray) * (slotrect.sizeDelta.x + scrollxgap)) + (slotrect.sizeDelta.x / 2);
            spos.y = -(firstgapy + ((nowslotnum / starray) * (slotrect.sizeDelta.y + scrollygap)) + ((slotrect.sizeDelta.y / 2)));

            slotrect.anchoredPosition = spos;
        }

        //가로
        void AddScrollViewHorizontal(GameObject slotobj, int nowslotnum, int slotnum)
        {
            Vector2 csize = scrollcontent.sizeDelta;
            RectTransform slotrect = slotobj.GetComponent<RectTransform>();
            Vector2 ssize = slotrect.sizeDelta;

            Vector2 spos = Vector2.zero;
            //
            float firstgapx = 10.0f;
            float firstgapy = (csize.y - ((ssize.y + scrollygap) * starray)) / 2;
            int addx = 0;
            if ((slotnum % starray) != 0)
            {
                addx = 1;
            }

            float xmax = firstgapx + ((ssize.x + scrollxgap) * ((slotnum / starray) + addx));
            csize.x = xmax;
            scrollcontent.sizeDelta = csize;

            spos.x = firstgapx + ((nowslotnum / starray) * (slotrect.sizeDelta.x + scrollxgap)) + (slotrect.sizeDelta.x / 2);
            spos.y = -(firstgapy + ((nowslotnum % starray) * (slotrect.sizeDelta.y + scrollygap)) + ((slotrect.sizeDelta.y / 2)));

            slotrect.anchoredPosition = spos;
        }

        //세로
        void CreateScrollViewVirtical(int slotnum)
        {
            ScrollRect scrollsc = scrollcontent.transform.parent.transform.parent.GetComponent<ScrollRect>();
            scrollsc.vertical = true;
            scrollsc.horizontal = false;

            GameObject slotbase = Instantiate(slotpre, scrollcontent);

            Vector2 csize = scrollcontent.sizeDelta;
            RectTransform slotrect = slotbase.GetComponent<RectTransform>();
            Vector2 ssize = slotrect.sizeDelta;
            Vector2 spos = Vector2.zero;
            float firstgapy = firstscrollpos;
            float firstgapx = (csize.x - ((ssize.x + scrollxgap) * starray)) / 2;
            int addy = 0;
            if ((slotnum % starray) != 0)
            {
                addy = 1;
            }

            float ymax = firstgapy + ((ssize.y + scrollygap) * ((slotnum / starray) + addy));
            csize.y = ymax;
            scrollcontent.sizeDelta = csize;

            for (int i = 0; i < slotnum; i++)
            {
                GameObject slotobj = Instantiate(slotpre, scrollcontent);
                slotobj.name = i.ToString();

                baseui slotsc = slotobj.GetComponent<baseui>();
                slotsc.scrollparent = scpa;
                slotsc.slotindex = i;
                slotrect = slotobj.GetComponent<RectTransform>();
                ssize = slotrect.sizeDelta;         //크기
                spos = slotrect.anchoredPosition;   //x,y

                spos.x = firstgapx + ((i % starray) * (slotrect.sizeDelta.x + scrollxgap)) + (slotrect.sizeDelta.x / 2);
                spos.y = -(firstgapy + ((i / starray) * (slotrect.sizeDelta.y + scrollygap)) + ((slotrect.sizeDelta.y / 2)));

                slotrect.anchoredPosition = spos;
                scrollslots.Add(slotobj);
            }

            Destroy(slotbase);
        }

        //가로
        void CreateScrollViewHorizontal(int slotnum)
        {
            ScrollRect scrollsc = scrollcontent.transform.parent.transform.parent.GetComponent<ScrollRect>();
            scrollsc.vertical = false;
            scrollsc.horizontal = true;

            GameObject slotbase = Instantiate(slotpre, scrollcontent);

            Vector2 csize = scrollcontent.sizeDelta;
            RectTransform slotrect = slotbase.GetComponent<RectTransform>();
            Vector2 ssize = slotrect.sizeDelta;
            Vector2 spos = Vector2.zero;
            float firstgapx = firstscrollpos;
            float firstgapy = (csize.y - ((ssize.y + scrollygap) * starray)) / 2;

            int addx = 0;
            if ((slotnum % starray) != 0)
            {
                addx = 1;
            }

            float xmax = firstgapx + ((ssize.x + scrollxgap) * ((slotnum / starray) + addx));
            csize.x = xmax;
            scrollcontent.sizeDelta = csize;

            for (int i = 0; i < slotnum; i++)
            {
                GameObject slotobj = Instantiate(slotpre, scrollcontent);
                slotobj.name = i.ToString();

                baseui slotsc = slotobj.GetComponent<baseui>();
                slotsc.scrollparent = scpa;
                slotsc.slotindex = i;
                slotrect = slotobj.GetComponent<RectTransform>();
                ssize = slotrect.sizeDelta;         //크기
                spos = slotrect.anchoredPosition;   //x,y

                spos.x = firstgapx + ((i / starray) * (slotrect.sizeDelta.x + scrollxgap)) + (slotrect.sizeDelta.x / 2);
                spos.y = -(firstgapy + ((i % starray) * (slotrect.sizeDelta.y + scrollygap)) + ((slotrect.sizeDelta.y / 2)));

                slotrect.anchoredPosition = spos;
                scrollslots.Add(slotobj);
            }

            Destroy(slotbase);
        }

        protected T ScrollParentScript<T>()
        {
            T script = scrollparent.GetComponent<T>();
            return script;
        }

        protected void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        public void CreateDontDestroy(GameObject pre)
        {
            if (!GameObject.Find(pre.name))
            {
                GameObject canvas = Instantiate(pre);
                DontDestroyOnLoad(canvas);
                canvas.name = pre.name;
            }
        }
        //폰트 재생성을 위해 생성된 ui 삭제 후 재생성
        protected void DestroyDontDestroyUI(string name)
        {
            GameObject dontui = GameObject.Find(name);
            if (null != dontui)
            {
                Destroy(dontui);
            }
        }
        protected virtual void ButtonFuntion(string btnname, string tagname) { }
        protected virtual void ToggleFuntion(string togglename, string tagname, bool ison) { }
        protected virtual void SliderClicked(GameObject slider) { }

        protected virtual void Update() { }
        public virtual async void OnReset() { }
        public virtual async void OnReset(int state) { }
        protected virtual void SetLanguage() { }
        public virtual void SlotButtonFuntion(int index, string btnname, baseui script = null) { }
        public virtual void OnResetScrollSlot() { }
        protected virtual void OnRestSortScroll() { }

        ////////////////////////////////////////////////////////////////////////////////////////////    

        /*
        protected int slotcount;
        protected int btncount = 0;
        protected List<Button> partlist1;
        protected List<Button> partlist2;
        protected List<Button> partlist3;
        protected List<Button> partlist4;
        protected List<Button> partlist5;
        protected List<Button> partlist6;
        protected List<Button> partlist7;
        protected List<Button> partlist8;
        */

        /*
            protected void SortScrollSlot()
            {
                //scrollslots.Sort((x1, x2) => x1.name.CompareTo(x2.name));
                //오름차순
                scrollslots.Sort((x1, x2) => x1.GetComponent<baseui>().sortvalue.CompareTo(x2.GetComponent<baseui>().sortvalue));

                //내림차순
                //scrollslots.Sort((x1, x2) => x2.GetComponent<baseui>().sortvalue.CompareTo(x1.GetComponent<baseui>().sortvalue));

                switch (sctype)
                {
                    case SCROLLTYPE.vritical_scroll:
                        {
                            OnResetScrollViewVirtical();
                        }
                        break;
                    case SCROLLTYPE.horizontal_scroll:
                        {
                            OnResetScrollViewHorizontal();
                        }
                        break;
                }
                OnResetScrollIndex();
            } 
            */

        //protected virtual void SetScrollParent() { }

        /*
        protected void ResetScrollViewXGap(int count, int minscrollcount, int xgapcount, float defaultypos, float yposgap)
        {
            int linecount = 0;
            if (count < minscrollcount) { linecount = 0; }
            else
            {
                //linecount = ((count - 1) / xgapcount) - 1;
                linecount = ((count - 1) / xgapcount) + 1;
                Vector2 size = scrollcontent.sizeDelta;
                //size.y = defaultypos + (yposgap * linecount);
                size.y = (yposgap * linecount);
                scrollcontent.sizeDelta = size;
            }
        }

        protected void ResetScrollView(int count, float yposgap)
        {
            Vector2 size = scrollcontent.sizeDelta;
            size.y = (yposgap * count);
            scrollcontent.sizeDelta = size;
        }

        protected async void AddTrimPartButton(Button btn, int num)
        {
            switch (num)
            {
                case 0:
                    {
                        partlist1.Add(btn);
                    }
                    break;
                case 1:
                    {
                        partlist2.Add(btn);
                    }
                    break;
                case 2:
                    {
                        partlist3.Add(btn);
                    }
                    break;
                case 3:
                    {
                        partlist4.Add(btn);
                    }
                    break;
                case 4:
                    {
                        partlist5.Add(btn);
                    }
                    break;
                case 5:
                    {
                        partlist6.Add(btn);
                    }
                    break;
                case 6:
                    {
                        partlist7.Add(btn);
                    }
                    break;
                case 7:
                    {
                        partlist8.Add(btn);
                    }
                    break;
            }
        }

        protected async void CreateTrimPartButton(int num, string parttag)
        {
            switch (num)
            {
                case 0:
                    {
                        partlist1 = new List<Button>();
                        foreach (Button btn in btns)
                        {
                            if (btn == null) continue;
                            if (btn.tag == parttag)
                            {
                                partlist1.Add(btn);
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        partlist2 = new List<Button>();
                        foreach (Button btn in btns)
                        {
                            if (btn == null) continue;
                            if (btn.tag == parttag)
                            {
                                partlist2.Add(btn);
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        partlist3 = new List<Button>();
                        foreach (Button btn in btns)
                        {
                            if (btn == null) continue;
                            if (btn.tag == parttag)
                            {
                                partlist3.Add(btn);
                            }
                        }
                    }
                    break;
                case 3:
                    {
                        partlist4 = new List<Button>();
                        foreach (Button btn in btns)
                        {
                            if (btn == null) continue;
                            if (btn.tag == parttag)
                            {
                                partlist4.Add(btn);
                            }
                        }
                    }
                    break;
                case 4:
                    {
                        partlist5 = new List<Button>();
                        foreach (Button btn in btns)
                        {
                            if (btn == null) continue;
                            if (btn.tag == parttag)
                            {
                                partlist5.Add(btn);
                            }
                        }
                    }
                    break;
                case 5:
                    {
                        partlist6 = new List<Button>();
                        foreach (Button btn in btns)
                        {
                            if (btn == null) continue;
                            if (btn.tag == parttag)
                            {
                                partlist6.Add(btn);
                            }
                        }
                    }
                    break;
                case 6:
                    {
                        partlist7 = new List<Button>();
                        foreach (Button btn in btns)
                        {
                            if (btn == null) continue;
                            if (btn.tag == parttag)
                            {
                                partlist7.Add(btn);
                            }
                        }
                    }
                    break;
                case 7:
                    {
                        partlist8 = new List<Button>();
                        foreach (Button btn in btns)
                        {
                            if (btn == null) continue;
                            if (btn.tag == parttag)
                            {
                                partlist8.Add(btn);
                            }
                        }
                    }
                    break;
            }
        }

        //버튼 직접 만들어서 붙이는건 Start가 아니라 버튼 1개당 이거로 사용
        protected void CustomStartBtnPart(Button btn)
        {
            btn.onClick.AddListener(() => ButtonClicked(btn.gameObject));
        }

        protected void CustomStartTogglePart(Toggle toggle)
        {
            toggle.onValueChanged.AddListener((value) => { ToggleClicked(toggle.gameObject); });
        }

        protected void CustomStartSliderPart(Slider sl)
        {
            sl.onValueChanged.AddListener((value) => { SliderClicked(sl.gameObject); });
        }

        protected void CustomStartBtnAll()
        {
            if (null != btns)
            {
                foreach (Button btn in btns)
                {                
                    btn.onClick.AddListener(() => ButtonClicked(btn.gameObject));
                }
            }
        }

        protected void CustomStartToggleAll()
        {
            if (null != toggles)
            {
                foreach (Toggle toggle in toggles)
                {
                    toggle.onValueChanged.AddListener((value) => { ToggleClicked(toggle.gameObject); });
                }
            }
        }

        protected void CreateButtonInit(int btntotalcount)
        {
            if(btns.Length > 0)
            {
                //이미 등록된 버튼이랑 만든 버튼합칠때
                for (int i = 0; i < btns.Length; i++)
                {
                    CustomStartBtnPart(btns[i]);
                }

                int savebtncount = btns.Length;
                btncount = savebtncount;
                Button[] savebtn = new Button[savebtncount];
                for (int i = 0; i < savebtncount; i++)
                {
                    savebtn[i] = btns[i];
                }
                btns = new Button[btntotalcount + savebtncount];
                for (int i = 0; i < savebtncount; i++)
                {
                    btns[i] = savebtn[i];
                }
            }
            else
            {
                btns = new Button[btntotalcount];
            }
        }
        */
    }
}
