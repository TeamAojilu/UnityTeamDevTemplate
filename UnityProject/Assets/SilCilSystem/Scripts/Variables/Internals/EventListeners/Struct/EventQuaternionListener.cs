using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventQuaternionListener : GameEventQuaternionListener, IGameEventSetter<EventQuaternion>
    {
        [SerializeField] private EventQuaternion m_event = default;
        public override IDisposable Subscibe(Action<Quaternion> action) => m_event.Subscribe(action);

        void IGameEventSetter<EventQuaternion>.SetGameEvent(EventQuaternion gameEvent)
        {
            m_event = gameEvent;
        }
    }
}