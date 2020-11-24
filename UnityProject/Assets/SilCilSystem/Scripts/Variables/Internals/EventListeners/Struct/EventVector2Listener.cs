using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventVector2Listener : GameEventVector2Listener, IGameEventSetter<EventVector2>
    {
        [SerializeField] private EventVector2 m_event = default;
        public override IDisposable Subscibe(Action<Vector2> action) => m_event.Subscribe(action);

        void IGameEventSetter<EventVector2>.SetGameEvent(EventVector2 gameEvent)
        {
            m_event = gameEvent;
        }
    }
}