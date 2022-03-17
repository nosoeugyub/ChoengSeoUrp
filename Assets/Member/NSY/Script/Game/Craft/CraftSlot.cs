using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NSY.Iven
{
    public class CraftSlot : ItemSlot
    {
        [SerializeField]
        CraftManager craftmanager;
        Item Recipe;
        Image ResultSprite;

       public int index;
       

        public InItemType itemtype;

        protected override void OnValidate()
        {
            base.OnValidate();
            ResultSprite = gameObject.GetComponent<Image>();
            gameObject.name = itemtype.ToString() + " Slot";
           
        }
        public override bool CanReceiveItem(Item item)
        {
            if (item == null)
            {
                return true;
            }
            Item craftitem = item ;
            return craftitem != null && craftitem.InItemType == itemtype;
        }


       public void UpdateResult(Item MadeRecipe)
        {
            Recipe = MadeRecipe;

            if (Recipe = null)
            {
                ResultSprite.enabled = false;

            }
            else if(Recipe != null)
            {
                ResultSprite.enabled = true ;
               
            }
        
        }

    }

}
