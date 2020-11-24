using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventVector3Listener : GameEventVector3Listener, IGameEventSetter<EventVector3>
    {
        [SerializeField] private EventVector3 m_event = default;
        public override IDisposable Subscibe(Action<Vector3> action) => m_event.Subscribe(action);

        void IGameEventSetter<EventVector3>.SetGameEvent(EventVector3 gameEvent)
        {
            m_event = gameEvent;
        }
    }
}