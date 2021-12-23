
using UnityEngine;

namespace NSY.PlayerState
{

    public class IdleState : PlayerState
    {
        [SerializeField]
        private int hungry;
        private int tired;
        private int thirsty;
        protected override int Hungry 
        { 
            get => hungry; 
            set
            {
                if (hungry >=0)
                {
                    hungry = value;
                }
                else
                {
                    hungry = -1;
                }
            }
        }
        protected override int Tired { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        protected override int Thirsty { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override void ChangeState(PlayerStateManager state)
        {
            
        }

        public override void EnterState(PlayerStateManager state)
        {
            hungry = 100;
            Debug.Log(hungry);
        }

        public override void UpdateState(PlayerStateManager state)
        {
            if (hungry != 0)
            {
                Debug.Log("배고픔 감소");
                hungry -= 1;
            }
            else if(hungry <= 50)
                state.SwitchState(state.dangerState);
        }
    }
}

