using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.Manager;


namespace Game.NPC
{
    


    public  class NPC : MonoBehaviour
    {
        [SerializeField]
        Character characterType;

        public int GetCharacterType()
        {
            return (int)characterType;
        }
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
        public void PlayDialog()
        {
           SuperManager.Instance.dialogueManager.FirstShowDialog((int)characterType);
        }
        
      

    }

 
}

