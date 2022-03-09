﻿using NSY.Player;
using UnityEngine;


namespace Game.NPC
{
    public class MainNpc : NPC, ITalkable
    {
        //public  int MainNPCID = 1001;
        //public  bool MainNPCIsid = false;

        public string CanInteract()
        {
            //PlayerInput.OnPressFDown = Interact;
            return "말걸기";
        }

        public Transform ReturnTF()
        {
            return transform;
        }

        public void Talk()
        {
            PlayDialog();
        }

        //protected override void AddID(int NPCid, bool NPCisID)
        //{
        //    // base.AddID(NPCid, NPCisID);
        //    NPCid = MainNPCID;
        //    NPCisID = MainNPCIsid;

        //}


    }
}

