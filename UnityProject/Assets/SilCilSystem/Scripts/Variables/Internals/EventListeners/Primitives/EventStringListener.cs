using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventStringListener : GameEventStringListener, IGameEventSetter<GameEventString>
    {
        [SerializeField] private GameEventString m_event = default;
        public override IDisposable Subscribe(Action<string> action) => m_event.Subscribe(action);

        void IGameEventSetter<GameEventString>.SetGameEvent(GameEventString gameEvent)
        {
            m_event = gameEvent;
        }
    }
}