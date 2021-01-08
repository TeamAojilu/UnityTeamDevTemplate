using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Readonly", Constants.ReadonlyMenuPath + "(Float)", typeof(VariableFloat))]
    internal class ReadonlyFloatValue : ReadonlyFloat
    {
        [SerializeField] private VariableFloat m_variable = default;

        public override float Value => m_variable;

        [OnAttached, Conditional("UNITY_EDITOR")]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableFloat>();
        }
    }
}
