using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [Variable("Variable")]
    internal class NotificationColor : VariableColor
    {
        [SerializeField] private Color m_value = default;
        [SerializeField] private GameEventColor m_onValueChanged = default;

        public override Color Value
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
            m_onValueChanged = parent.GetSubVariable<GameEventColor>();
        }
    }
}