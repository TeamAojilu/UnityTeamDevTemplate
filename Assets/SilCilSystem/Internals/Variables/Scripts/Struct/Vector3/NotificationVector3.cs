using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [Variable("Variable")]
    internal class NotificationVector3 : VariableVector3
    {
        [SerializeField] private Vector3 m_value = default;
        [SerializeField] private GameEventVector3 m_onValueChanged = default;

        public override Vector3 Value
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
            m_onValueChanged = parent.GetSubVariable<GameEventVector3>();
        }
    }
}