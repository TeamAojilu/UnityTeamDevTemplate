using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class NotificationColor : ColorValue, IGameEventSetter<GameEventColor>
    {
        [SerializeField, HideInInspector] private GameEventColor m_onValueChanged = default;

        public override Color Value 
        { 
            get => base.Value;
            set
            {
                base.Value = value;
                m_onValueChanged?.Publish(value);
            }
        }

        void IGameEventSetter<GameEventColor>.SetGameEvent(GameEventColor gameEvent)
        {
            m_onValueChanged = gameEvent;
        }
    }
}