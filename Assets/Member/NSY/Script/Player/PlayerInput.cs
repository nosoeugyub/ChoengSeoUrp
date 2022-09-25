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
        public static InputEvent OnPressESCDown; //F 누를 때 실행할 메서드 갈아껴주는 용도

        //플레이어가 누르는 키입력
        [Space]
        public KeyCode _InputInterBtn = KeyCode.F;
        public KeyCode _escKey = KeyCode.Escape;

        [Space]
        public KeyCode scaleUpKey = KeyCode.W;
        public KeyCode scaleDownKey = KeyCode.S;
        public KeyCode rotateLeftKey = KeyCode.A;
        public KeyCode rotateRightKey = KeyCode.D;
        public KeyCode frontKey = KeyCode.E;
        public KeyCode BackKey = KeyCode.Q;


        internal bool interectObj;

        void Update()
        {

            ActiveObj();
        }
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

            if (Input.GetKeyDown(_InputInterBtn))
            {
                OnPressFDown();
            }
            if (Input.GetKeyDown(_escKey))
            {
                OnPressESCDown();
            }
        }
    }
}

