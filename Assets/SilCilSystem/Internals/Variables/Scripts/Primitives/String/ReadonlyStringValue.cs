using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(VariablePath.ReadonlyMenuPath + "(String)", typeof(VariableString))]
    internal class ReadonlyStringValue : ReadonlyString
    {
        [SerializeField] private VariableString m_variable = default;

        public override string Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is VariableString value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}
