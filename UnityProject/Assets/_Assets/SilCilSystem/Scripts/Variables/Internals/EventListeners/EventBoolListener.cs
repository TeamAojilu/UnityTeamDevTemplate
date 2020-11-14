using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventBoolListener : GameEventBoolListener, IGameEventSetter<EventBool>
    {
        [SerializeField] private EventBool m_event = default;
        public override IDisposable Subscibe(Action<bool> action) => m_event.Subscribe(action);

        void IGameEventSetter<EventBool>.SetGameEvent(EventBool gameEvent)
        {
            m_event = gameEvent;
        }
    }
}