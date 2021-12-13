using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.NPC
{
    


    public  class NPC : MonoBehaviour
    {
        protected string Name;
        protected int Id;
        protected bool isID;

        protected virtual void Start()
        {
            

        }

        protected virtual void Update()
        {
          
        }

      
        protected virtual void talk()
        {
            Debug.Log("나는" + name);
        }

    }

 
}

