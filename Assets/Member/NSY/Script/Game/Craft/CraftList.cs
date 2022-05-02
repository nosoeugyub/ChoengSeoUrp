using System;
using UnityEngine;


namespace NSY.Iven
{
    public class CraftList : ItemContainer
    {
        [Header("위에 아이템 슬롯 신경 ㄴㄴ 레시피 리스트 목록")]
        public CraftWindow[] craftwind;
        [Header("모든 아이템")]
        public Item[] Craftingitems;
        [SerializeField] Transform itemsParent; //this
        public event Action<CraftSlot> OnLeftClickEventss;


        protected override void OnValidate()
        {
            if (itemsParent != null)
            {
                itemsParent.GetComponentsInChildren(includeInactive: true, result: Craftslot);
            }
        }

        private void Start()
        {
            for (int i = 0; i < Craftslot.Count; i++)
            {
                Craftslot[i].OnLeftClickEventss += OnLeftClickEventss;
            }
        }
    }
}

