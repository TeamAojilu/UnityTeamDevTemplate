using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventColorListener : GameEventColorListener, IGameEventSetter<EventColor>
    {
        [SerializeField] private EventColor m_event = default;
        public override IDisposable Subscibe(Action<Color> action) => m_event.Subscribe(action);

        void IGameEventSetter<EventColor>.SetGameEvent(EventColor gameEvent)
        {
            m_event = gameEvent;
        }
    }
}