using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Readonly", Constants.ReadonlyMenuPath + "(Quaternion)", typeof(VariableQuaternion))]
    internal class ReadonlyQuaternionValue : ReadonlyQuaternion
    {
        [SerializeField, NonEditable] private VariableQuaternion m_variable = default;

        public override Quaternion Value => m_variable.Value;

        [OnAttached, Conditional("UNITY_EDITOR")]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableQuaternion>();
        }
    }
}
