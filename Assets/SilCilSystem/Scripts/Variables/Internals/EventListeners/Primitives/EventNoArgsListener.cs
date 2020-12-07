using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventNoArgsListener : GameEventListener, IGameEventSetter<GameEvent>
    {
        [SerializeField] private GameEvent m_event = default;

        public override IDisposable Subscribe(Action action) => m_event.Subscribe(action);

        void IGameEventSetter<GameEvent>.SetGameEvent(GameEvent gameEvent)
        {
            m_event = gameEvent;
        }
    }
}