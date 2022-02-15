﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NSY.PlayerState;


public class FrutStateManager : MonoBehaviour
{








    //상태
    FruitState currentState;
    //각각상태의 스크립트 초기화
   public   IdleState idlestate = new IdleState();
    public WholeState wholeState = new WholeState();


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
   public void SwitchState(FruitState Pstate)
    {
        currentState = Pstate;
        Pstate.EnterState(this);

    }

  

}
