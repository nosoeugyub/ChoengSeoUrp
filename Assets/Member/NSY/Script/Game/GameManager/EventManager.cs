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

        //이벤트 델리게이트, 표지판 튜토리얼
        public delegate void StartSignPost();
        public static event StartSignPost FirstPostCollder;

        public delegate void EndSignPost();
        public static event EndSignPost UnFirstPostCollder;

        //이벤트 델리게이트 ,사과나무초반 튜토리얼
        public delegate void StartTreeTutor();
        public static event StartTreeTutor FristTreeCollder;

        public delegate void EndTreeTutor();
        public static event EndTreeTutor UnFristTreeCollder;
        //==

        public static Action[] EventActions = new Action[2];
        public static Action EventAction;
        //EventAction 는 항상 실행중.
        //이벤트 바로 넣기 >> EventActions[0] = ~~~;
        //이벤트 실행 >>      EventAction += EventActions[0];
        //이벤트 종료 >>      EventAction -= EventActions[0];

        //일반 과일나무와 상호작용 했을띠
        FrutStateManager state;

        public delegate void ActiveFruitTree(FrutStateManager state);
        public static event ActiveFruitTree activefruittree;


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
        //표지판 첫 퀘스트
        public void StartFirstPost()
        {
            if (FirstPostCollder != null)
            {
                FirstPostCollder();
            }
        }
        public void EndFirstPost()
        {
            if (UnFirstPostCollder != null)
            {
                UnFirstPostCollder();
            }
        }

        //사과나무 첫 퀘스트
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

        private void Update()
        {
            EventAction();
        }

        //일반적인 과일나무 상호작용
        public void PlayerActiveFruitTree(FrutStateManager state)
        {
            if (activefruittree != null)
            {
                activefruittree(state);
            }
        }
    }   
     
}









