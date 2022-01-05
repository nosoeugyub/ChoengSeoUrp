using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TT_INTERACT
{
    [System.Serializable]
    public class Slot
    {
        public string SlotNum;
        public GameObject SlotStack;
        public Text SlotStackTxt;
        public int maxStack;
        public bool isFull;
        public GameObject slot;
        [HideInInspector]
        public string curItemName;
        [HideInInspector]
        public int curItemNum;
        public bool haveItem;

    }

    public class Inventory : MonoBehaviour
    {
        [SerializeField] GameObject InventoryUI ;
        public Slot[] InventorySlotList;

        public bool isOpen = false;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                switch (isOpen)
                {
                    case true:
                        InventoryUI.SetActive(false);
                        isOpen = false;
                        break;
                    case false:
                        InventoryUI.SetActive(true);
                        isOpen = true;
                        break;
                }
            }
        }

        private void StackNumEnable()
        {
            for (int i = 0; i < InventorySlotList.Length; i++)
            {

            }    
        }
    }
}

