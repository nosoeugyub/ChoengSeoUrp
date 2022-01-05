
using UnityEngine;

namespace NSY.PlayerState
{
    public class dangerousState : PlayerState
    {
      

        public override void ChangeState(PlayerStateManager state)
        {
            throw new System.NotImplementedException();
        }

        public override void EnterState(PlayerStateManager state)
        {
            Debug.Log("음식을 먹어주세요");

        }

        public override void UpdateState(PlayerStateManager state)
        {
            Debug.Log("HP감소와 케릭터의 이동속도가 느려집니다.");
          
        }
 
    }
}

