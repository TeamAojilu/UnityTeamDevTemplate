using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    internal class ReadonlyFloatValue : ReadonlyFloat
    {
        [SerializeField, HideInInspector] private VariableFloat m_variable = default;

        public override float Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variables is VariableFloat value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}
