using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class NotificationQuaternion : QuaternionValue, IGameEventSetter<GameEventQuaternion>
    {
        [SerializeField, HideInInspector] private GameEventQuaternion m_onValueChanged = default;

        public override Quaternion Value 
        { 
            get => base.Value;
            set
            {
                base.Value = value;
                m_onValueChanged?.Publish(value);
            }
        }

        void IGameEventSetter<GameEventQuaternion>.SetGameEvent(GameEventQuaternion gameEvent)
        {
            m_onValueChanged = gameEvent;
        }
    }
}