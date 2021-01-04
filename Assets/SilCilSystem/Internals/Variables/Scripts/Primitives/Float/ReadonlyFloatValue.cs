using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(Constants.ReadonlyMenuPath + "(Float)", typeof(VariableFloat))]
    internal class ReadonlyFloatValue : ReadonlyFloat
    {
        [SerializeField] private VariableFloat m_variable = default;

        public override float Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableFloat>();
        }
    }
}
