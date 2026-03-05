using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Events
{
    [CreateAssetMenu(fileName = "NewGameEvent", menuName = "Assets/Foundation/Events/GameEvent.cs")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> _listeners = new();

        public void Raise()
        {
            foreach (GameEventListener listener in _listeners)
            {
                listener.OnEventRaised();
            }
        }

        public void Register(GameEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void Unregister(GameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}
