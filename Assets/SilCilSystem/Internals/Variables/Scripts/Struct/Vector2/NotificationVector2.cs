using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [Variable("Variable")]
    internal class NotificationVector2 : VariableVector2
    {
        [SerializeField] private Vector2 m_value = default;
        [SerializeField] private GameEventVector2 m_onValueChanged = default;

        public override Vector2 Value
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
            m_onValueChanged = parent.GetSubVariable<GameEventVector2>();
        }
    }
}