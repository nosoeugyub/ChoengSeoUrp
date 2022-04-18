using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;
using System;

namespace NSY.Iven
{
    public class CraftManager : MonoBehaviour ,ICraft
    {
     
        Item CurrentItem;
        //레시피
       
        
       
       
       

        [Header("레시피 컴포넌트")]
        [SerializeField] RecipeListPanel RecipelistPanel;


        [SerializeField] Transform CraftingSlotsParent;
        [SerializeField] public List<CraftSlot>  CratfingSlots;
        
        [SerializeField]public CraftSlot ResultSlot;
      
        Item craftingRecipes;

        [SerializeField] InventoryNSY ivenTory;

        [Header("Public variables")]
        public IItemContainer itemContainer; //아이템을 제작하기위한 참조

        [SerializeField]
        Item NullRecpie;

        private Item craftingRecipe;

        //아이템슬롯
        [SerializeField] InventoryNSY inven;


        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnLeftClickEvent;
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;

        private void OnValidate()
        {
         //   CratfingSlots = CraftingSlotsParent.GetComponentsInChildren<CraftSlot>();
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

            for (int i = 0; i < RecipelistPanel.RecipeList.Count; i++)
            {
                for (int j = 0; j < RecipelistPanel.RecipeList[i].recipe.Length; j++)
                {
                    for (int k = 0; k < CratfingSlots.Count; k++)
                    {
                        CratfingSlots[k].Amount -= RecipelistPanel.RecipeList[i].recipe[j].count;
                    }


                }
            }
           
            ResultSlot.item = null;
        }

        
        

       public Item SetCraftingRecipe()
        {
         
            bool isRecipe0; //
            bool isRecipe1;
            bool isRecipe2;
          
            
                for (int i = 0; i < RecipelistPanel.RecipeList.Count; i++)//레시피 검사
                {
                       Debug.Log("레시피 중");

                        isRecipe0 = false;
                        isRecipe1 = false;
                        isRecipe2 = false;
                    for (int j = 0; j < RecipelistPanel.RecipeList[i].recipe.Length;  j++)//레피안의 재료검사
                    { 
                        if (RecipelistPanel.RecipeList[i].recipe.Length == 2)
                        {
                            isRecipe2 = true;
                        }
                        else if(RecipelistPanel.RecipeList[i].recipe.Length == 1)
                        {
                               isRecipe1 = true;
                                isRecipe2 = true;
                        }
                        else if (RecipelistPanel.RecipeList[i].recipe.Length == 0)
                        {
                             isRecipe0 = true;
                        }
                           for (int k = 0; k < CratfingSlots.Count; k++)//현재 조합식이랑 레시피 조합식 비교
                           {
                        
                              if (RecipelistPanel.RecipeList[i].recipe[j].item == CratfingSlots[k].item && RecipelistPanel.RecipeList[i].recipe[j].count == CratfingSlots[k].Amount)
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
                                         Debug.Log("레시피 맞음");
                                         ResultSlot.UpdateResult(craftingRecipes);
                                         ResultSlot.item = RecipelistPanel.RecipeList[i];
                                
                                          Color color = ResultSlot.GetComponent<Image>().color;
                                           color.a = 1.0f;
                                          ResultSlot.GetComponent<Image>().color = color;

                                          return RecipelistPanel.RecipeList[i];
                                     }
                           

                              }
                        if (RecipelistPanel.RecipeList[i].recipe[j].item != CratfingSlots[k].item || RecipelistPanel.RecipeList[i].recipe[j].count != CratfingSlots[k].Amount)
                        {
                            
                           
                            ResultSlot.item = null;
                            Debug.Log("레시피가 맞는게 업성요");
                        }



                           }
                        
                    
                         
                    }
               
                    
                }
            return NullRecpie;// 맞지않다 


       }
        
        public  bool RestSlot( )
        {

            for (int i = 0; i < CratfingSlots.Count; i++)
            {
               
                if (CratfingSlots[i].item != null)
                {
                    for (int j = 0; j < inven.ItemSlots.Count; j++)
                    {
                        if (CratfingSlots[i].item != inven.ItemSlots[j].item && inven.ItemSlots[j] != null)
                        {
                           

                             inven.entireItem(CratfingSlots[i].item.GetCopy());
                          

                            CratfingSlots[i].item = null;
                      

                          
                            return true;
                        }
                      
                    }

                    for (int j = 0; j < inven.ItemSlots.Count; j++)
                    {
                        if (CratfingSlots[i].item == inven.ItemSlots[j].item)
                        {
                            
                            inven.ItemSlots[j].Amount += CratfingSlots[i].Amount;

                            CratfingSlots[i].item = null;
                            CratfingSlots[i].Amount = 0;
                        }
                    }
                   

                }


            }
            return false;

        }
      


        public bool DonthaveCraft()
        {
            for (int i = 0; i < CratfingSlots.Count; i++)
            {
                if (CratfingSlots[2].item != null)
                {
                          return false;
                 }
               
            }
           
            return true;
        }
             
               
           
           
        
    }

}

