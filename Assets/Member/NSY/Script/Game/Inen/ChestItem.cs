using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;

namespace NSY.Iven
{
    public class ChestItem : MonoBehaviour
    {
        [SerializeField] Item item;
        [SerializeField] InventoryNSY inventory;


        private bool isInRange;

       public void Update()
        {
            if (isInRange && Input.GetKeyDown(KeyCode.E))
            {
                inventory.AddItem(item);
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

