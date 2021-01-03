using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(VariablePath.ReadonlyMenuPath + "(Vector2)", typeof(VariableVector2))]
    internal class ReadonlyVector2Value : ReadonlyVector2
    {
        [SerializeField] private VariableVector2 m_variable = default;

        public override Vector2 Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is VariableVector2 value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}
