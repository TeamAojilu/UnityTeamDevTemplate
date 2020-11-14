using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class NotificationFloat : FloatValue, IGameEventSetter<GameEventFloat>
    {
        [SerializeField, HideInInspector] private GameEventFloat m_onValueChanged = default;

        public override float Value 
        { 
            get => base.Value;
            set
            {
                base.Value = value;
                m_onValueChanged?.Publish(value);
            }
        }

        void IGameEventSetter<GameEventFloat>.SetGameEvent(GameEventFloat gameEvent)
        {
            m_onValueChanged = gameEvent;
        }
    }
}