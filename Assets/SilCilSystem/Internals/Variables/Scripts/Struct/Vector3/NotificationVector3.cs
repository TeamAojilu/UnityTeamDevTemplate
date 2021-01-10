using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Variable")]
    internal class NotificationVector3 : VariableVector3
    {
        [SerializeField] private Vector3 m_value = default;
        [SerializeField, NotEditable] private GameEventVector3 m_onValueChanged = default;

        public override Vector3 Value
        {
            get => m_value;
            set
            {
                m_value = value;
                m_onValueChanged?.Publish(m_value);
            }
        }

        [OnAttached, Conditional("UNITY_EDITOR")]
        private void OnAttached(VariableAsset parent)
        {
            m_onValueChanged = parent.GetSubVariable<GameEventVector3>();
        }
    }
}