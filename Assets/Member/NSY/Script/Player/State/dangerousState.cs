
using UnityEngine;

namespace NSY.PlayerState
{
    public class dangerousState : PlayerState
    {
        protected override int Hungry { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        protected override int Tired { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        protected override int Thirsty { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override void ChangeState(PlayerStateManager state)
        {
            throw new System.NotImplementedException();
        }

        public override void EnterState(PlayerStateManager state)
        {
            Debug.Log("배고픈 상태");
        }

        public override void UpdateState(PlayerStateManager state)
        {
            throw new System.NotImplementedException();
        }
    }
}

