using UnityEngine.UI;
using System.Text;
using UnityEngine;
using DM.Inven;

namespace NSY.Iven
{
    public class ItemTooltip : MonoBehaviour
    {
        [SerializeField] Text ItemNameTex;
        [SerializeField] Text ItemSlotTex; //아이템 타입
        [SerializeField] Text ItemStatsTex;


       

      
        public void ShowEqulTooltip(EquippableItem eqitem)
        {
            ItemNameTex.text = eqitem.ItemName;
            ItemSlotTex.text = eqitem.equipmentType.ToString();
            ItemStatsTex.text = eqitem.ItemDescription;

        }
        public void ShowItemTooltip(Item item)
        {
            Debug.Log("이거 됨");
            ItemNameTex.text = item.ItemName;
            ItemSlotTex.text = item.InItemType.ToString();
            ItemStatsTex.text = item.ItemDescription;

        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);

        }


    }


}
