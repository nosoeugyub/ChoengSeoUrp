using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSY.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField]
        PlayerController playerController;

        private GameObject triggeringNpc;
        private bool trigger;


        private void Update()
        {
                //상호작용하면 불러올 함수들
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("NPC"))
            {
                trigger = true;
                triggeringNpc = other.gameObject;
            }
            if (other.CompareTag("InteractObj"))
            {
                other.GetComponent<InteractObject>().DropItems();
                print("spawn" + other.name);

            }
        }
        private void OnTriggerStay(Collider other)
        {
            
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("NPC"))
            {
                trigger = false;
                triggeringNpc = null;
            }
        }
    }

}
