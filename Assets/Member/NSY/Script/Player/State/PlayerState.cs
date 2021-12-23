using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSY.PlayerState
{
    public abstract class PlayerState 
    {


        abstract protected int Hungry
        {
            get; set; 
        }
        abstract protected int Tired 
        {
            get; set; 
        }
        abstract protected int Thirsty 
        {
            get; set; 
        }


      public  abstract  void EnterState(PlayerStateManager state);
      public  abstract void UpdateState(PlayerStateManager state);

       public abstract void ChangeState(PlayerStateManager state);

    }
}

