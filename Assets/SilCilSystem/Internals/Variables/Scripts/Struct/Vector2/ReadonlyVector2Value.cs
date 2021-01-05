using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [Variable("Readonly", Constants.ReadonlyMenuPath + "(Vector2)", typeof(VariableVector2))]
    internal class ReadonlyVector2Value : ReadonlyVector2
    {
        [SerializeField] private VariableVector2 m_variable = default;

        public override Vector2 Value => m_variable;

        [OnAttached]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableVector2>();
        }
    }
}
