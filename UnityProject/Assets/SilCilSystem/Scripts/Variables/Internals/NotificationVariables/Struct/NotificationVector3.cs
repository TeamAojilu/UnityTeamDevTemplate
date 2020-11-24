using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class NotificationVector3 : Vector3Value, IGameEventSetter<GameEventVector3>
    {
        [SerializeField, HideInInspector] private GameEventVector3 m_onValueChanged = default;

        public override Vector3 Value 
        { 
            get => base.Value;
            set
            {
                base.Value = value;
                m_onValueChanged?.Publish(value);
            }
        }

        void IGameEventSetter<GameEventVector3>.SetGameEvent(GameEventVector3 gameEvent)
        {
            m_onValueChanged = gameEvent;
        }
    }
}