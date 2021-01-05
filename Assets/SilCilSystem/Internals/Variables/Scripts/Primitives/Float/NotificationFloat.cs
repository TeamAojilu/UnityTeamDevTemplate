using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [Variable("Variable")]
    internal class NotificationFloat : VariableFloat
    {
        [SerializeField] private float m_value = default;
        [SerializeField] private GameEventFloat m_onValueChanged = default;

        public override float Value
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
            m_onValueChanged = parent.GetSubVariable<GameEventFloat>();
        }
    }
}