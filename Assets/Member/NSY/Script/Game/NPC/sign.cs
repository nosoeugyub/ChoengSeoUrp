using Game.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;

namespace Game.NPC
{
    public class sign : NPC
    {
      public  int SignID = 1000;
      public  bool SignIsid = false;
    
        protected override void AddID(int NPCid, bool NPCisID)
        {
            // base.AddID(NPCid, NPCisID);
            NPCid = SignID;
            NPCisID = SignIsid;
           
        }

    

    }
}


