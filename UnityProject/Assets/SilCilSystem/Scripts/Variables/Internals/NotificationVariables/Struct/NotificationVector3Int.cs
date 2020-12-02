using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class NotificationVector3Int : Vector3IntValue, IGameEventSetter<GameEventVector3Int>
    {
        [SerializeField, HideInInspector] private GameEventVector3Int m_onValueChanged = default;

        public override Vector3Int Value 
        { 
            get => base.Value;
            set
            {
                base.Value = value;
                m_onValueChanged?.Publish(value);
            }
        }

        void IGameEventSetter<GameEventVector3Int>.SetGameEvent(GameEventVector3Int gameEvent)
        {
            m_onValueChanged = gameEvent;
        }
    }
}