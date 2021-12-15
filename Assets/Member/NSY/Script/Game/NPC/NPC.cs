using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.NPC
{
    


    public  class NPC : MonoBehaviour
    {
      

        public int id { get; set; }
        public string name { get; set; }
        public bool isID { get; set; }



        protected virtual void Start()
        {
            

        }

        protected virtual void Update()
        {
          
        }
        public NPC()
        {
            name = "NPC";
            id = 0;
            isID = false;
        }
        protected virtual void AddID(int NPCid, bool NPCisID)
        {
        
        }
        protected virtual void PlayDierlog()
        {

        }
      
      

    }

 
}

