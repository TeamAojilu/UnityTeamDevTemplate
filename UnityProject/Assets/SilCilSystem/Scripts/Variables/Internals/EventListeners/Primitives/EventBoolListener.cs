using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventBoolListener : GameEventBoolListener, IGameEventSetter<GameEventBool>
    {
        [SerializeField] private GameEventBool m_event = default;
        public override IDisposable Subscribe(Action<bool> action) => m_event.Subscribe(action);

        void IGameEventSetter<GameEventBool>.SetGameEvent(GameEventBool gameEvent)
        {
            m_event = gameEvent;
        }
    }
}