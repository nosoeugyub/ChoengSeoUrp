using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSY.Bug
{
    public class RestAndStart : Bugstats //배회하기
    {


        public override void Enter(Bug_fly bugfly)
        {
            //fly 애니메이션 공중으로 조금 띄워집니다

            //   bugfly.CurrentFlyStats = Bug_FlyStats.Patol;
            Debug.Log("배회하셈");
        }

        public override void Execute(Bug_fly bugfly)
        {
           
            Debug.Log(" 줜나배회중");
           StartCoroutine(Flying());
            
            if (Input.GetKey(KeyCode.Space ))
            {
                bugfly.ChangeState(Bug_BugStats.Sit);
            }
        }

        public override void Exit(Bug_fly bugfly)
        {
            Debug.Log(" 오브젝트로 간다.");
        }

        IEnumerator Flying()
        {

            yield return null;
        }




    }
    /// <summary>
    /// //다른상태
    /// </summary>
    public class Sit : Bugstats // 앉을때 찾고  착지하는 상태
    {




        public override void Enter(Bug_fly bugfly)
        {
            //fly 
          //  bugfly.CurrentFlyStats = Bug_FlyStats.Sit;
            Debug.Log(" 목표물 찾음 ");
        }

        public override void Execute(Bug_fly bugfly)
        {
           
            Debug.Log(" 착지 이후 아이들상태만 계속..");
        }

        public override void Exit(Bug_fly bugfly)
        {

        }
    }
}
