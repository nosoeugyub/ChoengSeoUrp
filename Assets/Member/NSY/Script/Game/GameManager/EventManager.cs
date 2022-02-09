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
        
        //이벤트 델리게이트 ,초반 튜토리얼
        public delegate void StartTreeTutor();
        public static event StartTreeTutor FristTreeCollder;

        public delegate void EndTreeTutor();
        public static event StartTreeTutor UnFristTreeCollder;
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
        public void StartFirstTree()
        {
            if (FristTreeCollder != null)
            {
                FristTreeCollder();
            }
        }
        public void EndFirstTree()
        {
            if (UnFristTreeCollder != null)
            {
                UnFristTreeCollder();
            }
        }
    }   
     
}









