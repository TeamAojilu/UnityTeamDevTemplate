using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class NotificationVector2 : Vector2Value, IGameEventSetter<GameEventVector2>
    {
        [SerializeField, HideInInspector] private GameEventVector2 m_onValueChanged = default;

        public override Vector2 Value 
        { 
            get => base.Value;
            set
            {
                base.Value = value;
                m_onValueChanged?.Publish(value);
            }
        }

        void IGameEventSetter<GameEventVector2>.SetGameEvent(GameEventVector2 gameEvent)
        {
            m_onValueChanged = gameEvent;
        }
    }
}