using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Readonly", Constants.ReadonlyMenuPath + "(Vector2)", typeof(VariableVector2))]
    internal class ReadonlyVector2Value : ReadonlyVector2
    {
        [SerializeField, NonEditable] private VariableVector2 m_variable = default;

        public override Vector2 Value => m_variable.Value;

        [OnAttached, Conditional("UNITY_EDITOR")]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableVector2>();
        }
    }
}
