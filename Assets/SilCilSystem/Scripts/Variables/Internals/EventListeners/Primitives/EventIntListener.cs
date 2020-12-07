using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventIntListener : GameEventIntListener, IGameEventSetter<GameEventInt>
    {
        [SerializeField] private GameEventInt m_event;
        public override IDisposable Subscribe(Action<int> action) => m_event.Subscribe(action);

        void IGameEventSetter<GameEventInt>.SetGameEvent(GameEventInt gameEvent)
        {
            m_event = gameEvent;
        }
    }
}