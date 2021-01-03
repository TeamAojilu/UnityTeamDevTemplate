using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(VariablePath.ReadonlyMenuPath + "(Vector3Int)", typeof(VariableVector3Int))]
    internal class ReadonlyVector3IntValue : ReadonlyVector3Int
    {
        [SerializeField] private VariableVector3Int m_variable = default;

        public override Vector3Int Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is VariableVector3Int value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}
