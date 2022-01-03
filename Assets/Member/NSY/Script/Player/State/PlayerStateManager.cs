using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NSY.PlayerState;


public class PlayerStateManager : MonoBehaviour
{
    public const int HP = 0, HUNGRY = 1, THIRSTY = 2;


    [Header("이미지")]
    public Image[] StatusPlayer_Image;




    //상태
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
      
       
        GaugeUpdate();

    }
    //속성 바꾸기 
   public void SwitchState(PlayerState Pstate)
    {
        currentState = Pstate;
        Pstate.EnterState(this);

    }

    void GaugeUpdate()
    {
     //   StatusPlayer_Image[0].fillAmount = (float)currentState.currentHP / currentState.MaxHP;
        StatusPlayer_Image[HP].fillAmount = (float)currentState.currentHungry / currentState.MaxHungry;
      //  StatusPlayer_Image[2].fillAmount = (float)currentState.currentThirsty / currentState.MaxThirsty;
    }

}
