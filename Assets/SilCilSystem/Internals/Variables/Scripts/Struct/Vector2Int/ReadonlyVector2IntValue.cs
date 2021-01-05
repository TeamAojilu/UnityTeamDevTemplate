using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [Variable("Readonly", Constants.ReadonlyMenuPath + "(Vector2Int)", typeof(VariableVector2Int))]
    internal class ReadonlyVector2IntValue : ReadonlyVector2Int
    {
        [SerializeField] private VariableVector2Int m_variable = default;

        public override Vector2Int Value => m_variable;

        [OnAttached]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableVector2Int>();
        }
    }
}
