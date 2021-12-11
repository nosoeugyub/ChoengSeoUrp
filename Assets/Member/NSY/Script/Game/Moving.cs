using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Move
{
    public class Moving : MonoBehaviour
    {
        public CharacterController CharControl;
        [SerializeField] Transform target;
        public GameObject Player;
       
     

        public void MoveTo(Vector3 Destination)
        {
            //   GetComponent<CharacterController>().Move(Destination);
            CharControl.Move(Destination);
        }

        public void Distance(float Dis , GameObject Player , GameObject Target)
        {
            Dis = Vector3.Distance(Player.transform.position, Target.transform.position);
        }

    }
}
