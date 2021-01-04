using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(Constants.ReadonlyMenuPath + "(Int)", typeof(VariableInt))]
    internal class ReadonlyIntValue : ReadonlyInt
    {
        [SerializeField] private VariableInt m_variable = default;

        public override int Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableInt>();
        }
    }
}
