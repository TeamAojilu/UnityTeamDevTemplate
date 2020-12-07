using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventVector2Listener : GameEventVector2Listener, IGameEventSetter<GameEventVector2>
    {
        [SerializeField] private GameEventVector2 m_event = default;
        public override IDisposable Subscribe(Action<Vector2> action) => m_event.Subscribe(action);

        void IGameEventSetter<GameEventVector2>.SetGameEvent(GameEventVector2 gameEvent)
        {
            m_event = gameEvent;
        }
    }
}