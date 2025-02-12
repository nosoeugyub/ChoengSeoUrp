﻿using System.Collections;
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
        internal PlayerInteract playerinteract;
        //Player 상태
        //[SerializeField]
        internal CharacterController characterCtrl;
        [SerializeField]
        internal Camera maincamera;
        //internal Animator anim;

        //Player 정보
        public float PlayerSpeed;
        internal string CurrentState;

        //스프라이트 애니메이션

       public Animator SpritePlayerAnim;

       
        // Start is called before the first frame update
        void Awake()
        {
            characterCtrl = GetComponent<CharacterController>();
            //anim = GetComponent<Animator>();
        }

        internal void ChangeState(string newState)
        {
            if (newState != CurrentState)
            {
                SpritePlayerAnim.Play(newState);
                SpritePlayerAnim.speed = 1;
                CurrentState = newState;
            }
        }
        
        
       
    }
}

