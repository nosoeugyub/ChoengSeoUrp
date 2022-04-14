using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//상태 eum
public enum Bug_FlyStats { Sit =0 , fly , getback }


namespace NSY.Bug
{
    public class Bug_fly : BaseGameBug
    {
        private float distance;
        private Vector3 spanVec;
        private GameObject spawnObject;


        private Bug_FlyStats[] bugflystats;
        private Bug_FlyStats CurrentState;

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

        public override void SetUpBugData(string name)
        {
            base.SetUpBugData(name); //버츄얼 함수 호출
            gameObject.name = $"{BugiD:D2} Bug_{name}";

            //초기화
            distance = 0;
            spanVec = new Vector3(0, 0, 0);
            spawnObject = null;

            
        }



        public override void Update()
        {
            Debug.Log(" 꽃위에 앉으렴");

        }
    }

}

