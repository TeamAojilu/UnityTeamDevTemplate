using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Editors;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    [Variable("Variable")]
    internal class NotificationBool : VariableBool
    {
        [SerializeField] private bool m_value = default;
        [SerializeField] private GameEventBool m_onValueChanged = default;

        public override bool Value
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
            m_onValueChanged = parent.GetSubVariable<GameEventBool>();
        }
    }
}