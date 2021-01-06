using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Variable")]
    internal class NotificationQuaternion : VariableQuaternion
    {
        [SerializeField] private Quaternion m_value = default;
        [SerializeField] private GameEventQuaternion m_onValueChanged = default;

        public override Quaternion Value
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
            m_onValueChanged = parent.GetSubVariable<GameEventQuaternion>();
        }
    }
}