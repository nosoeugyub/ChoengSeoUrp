using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;

namespace NSY.Iven
{
    public class ChestItem : MonoBehaviour
    {
        public  Item item;
        public  InventoryNSY inventory;
        public SpriteRenderer spriteRenderer;
        public int amount = 1;

        public Color emptyColor;
        private bool isInRange;

        

        public void OnValidate()
        {
            if (inventory == null)
            {
                inventory = FindObjectOfType<InventoryNSY>();
            }
            if (inventory == null)
            {
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }
            spriteRenderer.sprite = item.ItemSprite;
            spriteRenderer.enabled = false;



        }

        public void Update()
        {
            if (isInRange && Input.GetKeyDown(KeyCode.E))
            {
                Item itemCopy = item.GetCopy();
                if (inventory.AddItem(itemCopy))
                {
                    amount--;
                    if (amount ==0)
                    {
                        spriteRenderer.color = emptyColor;
                    }
                }
                else
                {
                    itemCopy.Destroy();

                }
            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            isInRange = true;
        }
        private void OnTriggerExit(Collider other)
        {
            isInRange = false;
        }

    }


}

