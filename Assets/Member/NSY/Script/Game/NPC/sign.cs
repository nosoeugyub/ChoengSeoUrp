using NSY.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.NPC
{
    public class sign : NPC
    {
      public  int SignID ;
      public  bool SignIsid = false;   
        protected override void AddID(int NPCid, bool NPCisID)
        {
            NPCid = SignID;
            NPCisID = SignIsid;
        }

    }
}


