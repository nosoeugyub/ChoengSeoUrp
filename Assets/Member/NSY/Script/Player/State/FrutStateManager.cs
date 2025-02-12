﻿using NSY.Player;
using NSY.PlayerState;
using UnityEngine;

public class FrutStateManager : ItemObject//, IInteractable
{
    //상태
    FruitState currentState;
    //각각상태의 스크립트 초기화
    public IdleState idlestate = new IdleState();
    public WholeState wholeState = new WholeState();

    private void OnEnable()
    {
        currentState = idlestate; //초기 상태
        currentState.EnterState(this);//상태에대한 참조
    }
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
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

    public void OnCollisionEnter(Collision collision)
    {
        currentState.CollisionEnter(this, collision);
    }

    public void CanInteract(GameObject player)
    {
        //PlayerInput.OnPressFDown = Interact;
    }
    public Transform ReturnTF()
    {
        return transform;
    }
}
