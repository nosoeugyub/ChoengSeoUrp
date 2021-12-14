using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.NPC
{
   public class MainNpc : NPC
    {
        int MainNPCID;
        bool MainNPCIsid;
  
        protected override void AddID(int NPCid, bool NPCisID)
        {
            // base.AddID(NPCid, NPCisID);
            NPCid = MainNPCID;
            NPCisID = MainNPCIsid;
            NPCid = 2000;
            NPCisID = false;
        }

    }
}

