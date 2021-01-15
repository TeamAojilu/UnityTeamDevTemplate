using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Readonly", Constants.ReadonlyMenuPath + "(Bool)", typeof(VariableBool))]
    internal class ReadonlyBoolValue : ReadonlyBool
    {
        [SerializeField, NonEditable] private VariableBool m_variable = default;

        public override bool Value => m_variable;

        [OnAttached, Conditional("UNITY_EDITOR")]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableBool>();
        }
    }
}
