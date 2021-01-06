using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Variable")]
    internal class NotificationVector2Int : VariableVector2Int
    {
        [SerializeField] private Vector2Int m_value = default;
        [SerializeField] private GameEventVector2Int m_onValueChanged = default;

        public override Vector2Int Value
        {
            get => m_value;
            set
            {
                m_value = value;
                m_onValueChanged?.Publish(m_value);
            }
        }

        [OnAttached]
        private void OnAttached(VariableAsset parent)
        {
            m_onValueChanged = parent.GetSubVariable<GameEventVector2Int>();
        }
    }
}