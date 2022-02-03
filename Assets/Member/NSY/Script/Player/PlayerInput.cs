using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSY.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        PlayerController playerController;

        //플레이어가 누르는 키입력
        [Space]
        public KeyCode _GetItem = KeyCode.E;
        public KeyCode _GetNPC = KeyCode.Space;
        public KeyCode _InputBackBtn = KeyCode.B;

      
        internal bool GetItem;
        internal bool activeNpc;


        // Update is called once per frame
        void Update()
        {
            ActiveItem();
            ActiveNpc();
        }
        /// <summary> 튜토리얼 인풋
        





        /// </summary>



        //아이템먹기
        public void ActiveItem()
        {
            if (Input.GetKey(_GetItem))
            {
                GetItem = true;
            }
            else
            {
                GetItem = false;
            }
        }
        public void ActiveNpc()
        {
            if (Input.GetKey(_GetNPC))
            {
                activeNpc = true;
            }
            else
            {
                activeNpc = false;
            }
        }
    }
}

