using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(Constants.ReadonlyMenuPath + "(Vector2Int)", typeof(VariableVector2Int))]
    internal class ReadonlyVector2IntValue : ReadonlyVector2Int
    {
        [SerializeField] private VariableVector2Int m_variable = default;

        public override Vector2Int Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableVector2Int>();
        }
    }
}
