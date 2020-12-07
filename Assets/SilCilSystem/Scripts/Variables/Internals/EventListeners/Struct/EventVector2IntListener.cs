using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventVector2IntListener : GameEventVector2IntListener, IGameEventSetter<GameEventVector2Int>
    {
        [SerializeField] private GameEventVector2Int m_event = default;
        public override IDisposable Subscribe(Action<Vector2Int> action) => m_event.Subscribe(action);

        void IGameEventSetter<GameEventVector2Int>.SetGameEvent(GameEventVector2Int gameEvent)
        {
            m_event = gameEvent;
        }
    }
}