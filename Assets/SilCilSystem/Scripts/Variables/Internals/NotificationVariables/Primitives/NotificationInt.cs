using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class NotificationInt : IntValue, IGameEventSetter<GameEventInt>
    {
        [SerializeField, HideInInspector] private GameEventInt m_onValueChanged = default;

        public override int Value 
        { 
            get => base.Value;
            set
            {
                m_onValueChanged?.Publish(value);
                base.Value = value;
            }
        }

        void IGameEventSetter<GameEventInt>.SetGameEvent(GameEventInt gameEvent)
        {
            m_onValueChanged = gameEvent;
        }
    }
}