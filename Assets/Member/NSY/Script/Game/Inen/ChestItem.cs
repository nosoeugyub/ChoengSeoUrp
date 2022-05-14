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

        private void Awake()
        {
            item.GetCountItems = 0;
        }

        public void OnValidate()
        {
         
            if (inventory == null)
            {
                inventory = FindObjectOfType<InventoryNSY>();
            }
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }
            spriteRenderer.sprite = item.ItemSprite;
            spriteRenderer.enabled = false;



        }

        public void Update()
        {
            isInRange = true;
            if (isInRange && Input.GetKeyDown(KeyCode.E))
            {
                Item itemCopy = item.GetCopy();
                
                if (inventory.AddItem(item.GetCopy()))
                {

                    itemCopy.GetCountItems++;
                    if (itemCopy.CleanAmount == 0)
                    {
                        //spriteRenderer.color = emptyColor;
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

