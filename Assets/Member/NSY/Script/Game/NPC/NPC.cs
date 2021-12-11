using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.NPC
{
    


    public  class NPC : MonoBehaviour
    {
        protected string Name;

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

    class MainNPC : NPC
    {
        protected override void talk()
        {

            base.talk();
        }
    }


    class RabbitNPC : NPC
    {
        protected override void talk()
        {

            base.talk();
        }
    }
}

