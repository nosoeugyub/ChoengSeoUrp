using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSY.Bug
{
    public abstract class Bugstats : MonoBehaviour
    {
        public abstract void Enter(Bug_fly bugfly);
        public abstract void Execute(Bug_fly bugfly);

             public abstract void Exit(Bug_fly bugfly);


    }


}

