using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace NSY.PlayerState
{
    public abstract class PlayerState 
    {

       
        public int MaxHP=100;
        public int currentHP { get; set; }
        public int HPTime = 500;
        public int HPCurrentTime;

        [Header("배고픔")]
        public int MaxHungry=100;
        public int currentHungry;
        public int currenthungry { get { return currentHungry; } set { if (value < 0) currentHungry = 0; else currentHungry = value; } }
        public int HungryTime = 200;
        public int HungryCurrentTime;


        //[Header("목마름")]
        //public int MaxThirsty=100;
        //public int currentThirsty;
        //public int currentthirsty { get { return currentThirsty;  } set { if (value < 0) currentThirsty = 0; else currentThirsty = value; } }
        //public int ThirstyTime= 300;
        //public int ThirstyCurrentTime;
       

        





        public  abstract  void EnterState(PlayerStateManager state);
        public  abstract void UpdateState(PlayerStateManager state);

       public abstract void ChangeState(PlayerStateManager state);

    }
}

