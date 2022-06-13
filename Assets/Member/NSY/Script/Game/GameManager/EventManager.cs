using System;
using UnityEngine;

public enum EventEnum
{
    Test = 1,
    MoveToMyHome,
    MoveToBearsHouse,
    OnFollowPlayer,
    MoveToWalPort,
    OpenRoad1,
    OpenRoad2,
    OnChick,
    GotoBearsWithSheep,
    GotoBackWithSheep,
    DownClean,
    DownCleanDouble,
}
namespace NSY.Manager
{
    //ㅈㅅ합니다 개같은 코드
    public class EventManager : MonoBehaviour
    {
        //싱글턴
        private EventManager() { }
        private static EventManager _instace = null;

        public GameObject[] cols;
        public GameObject chick;

        ////이벤트 델리게이트, 표지판 튜토리얼
        //public delegate void StartSignPost();
        //public static event StartSignPost FirstPostCollder;

        //public delegate void EndSignPost();
        //public static event EndSignPost UnFirstPostCollder;

        ////이벤트 델리게이트 ,사과나무초반 튜토리얼
        //public delegate void StartTreeTutor();
        //public static event StartTreeTutor FristTreeCollder;

        //public delegate void OpenRoad();
        //public static event OpenRoad OpenRoadPort;
        //public static event OpenRoad OpenRoadFall;


        public static Action[] EventActions = new Action[15];
        public static Action<CutType> testevent;
        public static Action EventAction;
        //EventAction 는 항상 실행중.
        //이벤트 바로 넣기 >> EventActions[0] = ~~~;
        //이벤트 실행 >>      EventAction += EventActions[0];
        //이벤트 종료 >>      EventAction -= EventActions[0];

        //일반 과일나무와 상호작용 했을띠
        //FrutStateManager state;

        //public delegate void ActiveFruitTree();
        //public static event ActiveFruitTree activefruittree;

        public static EventManager _Instace
        {
            get
            {
                if (_instace == null)
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

        private void Start()
        {
            EventActions[((int)EventEnum.OpenRoad1)] = OpenPortCol1;
            EventActions[((int)EventEnum.OpenRoad2)] = OpenPortCol2;
            EventActions[((int)EventEnum.OnChick)] = OnEnableChick;

        }
        private void Update()
        {
            if (EventAction == null) return;
            EventAction(); //이벤트 실행
        }

        public void OpenPortCol1()
        {
            cols[0].SetActive(false);
        }
        public void OpenPortCol2()
        {
            cols[1].SetActive(false);
        }
        public void OnEnableChick()
        {
            chick.SetActive(true);
        }
    }
}








