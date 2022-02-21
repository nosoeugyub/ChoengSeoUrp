using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSY.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        PlayerController playerController;

        public delegate void InputEvent();
        public static InputEvent OnPressFDown; //F 누를 때 실행할 메서드 갈아껴주는 용도

        //플레이어가 누르는 키입력
        [Space]
        public KeyCode _InputInterBtn = KeyCode.F;
      
       
        internal bool interectObj;


        // Update is called once per frame
        void Update()
        {

            ActiveObj();
        }
        /// <summary> 튜토리얼 인풋
        





        /// </summary>



       
        public void ActiveObj()
        {
            if (Input.GetKey(_InputInterBtn))
            {
                interectObj = true;
            }
            else
            {
                interectObj = false;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                print("상호작용");
                OnPressFDown();
            }
        }


     
    }
}

