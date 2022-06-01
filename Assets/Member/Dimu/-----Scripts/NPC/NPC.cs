using UnityEngine;

namespace DM.NPC
{
    public class NPC : Interactable
    {
        [SerializeField]
        Character characterType;

        public override int CanInteract()
        {
            throw new System.NotImplementedException();
        }
        public Character GetCharacterType()
        {
            return characterType;
        }
        public virtual void Talk()
        {

        }
    }
}

