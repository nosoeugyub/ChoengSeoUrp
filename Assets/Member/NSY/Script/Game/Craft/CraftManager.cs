using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;
using System;

namespace NSY.Iven
{
    public class CraftManager : MonoBehaviour
    {
     
        Item CurrentItem;
        //레시피
       
        
       
       
        [SerializeField] RectTransform arrowParent;
        public int slotIndex = 0;

        [Header("레시피 컴포넌트")]
      
        [SerializeField] Transform CraftingSlotsParent;
        [SerializeField]public CraftSlot[] CratfingSlots;
        
        [SerializeField] CraftSlot ResultSlot;
        public List<CraftingRecipe> CraftRecipe;
        CraftingRecipe craftingRecipes;

        [SerializeField] InventoryNSY ivenTory;

        [Header("Public variables")]
        public IItemContainer itemContainer; //아이템을 제작하기위한 참조



        private CraftingRecipe craftingRecipe;
      



        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnLeftClickEvent;
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;

        private void OnValidate()
        {
            //CratfingSlots = CraftingSlotsParent.GetComponentsInChildren<CraftSlot>();
        }

        private void Start()
        {
            //for (int i = 0; i < CratfingSlots.Length; i++)
          //  {
                //CratfingSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
                //CratfingSlots[i].OnPointerExitEvent += OnPointerExitEvent;
                //CratfingSlots[i].OnRightClickEvent += OnLeftClickEvent;
                //CratfingSlots[i].OnBeginDragEvent += OnBeginDragEvent;
                //CratfingSlots[i].OnEndDragEvent += OnEndDragEvent;
                //CratfingSlots[i].OnDragEvent += OnDragEvent;
                //CratfingSlots[i].OnDropEvent += OnDropEvent;
          //  }


            foreach (CraftSlot craftSlot in CratfingSlots)
            {
                craftSlot.OnPointerEnterEvent += OnPointerEnterEvent;
                craftSlot.OnPointerExitEvent += OnPointerExitEvent;
                craftSlot.OnRightClickEvent += OnLeftClickEvent;
                craftSlot.OnBeginDragEvent += OnBeginDragEvent;
                craftSlot.OnEndDragEvent += OnEndDragEvent;
                craftSlot.OnDragEvent += OnDragEvent;
                craftSlot.OnDropEvent += OnDropEvent;
            }
        }


      

       

        public  bool CraftAddItem( Item item)
        {
            for (int i = 0; i < CratfingSlots.Length; i++)
            {
                if (CratfingSlots[i].CanAddStack(item))
                {
                    CratfingSlots[i].item = item;
                    CratfingSlots[i].Amount++;
                    return true;
                }


            }
            for (int i = 0; i < CratfingSlots.Length; i++)
            {
                if (CratfingSlots[i].item == null)
                {
                    CratfingSlots[i].item = item;
                    CratfingSlots[i].Amount++;
                    return true;
                }


            }
       
            return false;
        }
        //제거
        public bool RemoveItem(Item item)
        {
            for (int i = 0; i < CratfingSlots.Length; i++)
            {
                if (CratfingSlots[i].item == item)
                {
                    CratfingSlots[i].item = null;
                    CratfingSlots[i].Amount = 0;
                    return true;
                }


            }
            return false;
        }



      
       public void OnResultBtn()
        {
            Debug.Log("버튼클릭함");

            Result();

        }

        
        

       public void SetCraftingRecipe()
        {
         
            bool isRecipe0; //1번째 레시피 ...
            bool isRecipe1;
            bool isRecipe2;
            int SlotCheckNumber;
            
                for (int i = 0; i < CraftRecipe.Count; i++)//레시피 검사
                {

                        isRecipe0 = false;
                        isRecipe1 = false;
                        isRecipe2 = false;
                    for (int j = 0; j < CraftRecipe[i].Materials.Count;  j++)//레피안의 재료검사
                    {
                        if (CraftRecipe[i].Materials.Count == 2)
                        {
                            isRecipe2 = true;
                        }
                        else if(CraftRecipe[i].Materials.Count == 1)
                        {
                               isRecipe1 = true;
                                isRecipe2 = true;
                        }
                        for (int k = 0; k < CratfingSlots.Length; k++)//새로만들 조합대
                        {
                            if (CraftRecipe[i].Materials[j].Item == CratfingSlots[k].item && CraftRecipe[i].Materials[j].Amount == CratfingSlots[k].Amount)
                            {
                                if (j== 0 )
                                {
                                    isRecipe0 = true;
                                }
                                else if(j == 1)
                                {
                                    isRecipe1 = true;
                                }
                                else if(j == 2)
                                {
                                    isRecipe2 = true;
                                }
                            }

                        }
                        if (isRecipe1 && isRecipe2 && isRecipe0)
                        {
                        Debug.Log("레시피 맞음");

                        ResultSlot.item = CraftRecipe[i].Results[i].Item;
                        ResultSlot.Amount = CraftRecipe[i].Results[i].Amount;
                        Color color = ResultSlot.GetComponent<Image>().color;
                        color.a = 1.0f;
                        ResultSlot.GetComponent<Image>().color = color;

                      
                       


                       
                        }
                    
                         
                    }
               
            }
            


        }
        
        void Result()
        {
            ivenTory.AddItem(ResultSlot.item.GetCopy());
        }

    }

}

