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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

