using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    internal class ReadonlyVector3IntValue : ReadonlyVector3Int
    {
        [SerializeField, HideInInspector] private VariableVector3Int m_variable = default;

        public override Vector3Int Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variables is VariableVector3Int value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}
