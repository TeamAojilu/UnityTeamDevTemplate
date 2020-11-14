using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class NotificationString : StringValue, IGameEventSetter<GameEventString>
    {
        [SerializeField, HideInInspector] private GameEventString m_onValueChanged = default;

        public override string Value 
        { 
            get => base.Value;
            set
            {
                m_onValueChanged?.Publish(value);
                base.Value = value;
            }
        }

        void IGameEventSetter<GameEventString>.SetGameEvent(GameEventString gameEvent)
        {
            m_onValueChanged = gameEvent;
        }
    }
}