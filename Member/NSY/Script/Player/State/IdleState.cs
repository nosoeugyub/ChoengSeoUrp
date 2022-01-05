
using UnityEngine;

namespace NSY.PlayerState
{

    public class IdleState : PlayerState
    {


       


        public override void ChangeState(PlayerStateManager state)
        {
            

        }

        public override void EnterState(PlayerStateManager state)
        {
            Debug.Log("살아있어요");
            currentHungry = 100;
        }

        public override void UpdateState(PlayerStateManager state)
        {
            if (currentHungry > 0)
            {
                if (HungryCurrentTime <= HungryTime)
                {
                    HungryCurrentTime++;
                }
                else
                {
                    currentHungry--;
                    HungryCurrentTime = 0;
                }
            }
            else
            state.SwitchState(state.dangerState);

      

        }
       
    }
}

