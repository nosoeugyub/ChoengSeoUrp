using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace NSY.Iven
{
    public class CraftList : ItemContainer
    {
        [Header("위에 아이템 슬롯 신경 ㄴㄴ 레시피 리스트 목록")]
        public CraftWindow[] craftwind;
       public Item[] Craftingitems;
        [SerializeField] Transform itemsParent; //this

       

        public event Action<CraftSlot> OnLeftClickEventss ;

        protected override void OnValidate()
        {
           
            if (itemsParent != null)
            {
                itemsParent.GetComponentsInChildren(includeInactive: true, result: Craftslot);
            }

            if (!Application.isPlaying)
            {
              //  SetStartingTiems();
            }
        }

        private void Start()
        {
            for (int i = 0; i < Craftslot.Count; i++)
            {
                Craftslot[i].OnLeftClickEventss += OnLeftClickEventss;
            }
////
          //  SetStartingTiems();
        }
       
        private void SetStartingTiems()//아이템 슬롯과 리스트가 일치하게 돌려주는 함수 
        {
            Clear();
            for (int i = 0; i < Craftslot.Count; i++)
            {
                Craftslot[i].RecipeItem = Craftingitems[i];
            }
        }

      


    
    }
}

