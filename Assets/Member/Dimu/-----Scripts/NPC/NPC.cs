using UnityEngine;

namespace Game.NPC
{
    public class NPC : MonoBehaviour
    {
        [SerializeField]
        Character characterType;

        public Character GetCharacterType()
        {
            return characterType;
        }
    }
}

