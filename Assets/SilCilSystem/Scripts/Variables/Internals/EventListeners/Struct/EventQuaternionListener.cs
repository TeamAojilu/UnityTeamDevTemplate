using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventQuaternionListener : GameEventQuaternionListener, IGameEventSetter<GameEventQuaternion>
    {
        [SerializeField] private GameEventQuaternion m_event = default;
        public override IDisposable Subscribe(Action<Quaternion> action) => m_event.Subscribe(action);

        void IGameEventSetter<GameEventQuaternion>.SetGameEvent(GameEventQuaternion gameEvent)
        {
            m_event = gameEvent;
        }
    }
}