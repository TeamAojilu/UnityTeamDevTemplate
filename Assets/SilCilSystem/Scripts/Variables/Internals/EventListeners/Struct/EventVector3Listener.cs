using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventVector3Listener : GameEventVector3Listener, IGameEventSetter<GameEventVector3>
    {
        [SerializeField] private GameEventVector3 m_event = default;
        public override IDisposable Subscribe(Action<Vector3> action) => m_event.Subscribe(action);

        void IGameEventSetter<GameEventVector3>.SetGameEvent(GameEventVector3 gameEvent)
        {
            m_event = gameEvent;
        }
    }
}