
using UnityEngine;

namespace NSY.PlayerState
{

    public class IdleState : FruitState
    {

        Vector3 StartApplesize = new Vector3(0.01f, 0.01f, 0.01f);
        Vector3 GrowAppleScaler = new Vector3(0.01f, 0.01f, 0.01f);
        float GrowSpeed = 2.0f;


        public override void ChangeState(FrutStateManager state)
        {
            

        }

        public override void CollisionEnter(FrutStateManager state, Collision collision)
        {
            throw new System.NotImplementedException();
        }

        public override void EnterState(FrutStateManager state)
        {
            state.transform.localScale = StartApplesize;
            Debug.Log("크기 처음임");
        }

        public override void UpdateState(FrutStateManager state)
        {
            if (state.transform.localScale.x <0.08f && state.transform.localScale.y < 0.08f)
            {
                state.transform.localScale += GrowAppleScaler * Time.deltaTime* GrowSpeed;
            }
            else
            {
                state.SwitchState(state.wholeState);
            }

        }
       
    }
}

