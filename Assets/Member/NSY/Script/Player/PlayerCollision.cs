using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSY.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField]
        PlayerController playerController;

        private GameObject TriggerNPC;
        private bool TriggerCheck;



        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("NPC"))
            {
                TriggerCheck = true;
                TriggerNPC = other.gameObject;
            }
        }
        private void OnTriggerStay(Collider other)
        {
            
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("NPC"))
            {
                TriggerCheck = false;
                TriggerNPC =null;
            }
        }
    }

}
