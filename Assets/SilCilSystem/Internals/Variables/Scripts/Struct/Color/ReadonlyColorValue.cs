using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Readonly", Constants.ReadonlyMenuPath + "(Color)", typeof(VariableColor))]
    internal class ReadonlyColorValue : ReadonlyColor
    {
        [SerializeField, NotEditable] private VariableColor m_variable = default;

        public override Color Value => m_variable.Value;

        [OnAttached, Conditional("UNITY_EDITOR")]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableColor>();
        }
    }
}
