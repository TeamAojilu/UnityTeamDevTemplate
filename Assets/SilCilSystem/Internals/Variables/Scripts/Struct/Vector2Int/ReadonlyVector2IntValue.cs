using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(VariablePath.ReadonlyMenuPath + "(Vector2Int)", typeof(VariableVector2Int))]
    internal class ReadonlyVector2IntValue : ReadonlyVector2Int
    {
        [SerializeField] private VariableVector2Int m_variable = default;

        public override Vector2Int Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is VariableVector2Int value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}
