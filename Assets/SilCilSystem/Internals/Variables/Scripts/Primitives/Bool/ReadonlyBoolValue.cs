using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(VariablePath.ReadonlyMenuPath + "(Bool)", typeof(VariableBool))]
    internal class ReadonlyBoolValue : ReadonlyBool
    {
        [SerializeField] private VariableBool m_variable = default;

        public override bool Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is VariableBool value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}
