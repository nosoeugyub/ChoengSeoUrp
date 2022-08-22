using UnityEngine.UI;
using System.Text;
using UnityEngine;
using DM.Inven;

namespace NSY.Iven
{
    public class ItemTooltip : MonoBehaviour
    {
        //이벤토리
        [SerializeField] Text ItemNameTex;
        [SerializeField] Text ItemStatsTex;
        [SerializeField] Image backgroundImg;
        public Transform tooltipTransform;
       


        public void ShowEqulTooltip(EquippableItem eqitem)
        {
            ItemNameTex.text = eqitem.ItemName;
            //ItemStatsTex.text = eqitem.ItemDescription;
            gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundImg.rectTransform);
        }
        public void ShowItemTooltip(Item item)
        {
          
            ItemNameTex.text = item.ItemName;
            //ItemStatsTex.text = item.ItemDescription;
            gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundImg.rectTransform);
        }
     
        
        public void HideTooltip()
        {
            gameObject.SetActive(false);

        }


    }


}
