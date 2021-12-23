using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//부모 클래스
namespace NSY.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        internal PlayerInput playerinput;
        [SerializeField]
        internal PlayerMoveMent playermove;
        [SerializeField]
        internal PlayerState playerstate;
        [SerializeField]
        internal PlayerCollision playercollision;

        //Player 상태
        [SerializeField]
        internal CharacterController characterCtrl;
        [SerializeField]
        internal Camera maincamera;
        internal Animator anim;

        //Player 정보
        public float PlayerSpeed;
        internal string CurrentState;


        // Start is called before the first frame update
        void Start()
        {
            characterCtrl = GetComponent<CharacterController>();
            anim = GetComponent<Animator>();
        }

        internal void ChageState(string newState)
        {
            if (newState != CurrentState)
            {
                anim.Play(newState);
                CurrentState = newState;
            }
        }
   
    

    
    }
}

