using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSY.BUGS
{
    public class BugStats : MonoBehaviour
    {
        private GameObject Bugs { get; set; }
        private string BugsNames { get; set; }
        private bool BoolBug { get; set; }
        //속성값
        private float Range;



        //애니메이션
        Animator BugAnim;
        bool isAction;





        private void Start()
        {
            BugAnim = GetComponent<Animator>();
        }

        protected virtual void Update()
        {

        }
        public BugStats()
        {
            Bugs = null;
            BugsNames = "BUG";
            BoolBug = false;
        }



    }


}

