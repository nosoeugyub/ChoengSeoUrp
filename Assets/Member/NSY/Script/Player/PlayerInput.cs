using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSY.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        PlayerController playerController;


        internal bool inputInventroy;
        internal bool GetItem;
        internal bool activeNpc;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ActiveItem();
            ActiveNpc();
        }
        //아이템먹기
        public void ActiveItem()
        {
            if (Input.GetKey(KeyCode.G))
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
            if (Input.GetKey(KeyCode.Space))
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

