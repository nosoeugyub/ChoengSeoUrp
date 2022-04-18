using UnityEngine;

namespace DM.NPC
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

