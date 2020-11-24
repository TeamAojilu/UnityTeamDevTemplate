using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class NotificationBool : BoolValue, IGameEventSetter<GameEventBool>
    {
        [SerializeField, HideInInspector] private GameEventBool m_onValueChanged = default;

        public override bool Value 
        { 
            get => base.Value;
            set
            {
                base.Value = value;
                m_onValueChanged?.Publish(value);
            }
        }

        void IGameEventSetter<GameEventBool>.SetGameEvent(GameEventBool gameEvent)
        {
            m_onValueChanged = gameEvent;
        }
    }
}