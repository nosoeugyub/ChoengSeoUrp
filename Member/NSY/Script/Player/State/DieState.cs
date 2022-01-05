using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSY.PlayerState
{
    public class DieState : PlayerState
    {
       

        public override void ChangeState(PlayerStateManager state)
        {
            throw new System.NotImplementedException();
        }

        public override void EnterState(PlayerStateManager state)
        {
            Debug.Log("게임 오버");
        }

        public override void UpdateState(PlayerStateManager state)
        {
            Debug.Log("게임 클로즈창떠");
        }
    }

}
