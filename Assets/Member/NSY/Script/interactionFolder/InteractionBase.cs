using Player.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.interaction
{
    public class InteractionBase : PlyerState
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override  void Update()
        {
            base.Update();
            interaction();
        }

        private void interaction()
        {
           
        }
    }
}

