using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Events
{
    [CreateAssetMenu(fileName = "NewGameEvent", menuName = "Foundation/Events/GameEvent")]
    public class GameEvent<T> : ScriptableObject
    {
        private readonly List<GameEventListener<T>> _listeners = new();

        public void Raise(T value)
        {
            foreach (GameEventListener<T> listener in _listeners)
            {
                listener.OnEventRaised(value);
            }
        }

        public void Register(GameEventListener<T> listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void Unregister(GameEventListener<T> listener)
        {
            _listeners.Remove(listener);
        }
    }
}
