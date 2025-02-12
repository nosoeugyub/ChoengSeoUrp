﻿using DG.Tweening;
using NSY.Iven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewInventUIManager : MonoBehaviour
{
    public GameObject NoPopUp;
    public Button button;

    [Header("오픈될 오브젝트")] [SerializeField] RectTransform BG, invenBtn;
    [SerializeField] GameObject[] TabUI;
    [SerializeField] ScrollRect TopRect;

    [SerializeField] CraftWindow[] CraftWindows;
    [SerializeField] InventoryNSY iven;
    [SerializeField] CraftSlot nowSelectItem;

    [SerializeField] Scrollbar scrollbar;

    public int nowActiveTabIdx;
    bool isCreateMode = false;

    [Header("열고 닫고 체인지")]
    public bool isOpen;
    public bool isChange;
    public int TabuiNumber;
    //영역생성
    public Image image;
    // Start is called before the first frame update
    void Awake()
    {

        for (int i = 0; i < TabUI.Length; i++)
        {
            {
                Transform tabuichild = TabUI[i].transform.GetChild(1);
                tabuichild.GetComponent<CraftList>().OnLeftClickEventss += ShowRecipe;
            }
        }
        iven.OnAddItemEvent += ShowRecipe;
    }

    private void Start()
    {
        nowActiveTabIdx = -1;
        //조건 검사 as

        UnLockManager.Unlockmanager.GetItemUnlocks += InterectingItem;
    }
    private void OnDisable()
    {
        for (int i = 0; i < TabUI.Length; i++)
        {
            for (int j = 1; j < TabUI[i].transform.childCount; j++)
            {
                TabUI[i].transform.GetChild(j).GetComponent<CraftList>().OnLeftClickEventss -= ShowRecipe;
            }
        }
        UnLockManager.Unlockmanager.GetItemUnlocks -= InterectingItem;
    }
    void InterectingItem()//아이템 n개 획득 시 해금  검사
    {
        for (int i = 0; i < iven.ItemSlots.Count; i++)
        {
            for (int r = 0; r < TabUI.Length; r++) //4 
            {
                CraftList tempCL = TabUI[r].transform.GetChild(1).GetComponent<CraftList>();
                for (int k = 0; k < tempCL.Craftslot.Count; k++)
                {
                    if (tempCL.Craftslot[k].RecipeItem != null)
                    {
                        for (int n = 0; n < tempCL.Craftslot[k].RecipeItem.UnlockItem.Length; n++) // 이벤토리 슬롯과 탭창의 아이템의 해금재료아이템을 검사
                        {
                            if (iven.ItemSlots[i].item != null)
                            {
                                if (iven.ItemSlots[i].item.ItemName == tempCL.Craftslot[k].RecipeItem.UnlockItem[n].item.ItemName && //슬롯과 해료 이름이같으면
                                   iven.ItemSlots[i].item.GetnuCountItems >= tempCL.Craftslot[k].RecipeItem.UnlockItem[n].count) // 갯수도 같으면
                                {
                                    if (tempCL.Craftslot[k].isHaveRecipeItem == false)
                                    {

                                        tempCL.Craftslot[k].isHaveRecipeItem = true;

                                        DebugText.Instance.SetText(string.Format("{0} 자재를 제작할 수 있게 되었습니다.", tempCL.Craftslot[k].RecipeItem.ItemName));
                                    }

                                }

                            }
                        }
                    }
                }
            }
        }


    }

    public void ShowRecipe()
    {
        for (int i = 0; i < CraftWindows.Length; i++)
        {
            CraftWindows[i].UpdateWindowState();
        }
    }
    public void ShowRecipe(CraftSlot obj)
    {
        nowSelectItem = obj;
        obj.RecipeName.text = obj.RecipeItem.ItemName;

        for (int i = 0; i < CraftWindows.Length; i++)
        {
            if (obj.RecipeItem.recipe[i].item)
            {
                CraftWindows[i].RecipeAmount = obj.RecipeItem.recipe[i].count;
            }
            CraftWindows[i].Item = obj.RecipeItem.recipe[i].item;
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            CreateMode();
    }
    private bool HasItemSlot()
    {
        for (int i = 0; i < iven.ItemSlots.Count; i++)
        {
            if (iven.ItemSlots[i].CanAddStack(nowSelectItem.RecipeItem, 1))
            {
                return false;
            }
        }
        StartCoroutine(StartPopup());//더할 스택이 있을때 true 반환
        return true;
    }
    public bool HaventItems()
    {
        return iven.Fulled() && !HasItemSlot();
    }
    IEnumerator StartPopup()
    {
        NoPopUp.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        NoPopUp.SetActive(false);
    }
    public void CreateMode()
    {
        isCreateMode = !isCreateMode;
        DebugText.Instance.SetText(string.Format("Creative 모드 {0}입니다.", isCreateMode.ToString()));
    }
    public void BtnSolution()
    {
        if (HaventItems())
        {

        }
        if (isCreateMode == true)
        {
            PlayerData.AddValue((int)nowSelectItem.RecipeItem.InItemType, (int)ItemBehaviorEnum.Craft, PlayerData.ItemData, (int)ItemBehaviorEnum.length);
            iven.AddItem(nowSelectItem.RecipeItem, true);
        }

        List<List<ItemSlot>> itemSlots = CanCraftItem();
        if (itemSlots == null || itemSlots[0].Count == 0)
        {
            return;
        }
        //if (iven.IsFullInven())
        //{
        //    iven.NoPopupOn();
        //    return;
        //}
        for (int i = 0; i < CraftWindows.Length; i++)
        {
            if (itemSlots[i].Count == 0) continue;

            //슬롯 중 숫자가 제일 작은 슬롯 구해올 변수
            int ra = CraftWindows[i].RecipeAmount;

            ItemSlot minSlot = itemSlots[i][itemSlots[i].Count - 1];

            do
            {
                //print(itemSlots[i].Count - 1);
                minSlot = itemSlots[i][itemSlots[i].Count - 1];

                foreach (var item in itemSlots[i]) //최소 슬롯 찾음
                {
                    if (item.Amount < minSlot.Amount)
                    {
                        minSlot = item;
                    }
                }

                if (minSlot.Amount < ra) //전체 6  민 칸 1개, 필요 2개일 경우 
                {
                    minSlot.item.GetCountItems -= minSlot.Amount;//6- 1
                    ra -= minSlot.Amount; //2 -= 1
                    minSlot.Amount = 0;
                    itemSlots[i].Remove(minSlot);
                }
                else
                    break;
            }
            while (ra > 0);// 그런데도

            minSlot.item.GetCountItems -= ra;
            CraftWindows[i].SetRecipeHaverAmountText(minSlot.item.GetCountItems.ToString());

            minSlot.Amount -= ra;
        }

        if (!iven.AddItem(nowSelectItem.RecipeItem, true))
        {
            int small= 100;
            int smallidx= 100;
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (itemSlots[i].Count == 0)
                    continue;

                for (int j = 0; j < itemSlots[i].Count; j++)
                {
                    if (small > itemSlots[i][j].Amount)
                    {
                        small = itemSlots[i][j].Amount;
                        smallidx = j;
                    }
                }

                itemSlots[i][smallidx].Amount += CraftWindows[i].RecipeAmount;
                itemSlots[i][smallidx].item.GetCountItems += CraftWindows[i].RecipeAmount;
                continue;
            }
            //foreach (var item in itemSlots)
            //{
            //    if (item.Count == 0) continue;

            //    item[item.Count - 1].Amount++;
            //    item[item.Count - 1].item.GetCountItems += CraftWindows[i].RecipeAmount;
            //    continue;
        }
        else
        {
            PlayerData.AddValue((int)nowSelectItem.RecipeItem.InItemType, (int)ItemBehaviorEnum.Craft, PlayerData.ItemData, (int)ItemBehaviorEnum.length);

        }
    }

    private List<List<ItemSlot>> CanCraftItem()
    {
        List<List<ItemSlot>> itemSlots = new List<List<ItemSlot>>();

        foreach (CraftWindow itemwid in CraftWindows)
        {
            List<ItemSlot> itemSlot1 = new List<ItemSlot>();
            itemSlots.Add(itemSlot1);
            if (itemwid.Item == null) continue;// 아이템이 널이면 다음 슬롯 검사로.

            for (int i = 0; i < iven.ItemSlots.Count; i++)//전체 인벤 돌기
            {
                //특정 템 있는 슬롯 얻어옴
                if (itemwid.Item == iven.ItemSlots[i].item) // 필요 재료와 아이템 타입이 같다.
                {
                    if (itemwid.RecipeAmount > iven.ItemSlots[i].item.GetCountItems) return null; //필요 개수가 모자라면 널 리턴시킴.
                    itemSlots[itemSlots.Count - 1].Add(iven.ItemSlots[i]);
                    //break;//개수 충족했다면 일단 다음.
                }
            }
            if (itemSlots[itemSlots.Count - 1].Count == 0) return null;
        }
        return itemSlots;
    }

    public void BtnTabSelect(int TabNum)
    {
        foreach (var item in TabUI)
        {
            item.SetActive(false);
        }
        TabUI[TabNum].SetActive(true);


        if (nowActiveTabIdx == TabNum)//같은탭을 눌렀다면
        {
            //nowActiveTabIdx = -1;
            if (isOpen == false) Open(); //닫혀있을 때는 열리기
            else close();
        }
        else //다르면
        {
            nowActiveTabIdx = TabNum;
            if (isOpen == false) Open(); //닫혀있을 때는 열리기
        }
        MoveTabs(TabNum);
        TopRect.content = TabUI[TabNum].GetComponent<RectTransform>();
    }

    public void Open()
    {
        if (isOpen == false)
        {
            BG.DOLocalMoveX(819, 1).SetEase(Ease.OutQuart);
            invenBtn.DOLocalMoveX(666, 1).SetEase(Ease.OutQuart);//630 815 1 85
            isOpen = true;
        }
    }

    public void MoveTabs(int index)
    {
        for (int i = 0; i < invenBtn.childCount; i++)
        {
            if (index == i)
                invenBtn.GetChild(i).DOLocalMoveX(-15, 1).SetEase(Ease.OutQuart);
            else
                invenBtn.GetChild(i).DOLocalMoveX(0, 1).SetEase(Ease.OutQuart);
        }
    }
    public void close()
    {
        BG.DOLocalMoveX(1105, 1).SetEase(Ease.OutQuart);
        invenBtn.DOLocalMoveX(952, 1).SetEase(Ease.OutQuart); //187
        isOpen = false;
    }
}
