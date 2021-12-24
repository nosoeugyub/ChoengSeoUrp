using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.PlayerState;


public class PlayerStateManager : MonoBehaviour
{
    PlayerState currentState;
    //각각상태의 스크립트 초기화
   public   IdleState idlestate = new IdleState();
   public dangerousState dangerState = new dangerousState();
   public DieState diestate = new DieState();


    private void Start()
    {
        currentState = idlestate; //초기 상태
        currentState.EnterState(this);//상태에대한 참조

    }
    private void Update()
    {
        currentState.UpdateState(this);
    }
    //속성 바꾸기 
   public void SwitchState(PlayerState Pstate)
    {
        currentState = Pstate;
        Pstate.EnterState(this);

    }
}
