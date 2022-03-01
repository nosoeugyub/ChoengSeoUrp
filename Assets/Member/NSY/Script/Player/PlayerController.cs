using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.Iven;
using DM.Inven;
using System;
using UnityEngine.UI;


//부모 클래스
namespace NSY.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        internal PlayerVital playerVital;
        [SerializeField]
        internal PlayerInput playerinput;
        [SerializeField]
        internal PlayerMoveMent playermove;
       
        [SerializeField]
        internal PlayerInteract playercollision;

        //Player 상태
        [SerializeField]
        internal CharacterController characterCtrl;
        [SerializeField]
        internal Camera maincamera;
        internal Animator anim;

        //Player 정보
        public float PlayerSpeed;
        internal string CurrentState;

        //스프라이트 애니메이션

       public Animator SpritePlayerAnim;
       public Animator GamePlayerAnim;

        // 이벤트 드래그 엔 드롭
        [SerializeField] InventoryNSY inventory;
        [SerializeField] EquipPanel equipmentPanel;
        [SerializeField] Image DraggableItem;

        private void Awake()
        {
            //장착 관련 이벤
            inventory.OnRightClickEvent += Equip;
            equipmentPanel.OnRightClickEvent += UnEquip;
            //드래그 이벤트
            inventory.OnBeginDragEvent += BeginDrag;
            equipmentPanel.OnBeginDragEvent += BeginDrag;
            //드래그 끝 이벤트
            inventory.OnEndDragEvent += EndDrag;
            equipmentPanel.OnEndDragEvent += EndDrag;
            //드래근
            inventory.OnDragEvent += Drag;
            equipmentPanel.OnDragEvent += Drag;
            //드롭
            inventory.OnDropEvent += Drop;
            equipmentPanel.OnDropEvent += Drop;
        }
        // Start is called before the first frame update
        void Start()
        {
            characterCtrl = GetComponent<CharacterController>();
            anim = GetComponent<Animator>();
        }

        internal void ChageState(string newState)
        {
            if (newState != CurrentState)
            {
                anim.Play(newState);
                CurrentState = newState;
            }
        }
        
     
        //드래그 엔 드롭 이벤트 함수
        public void Equip(ItemSlot itemslot )
        {
            Item EquippableItem = itemslot.item as Item;
            if (EquippableItem != null)
            {
                Equip(EquippableItem);
            }
        }

        private void Equip(Item equippableItem)
        {
            if (inventory.RemoveItem(equippableItem))
            {
                Item previousItem;
                if (equipmentPanel.AddItem(equippableItem, out previousItem))
                {
                    if (previousItem != null)
                    {
                        inventory.AddItem(previousItem);
                        // previousItem.UnEquip(this);

                    }
                    //  Item.Equals(this);
                }
            }
        }

        public void UnEquip(ItemSlot itemslot)
        {
            Item EquippableItem = itemslot.item as Item;
            if (EquippableItem != null)
            {
                UnEquip(EquippableItem);
            }
        }

        private void UnEquip(Item equippableItem)
        {
            throw new NotImplementedException();
        }

     

        private void BeginDrag(ItemSlot itemSlot)
        {
            if (itemSlot.item != null)
            {

            }
        }
        private void EndDrag(ItemSlot itemSlot)
        {

        }
        private void Drag(ItemSlot itemSlot)
        {

        }
        private void Drop(ItemSlot itemSlot)
        {

        }
    }
}

