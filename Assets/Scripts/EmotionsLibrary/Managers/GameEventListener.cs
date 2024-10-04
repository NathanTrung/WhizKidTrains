using UnityEngine;
using UnityEngine.Events;

namespace WhizKid.EmotionsLibrary
{
    public class GameEventListener : MonoBehaviour
    {
        //! Variables
        public GameEvent gameEvent;  // The event this listener is listening for
        public UnityEvent onEventTriggered;  // What happens when the event is triggered

        //! Methods
        public void OnEventTriggered()
        {
            onEventTriggered.Invoke();  // Invoke the assigned UnityEvent
        }

        private void OnEnable()
        {
            gameEvent.AddListener(this);  // Add this listener when enabled
        }

        private void OnDisable()
        {
            gameEvent.RemoveListener(this);  // Remove this listener when disabled
        }
    }

}
