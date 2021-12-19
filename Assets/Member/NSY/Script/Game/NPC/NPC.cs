using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.NPC
{
    


    public  class NPC : MonoBehaviour
    {
        public NPC()
        {
            name = "NPC";
            id = 0;
            isID = false;
        }

        public int id { get; set; }// 변수역할과 동시에 입력값을 받을수도 있음
        public string name { get; set; }
        public bool isID { get; set; }



        protected virtual void Start()
        {
            

        }

        protected virtual void Update()
        {
          
        }
   
        protected virtual void AddID(int NPCid, bool NPCisID)
        {
        
        }
        protected virtual void PlayDierlog()
        {

        }
        
      

    }

 
}

