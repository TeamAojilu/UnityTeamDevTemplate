using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventColorListener : GameEventColorListener, IGameEventSetter<GameEventColor>
    {
        [SerializeField] private GameEventColor m_event = default;
        public override IDisposable Subscribe(Action<Color> action) => m_event.Subscribe(action);

        void IGameEventSetter<GameEventColor>.SetGameEvent(GameEventColor gameEvent)
        {
            m_event = gameEvent;
        }
    }
}