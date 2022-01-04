using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace NSY.Manager
{

    public class EventManager : MonoBehaviour
    {
        public event Action FirstAddBox;
        public event Action Firstsign;
        void FirstTut()
        {
            FirstAddBox?.Invoke();
           
        }

    }
     
    
}









