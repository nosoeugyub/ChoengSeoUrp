       using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace NSY.Manager
{

    public class EventManager : MonoBehaviour
    {
        //싱글턴
        private EventManager() { }
        private static EventManager _instace = null;
        
        //이벤트 델리게이트
        public delegate void StartStateTutor();
        public static event  StartStateTutor HitFoodBox;
        public delegate void EndStateTutor();
        public static event EndStateTutor UnHitFoodBox;
        //==


        public static EventManager _Instace
        {
            get
            {
                if (_instace ==null)
                {
                    _instace = new EventManager();
                }
                return _instace;

            }
            private set
            {
                _instace = value;
            }
        }
        public void StartTutor()
        {
            if (HitFoodBox != null)
            {
                HitFoodBox();
            }
        }
        public void EndTutor()
        {
            if (UnHitFoodBox != null)
            {
                UnHitFoodBox();
            }
        }
    }   
     
}









