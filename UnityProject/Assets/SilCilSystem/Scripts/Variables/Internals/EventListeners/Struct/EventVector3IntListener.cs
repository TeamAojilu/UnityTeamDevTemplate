using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventVector3IntListener : GameEventVector3IntListener, IGameEventSetter<EventVector3Int>
    {
        [SerializeField] private EventVector3Int m_event = default;
        public override IDisposable Subscibe(Action<Vector3Int> action) => m_event.Subscribe(action);

        void IGameEventSetter<EventVector3Int>.SetGameEvent(EventVector3Int gameEvent)
        {
            m_event = gameEvent;
        }
    }
}