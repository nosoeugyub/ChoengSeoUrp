using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;

namespace NSY.Iven
{
    public class ChestItem : MonoBehaviour
    {
        Item item;
        InventoryNSY inventory;


        private bool isInRange;

        private void Start()
        {
            inventory = FindObjectOfType<InventoryNSY>();
        }

        public void OnValidate()
        {
            if (inventory == null)
            {
                inventory = FindObjectOfType<InventoryNSY>();
            }
           
        }

        public void Update()
        {
            if (isInRange && Input.GetKeyDown(KeyCode.E))
            {
                if (item != null)
                {
                    inventory.AddItem(Instantiate(item));
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

