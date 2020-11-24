using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class NotificationVector2Int : Vector2IntValue, IGameEventSetter<GameEventVector2Int>
    {
        [SerializeField, HideInInspector] private GameEventVector2Int m_onValueChanged = default;

        public override Vector2Int Value 
        { 
            get => base.Value;
            set
            {
                base.Value = value;
                m_onValueChanged?.Publish(value);
            }
        }

        void IGameEventSetter<GameEventVector2Int>.SetGameEvent(GameEventVector2Int gameEvent)
        {
            m_onValueChanged = gameEvent;
        }
    }
}