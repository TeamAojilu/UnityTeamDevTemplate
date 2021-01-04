using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(Constants.ReadonlyMenuPath + "(Vector2)", typeof(VariableVector2))]
    internal class ReadonlyVector2Value : ReadonlyVector2
    {
        [SerializeField] private VariableVector2 m_variable = default;

        public override Vector2 Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableVector2>();
        }
    }
}
