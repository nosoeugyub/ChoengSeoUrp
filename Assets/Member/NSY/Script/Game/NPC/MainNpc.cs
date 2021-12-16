using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.NPC
{
   public class MainNpc : NPC
    {
        int MainNPCID = 2000;
        bool MainNPCIsid = false;
  
        protected override void AddID(int NPCid, bool NPCisID)
        {
            // base.AddID(NPCid, NPCisID);
            NPCid = MainNPCID;
            NPCisID = MainNPCIsid;
           
        }

    }
}

