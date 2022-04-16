using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//상태 eum
public enum Bug_FlyStats { Patol = 0 , fly , Sit }


namespace NSY.Bug
{
    public class Bug_fly : BaseGameBug
    {
        private float distance;
        private Vector3 spanVec;
        private GameObject spawnObject;
        private Bug_FlyStats currentFlyStats;

        private Bugstats[] bugflystats;
        private Bugstats currentState;

        public float Distance
        {
            set => distance = Mathf.Max(0, value);
            get => distance;
        }
        public Vector3 SpanVec
        {
            set => spanVec =value;
            get => spanVec;
        }
        public GameObject SpawnObject
        {
            set => spawnObject = value;
            get => spawnObject;
        }
        public Bug_FlyStats CurrentFlyStats
        {
            set => currentFlyStats = value;
            get => currentFlyStats;
        }




        public override void SetUpBugData(string name)
        {
            base.SetUpBugData(name); //버츄얼 함수 호출
            gameObject.name = $"{BugiD:D2} Bug_{name}";

            //상태 등록해야 enter간아옴
            bugflystats = new Bugstats[3];
            bugflystats[(int)Bug_FlyStats.Patol] = new RestAndStart();
            bugflystats[(int)Bug_FlyStats.Sit] = new FindandSit();
            currentState = bugflystats[(int)Bug_FlyStats.Patol];//초기상태는 배회하기
           

             

            //초기화
            distance = 0;
            spanVec = new Vector3(0, 0, 0);
            spawnObject = null;
            currentFlyStats = Bug_FlyStats.Patol;


        }



        public override void Update()
        {
            if (currentState != null)
            {
                currentState.Execute(this);

            }
            //현재 재생중인 상태가 있으면 Exit
          

        }

        public void ChangState(Bug_FlyStats newState)
        {
            if (bugflystats[(int)newState] == null)
            {
                return; //새로바꾸려는 상태가 비어있으면 상태를 나둠
            }
            if (currentState != null )
            {
                currentState.Exit(this);//현재 재생중인 상태가 있으면 나가
            }

            //새로운 상태로 변경하고 새로 바뀐상태의 enter호출
            currentState = bugflystats[(int)newState];
            currentState.Enter(this);
        }
    }

}

