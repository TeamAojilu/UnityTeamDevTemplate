using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventNoArgsListener : GameEventListener, IGameEventSetter<EventNoArgs>
    {
        [SerializeField] private EventNoArgs m_event = default;

        public override IDisposable Subscibe(Action action) => m_event.Subscribe(action);

        void IGameEventSetter<EventNoArgs>.SetGameEvent(EventNoArgs gameEvent)
        {
            m_event = gameEvent;
        }
    }
}