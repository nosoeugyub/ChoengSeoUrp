using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMBasic;
public class scrollviewsample : baseui
{
    public int slotnum = 10;
    public int slotarraynum = 2;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        StartCoroutine(CreateScrollE());
    }

    //sample
    IEnumerator CreateScrollE()
    {
        yield return new WaitForSeconds(0.5f);      //data load 시간 임의

        //1. 디폴트 기반으로 내 유닛 생성 추가 삭제 임의를 위해
        int randdelnum = 0;
        
        for (int i=0; i< slotnum; i++)
        {
            randdelnum = Random.RandomRange(0, UserData.Instance.defaultunit.Count);
            UserData.Instance.myunits.Add(UserData.Instance.defaultunit[randdelnum]);
        }

        //UserData.Instance.myunits = GlobalData.Instance.CopyList(UserData.Instance.defaultunit);

        //2. 스크롤뷰 생성
        //CreateScrollView(SCROLLTYPE.vritical_scroll, gameObject, UserData.Instance.myunits.Count, slotarraynum, 20f, 20f, 100f);
        CreateScrollView(SCROLLTYPE.vritical_scroll, gameObject, UserData.Instance.myunits.Count, slotarraynum, firstpos: 100f);

        //CreateScrollView(SCROLLTYPE.horizontal_scroll, gameObject, UserData.Instance.myunits.Count, slotarraynum);

        //3. 스크롤뷰의 슬롯에 데이터는 그때그때 달라서 따로 override 해서 만듬
        OnResetScrollSlot();
    }

    public override void OnResetScrollSlot()
    {
        for (int i = 0; i < scrollslots.Count; i++)
        {
            //scroll마다 바꿔줘야 함
            scrollslot slotsc = scrollslots[i].GetComponent<scrollslot>();
            slotsc.OnResetScrollSlot();
        }
     }

    //slot에서 처리 가능하나 전달해서 parent에서 기능 처리도 가능
    public override void SlotButtonFuntion(int index, string btnname, baseui script = null)
    {
        print(index.ToString() + " : " + btnname);
        switch (btnname)
        {
            case "btn1":
                {
                    //script.gameObject.GetComponent<scrollslot>().SetChangeString("BUTTON1");
                    //script.texts[0].text = "BUTTON1";
                }
                break;
            case "btn2":
                {
                    //script.gameObject.GetComponent<scrollslot>().SetChangeString("BUTTON2");
                }
                break;
        }
    }

    protected override void OnRestSortScroll()
    {
        ClearScollSlot();
        for (int i = 0; i < UserData.Instance.myunits.Count; i++)
        {
            AddScrollSlot();
        }
        OnResetScrollSlot();
    }

    protected override void ButtonFuntion(string btnname, string tagname)
    {
        switch (btnname)
        {
            case "sorbtn":
                {
                    //원본 데이터 정렬 후 슬롯 다시 정렬
                    UserData.Instance.myunits.Sort((x1, x2) => x1.index.CompareTo(x2.index));
                    //UserData.Instance.myunits.Sort((x1, x2) => x1.GetComponent<unitdata>().index.CompareTo(x2.GetComponent<unitdata>().index));

                    OnRestSortScroll();
                }
                break;
            case "clearbtn":
                {
                    //슬롯만 전부 삭제. 데이터 삭제할려면 따로
                    //UserData.Instance.myunits.Clear();

                    ClearScollSlot();
                }
                break;
            case "alladdbtn":
                {
                    OnRestSortScroll();
                }
                break;
            case "delbtn":
                {
                    if (UserData.Instance.myunits.Count == 0) return;

                    int randdelnum = Random.RandomRange(0, UserData.Instance.myunits.Count);
                    UserData.Instance.myunits.RemoveAt(randdelnum);                    

                    DelScrollSlot(randdelnum);
                    OnResetScrollSlot();
                }
                break;
            case "addbtn":
                {
                    int randdelnum = Random.RandomRange(0, UserData.Instance.defaultunit.Count);
                    UserData.Instance.myunits.Add(UserData.Instance.defaultunit[randdelnum]);

                    AddScrollSlot();
                    OnResetScrollSlot();
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
