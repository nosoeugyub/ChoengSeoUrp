using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSY.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField]
        PlayerController playerController;

        private void OnTriggerEnter(Collider other)
        {
           
        }
        private void OnTriggerStay(Collider other)
        {
            
        }
        private void OnTriggerExit(Collider other)
        {

        }
    }

}
