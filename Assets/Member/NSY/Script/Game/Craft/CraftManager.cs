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
        [SerializeField] public List<CraftSlot>  CratfingSlots;
        
        [SerializeField]public CraftSlot ResultSlot;
        public Item[] CraftRecipe;
        Item craftingRecipes;

        [SerializeField] InventoryNSY ivenTory;

        [Header("Public variables")]
        public IItemContainer itemContainer; //아이템을 제작하기위한 참조

        [SerializeField]
        Item NullRecpie;

        private Item craftingRecipe;
      



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


      

       
        //추가 , 탐색, 비쥬얼
        public  bool CraftAddItem( Item item  )
        {
            for (int i = 0; i < CratfingSlots.Count; i++)
            {
                if (CratfingSlots[i].CanAddStack(item))
                {
                 
                    CratfingSlots[i].item = item;
                    CratfingSlots[i].Amount++;
                  //  CratfingSlots[i]._item.recipe[i].item = item;
                   

                  
                  

                    return true;
                }


            }
            for (int i = 0; i < CratfingSlots.Count; i++)
            {
                if (CratfingSlots[i].item == null)
                {
                //    item = CratfingSlots[i]._item.recipe[i].item;
                    CratfingSlots[i].item = item;
                   
                    CratfingSlots[i].Amount++;
                    Debug.Log(" 더해줌띠");

                    return true;
                }


            }

            
            return false;
        }
        public bool CraftMiuseItem(Item item)
        {
            for (int i = 0; i < CratfingSlots.Count; i++)
            {
                if (CratfingSlots[i].CanAddStack(item))
                {
                    CratfingSlots[i].item = item;
                    CratfingSlots[i].Amount--;
                  

                    return true;
                }


            }
            for (int i = 0; i < CratfingSlots.Count; i++)
            {
                if (CratfingSlots[i].item == null)
                {
                    CratfingSlots[i].item = item;
                    CratfingSlots[i]._item.recipe[i].item = item;
                    CratfingSlots[i].Amount--;
                    

                    return true;
                }


            }


            return false;
        }
        //제거
        public bool RemoveItem(Item item)
        {
            for (int i = 0; i < CratfingSlots.Count; i++)
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

            ivenTory.AddItem(ResultSlot.item.GetCopy());

            for (int i = 0; i < CraftRecipe.Length; i++)
            {
                for (int j = 0; j < CraftRecipe[i].recipe.Length; j++)
                {
                    for (int k = 0; k < CratfingSlots.Count; k++)
                    {
                        CratfingSlots[k].Amount -= CraftRecipe[i].recipe[j].Count;
                    }


                }
            }
           
            ResultSlot.item = null;
        }

        
        

       public Item SetCraftingRecipe()
        {
         
            bool isRecipe0; //1번째 레시피 ...
            bool isRecipe1;
            bool isRecipe2;
            
            
                for (int i = 0; i < CraftRecipe.Length; i++)//레시피 검사
                {
                       Debug.Log("레시피 중");

                        isRecipe0 = false;
                        isRecipe1 = false;
                        isRecipe2 = false;
                    for (int j = 0; j < CraftRecipe[i].recipe.Length;  j++)//레피안의 재료검사
                    {
                        if (CraftRecipe[i].recipe.Length == 2)
                        {
                            isRecipe2 = true;
                        }
                        else if(CraftRecipe[i].recipe.Length == 1)
                        {
                               isRecipe1 = true;
                                isRecipe2 = true;
                        }

                        for (int k = 0; k < CratfingSlots.Count; k++)//새로만들 조합대
                        {
                           if (CraftRecipe[i].recipe[j].item == CratfingSlots[k].item && CraftRecipe[i].recipe[j].Count == CratfingSlots[k].Amount)
                           {
                               if (j == 0)
                               {
                                isRecipe0 = true;
                               }
                               else if (j == 1)
                               {
                                isRecipe1 = true;
                               }
                               else if (j == 2)
                               {
                                isRecipe2 = true;
                               }

                               if (isRecipe1 && isRecipe2 && isRecipe0)
                               {
                                //Debug.Log("레시피 맞음");
                                ResultSlot.UpdateResult(craftingRecipes);
                                ResultSlot.item = CraftRecipe[i];

                                Color color = ResultSlot.GetComponent<Image>().color;
                                color.a = 1.0f;
                                ResultSlot.GetComponent<Image>().color = color;

                                PlayerData.AddValue((int)ResultSlot.item.InItemType, (int)CraftBehaviorEnum.Craft, PlayerData.craftData, (int)CraftBehaviorEnum.length);
                                Debug.Log(string.Format("{0} 타입의 제작물 생성", ResultSlot.item.InItemType));

                                return CraftRecipe[i];
                               }
                                else
                               {
                                ResultSlot.item = null;
                                }

                        }
                          



                        }
                        
                    
                         
                    }
               
                    
                }
            return NullRecpie;// 맞지않다 


       }
        
  

    }

}

