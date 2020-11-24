using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventStringListener : GameEventStringListener, IGameEventSetter<EventString>
    {
        [SerializeField] private EventString m_event = default;
        public override IDisposable Subscibe(Action<string> action) => m_event.Subscribe(action);

        void IGameEventSetter<EventString>.SetGameEvent(EventString gameEvent)
        {
            m_event = gameEvent;
        }
    }
}