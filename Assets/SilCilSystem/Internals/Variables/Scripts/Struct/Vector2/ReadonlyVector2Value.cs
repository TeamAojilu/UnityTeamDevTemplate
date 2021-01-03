using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    internal class ReadonlyVector2Value : ReadonlyVector2
    {
        [SerializeField, HideInInspector] private VariableVector2 m_variable = default;

        public override Vector2 Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variables is VariableVector2 value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}
