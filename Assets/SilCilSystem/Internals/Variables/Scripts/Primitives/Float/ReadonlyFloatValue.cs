using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(VariablePath.ReadonlyMenuPath + "(Float)", typeof(VariableFloat))]
    internal class ReadonlyFloatValue : ReadonlyFloat
    {
        [SerializeField] private VariableFloat m_variable = default;

        public override float Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is VariableFloat value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}
