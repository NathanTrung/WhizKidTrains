using System.Collections.Generic;
using UnityEngine;

namespace WhizKid.EmotionsLibrary
{
    [CreateAssetMenu(menuName = "Game Event")]
    public class GameEvent : ScriptableObject
    {
        //! Variables
        private List<GameEventListener> _listeners = new List<GameEventListener>();

        //! Methods
        public void TriggerEvent()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventTriggered();
            }
        }

        public void AddListener(GameEventListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(GameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}
