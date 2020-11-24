using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventFloatListener : GameEventFloatListener, IGameEventSetter<EventFloat>
    {
        [SerializeField] private EventFloat m_event = default;
        public override IDisposable Subscibe(Action<float> action) => m_event.Subscribe(action);

        void IGameEventSetter<EventFloat>.SetGameEvent(EventFloat gameEvent)
        {
            m_event = gameEvent;
        }
    }
}