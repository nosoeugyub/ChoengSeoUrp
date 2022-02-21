using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace NSY.PlayerState
{
    public abstract class FruitState
    {


        public  abstract  void EnterState(FrutStateManager state);
        public  abstract void UpdateState(FrutStateManager state);

       public abstract void EventState(FrutStateManager state);

        public abstract void CollisionEnter(FrutStateManager state , Collision collision);
    }
}

