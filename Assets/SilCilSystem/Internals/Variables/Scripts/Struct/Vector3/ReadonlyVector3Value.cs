using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    internal class ReadonlyVector3Value : ReadonlyVector3
    {
        [SerializeField, HideInInspector] private VariableVector3 m_variable = default;

        public override Vector3 Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variables is VariableVector3 value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}
