using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventVector2IntListener : GameEventVector2IntListener, IGameEventSetter<EventVector2Int>
    {
        [SerializeField] private EventVector2Int m_event = default;
        public override IDisposable Subscibe(Action<Vector2Int> action) => m_event.Subscribe(action);

        void IGameEventSetter<EventVector2Int>.SetGameEvent(EventVector2Int gameEvent)
        {
            m_event = gameEvent;
        }
    }
}