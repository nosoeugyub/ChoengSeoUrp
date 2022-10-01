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
    GotoStartPos,
    FadeIn,
    FadeOut,
    StartTalk,
    ChickenGOBearHOuse,
    ChickenGoSheepHouse,
    ChickSuddenlyAppear,
    ChickenAppearAndGetChick,
    ChickenGone,
    DearAppear,
    SheepDearGone,
    BearGoSheepHouse,
    BearGoHisHouse,
}
namespace NSY.Manager
{
    public class DIalogEventManager : MonoBehaviour
    {
        private DIalogEventManager() { }
        private static DIalogEventManager _instace = null;

        public GameObject[] cols;
        public GameObject[] portColBefore;
        public GameObject[] portColAfter;
        public GameObject chick;

        public static Action[] EventActions = new Action[30];
        public static Action[] BackEventActions = new Action[30];
        public static Action<CutType> testevent;
        public static Action EventAction;
        //EventAction 는 항상 실행중.
        //이벤트 바로 넣기 >> EventActions[0] = ~~~;
        //이벤트 실행 >>      EventAction += EventActions[0];
        //이벤트 종료 >>      EventAction -= EventActions[0];

        public static DIalogEventManager _Instace
        {
            get
            {
                if (_instace == null)
                {
                    _instace = new DIalogEventManager();
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








