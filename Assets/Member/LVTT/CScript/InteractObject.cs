using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TT_INTERACT
    {
   
    public class InteractObject : MonoBehaviour
    {
        Inventory inventory;

        public BuildingMat MaterialType;
        public string ItemName;
        public GameObject ItemButton;
        public SpriteRenderer ItemImage;

        public bool isinteracting;

        InteractPlayer PlayerCollision;
        private void Awake()
        {
            
        }
        void Start()
        {
            PlayerCollision = FindObjectOfType<InteractPlayer>();
            inventory = FindObjectOfType<Inventory>();
            ItemImage.sprite = MaterialType.Mats_Image;
            transform.name = MaterialType.name;
            ItemName = MaterialType.name;
            ItemButton = MaterialType.Inventory_Image;
            
        }

        void Update()
        {
                if(isinteracting)
                {
                    if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
                    {
                        ObjPickup();
                    }
                }   
        }
       
        void ObjPickup()
        {

            for (int i = 0; i < inventory.InventorySlotList.Length; i++)
            {
                if (!inventory.InventorySlotList[i].isFull)
                {
                    if (inventory.InventorySlotList[i].haveItem)
                    {
                        if (inventory.InventorySlotList[i].curItemName != null)
                        {

                            if (ItemName == inventory.InventorySlotList[i].curItemName)
                            {
                           
                                inventory.InventorySlotList[i].curItemNum += 1;
                                inventory.InventorySlotList[i].SlotStackTxt.text = inventory.InventorySlotList[i].curItemNum.ToString();
                                if (inventory.InventorySlotList[i].curItemNum == inventory.InventorySlotList[i].maxStack)
                                {
                                    inventory.InventorySlotList[i].isFull = true;
                                }
                                Destroy(gameObject);
                                break;
                            }
                        }
                    }
                    else if (!inventory.InventorySlotList[i].haveItem)
                    {
                        inventory.InventorySlotList[i].SlotStack.SetActive(true);
                        inventory.InventorySlotList[i].haveItem = true;
                        GameObject Itemimage = Instantiate(ItemButton, inventory.InventorySlotList[i].slot.transform, false);
                        Itemimage.name = ItemButton.name;
                        inventory.InventorySlotList[i].curItemNum += 1;
                        inventory.InventorySlotList[i].curItemName = Itemimage.name;
                        inventory.InventorySlotList[i].SlotStackTxt.text = inventory.InventorySlotList[i].curItemNum.ToString();
                        Destroy(gameObject);
                        break;
                    }
                }

            }

        }
       
    }
}

