using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventFloatListener : GameEventFloatListener, IGameEventSetter<GameEventFloat>
    {
        [SerializeField] private GameEventFloat m_event = default;
        public override IDisposable Subscribe(Action<float> action) => m_event.Subscribe(action);

        void IGameEventSetter<GameEventFloat>.SetGameEvent(GameEventFloat gameEvent)
        {
            m_event = gameEvent;
        }
    }
}