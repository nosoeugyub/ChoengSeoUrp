using NSY.Player;
using UnityEngine;


namespace Game.NPC
{
    public class MainNpc : NPC, IInteractable
    {
        //public  int MainNPCID = 1001;
        //public  bool MainNPCIsid = false;

        public void CanInteract(GameObject player)
        {
            PlayerInput.OnPressFDown = Interact;
        }

        public void EndInteract() { }

        public void Interact()
        {
            PlayDialog();
        }

        public Transform ReturnTF()
        {
            return transform;
        }

        //protected override void AddID(int NPCid, bool NPCisID)
        //{
        //    // base.AddID(NPCid, NPCisID);
        //    NPCid = MainNPCID;
        //    NPCisID = MainNPCIsid;

        //}


    }
}

