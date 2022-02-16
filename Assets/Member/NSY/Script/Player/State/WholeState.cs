using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.Manager;

namespace NSY.PlayerState
{
    public class WholeState : FruitState
    {
        bool isFruitDrop = false;

        void Start()
        {
            EventManager.activefruittree += UpdateState;

        }

        void OnDestroy()
        {
            EventManager.activefruittree -= UpdateState;
        }


        public override void ChangeState(FrutStateManager state)
        {


        }

     

        public override void EnterState(FrutStateManager state)
        {
           
            //플레이어가 상호작용을하면 열매가 떨어집니다.
        }

        public override void UpdateState(FrutStateManager state)
        {

            isFruitDrop = true;
                Debug.Log("열매 하강!");
                state.GetComponent<Rigidbody>().useGravity = isFruitDrop;
            

        }
        public override void CollisionEnter(FrutStateManager state, Collision collision)
        {
           
        }
    }

}

