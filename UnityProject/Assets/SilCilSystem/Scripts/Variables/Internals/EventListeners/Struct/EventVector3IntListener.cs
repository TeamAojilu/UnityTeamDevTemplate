using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventVector3IntListener : GameEventVector3IntListener, IGameEventSetter<GameEventVector3Int>
    {
        [SerializeField] private GameEventVector3Int m_event = default;
        public override IDisposable Subscribe(Action<Vector3Int> action) => m_event.Subscribe(action);

        void IGameEventSetter<GameEventVector3Int>.SetGameEvent(GameEventVector3Int gameEvent)
        {
            m_event = gameEvent;
        }
    }
}