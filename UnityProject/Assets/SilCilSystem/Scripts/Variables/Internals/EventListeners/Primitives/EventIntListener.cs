using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventIntListener : GameEventIntListener, IGameEventSetter<EventInt>
    {
        [SerializeField] private EventInt m_event;
        public override IDisposable Subscibe(Action<int> action) => m_event.Subscribe(action);

        void IGameEventSetter<EventInt>.SetGameEvent(EventInt gameEvent)
        {
            m_event = gameEvent;
        }
    }
}