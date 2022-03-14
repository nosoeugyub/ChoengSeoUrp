using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.Iven;
using DM.Inven;
using System;
using UnityEngine.UI;


//부모 클래스
namespace NSY.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        internal PlayerVital playerVital;
        [SerializeField]
        internal PlayerInput playerinput;
        [SerializeField]
        internal PlayerMoveMent playermove;
       
        [SerializeField]
        internal PlayerInteract playercollision;

        //Player 상태
        [SerializeField]
        internal CharacterController characterCtrl;
        [SerializeField]
        internal Camera maincamera;
        internal Animator anim;

        //Player 정보
        public float PlayerSpeed;
        internal string CurrentState;

        //스프라이트 애니메이션

       public Animator SpritePlayerAnim;
       public Animator GamePlayerAnim;

        //피로도
        public int Tired;

       
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

