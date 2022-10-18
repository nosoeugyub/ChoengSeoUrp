using UnityEngine;

namespace DM.Event
{
    public class EventContainer : MonoBehaviour
    {
        [SerializeField] GameEvent[] gameEvent;

        public void RaiseEvent(GameEventType gameEventType)
        {
            if (gameEvent[(int)gameEventType])
                gameEvent[(int)gameEventType].Raise();
        }
    }

    public enum GameEventType
    {
        playerMoveOnEvent, playerMoveOffEvent, playerCanInteractEvent, playerCantInteractEvent
    }
}