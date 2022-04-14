using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//전체적인 곤충 컨트롤러
namespace NSY.Bug
{
    public abstract class BaseGameBug : MonoBehaviour
    {
        internal static int m_iNextBugId = 0;
        private string BugName;
        private int bugId;
        public int BugiD //이 스크립트들의 상속받는 벌레들의 고유 번호
        {
            get => bugId;


            set
            {
                bugId = value;
                m_iNextBugId++;
            }
        }


        public virtual void SetUpBugData(string name)
        {//상속된 벌레들에게 데이터 부여
            BugiD = m_iNextBugId;
            BugName = name;

        }

        public abstract void Update(); //벌래들의 행동 

        
    }

}
