using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(VariablePath.ReadonlyMenuPath + "(Vector3)", typeof(VariableVector3))]
    internal class ReadonlyVector3Value : ReadonlyVector3
    {
        [SerializeField] private VariableVector3 m_variable = default;

        public override Vector3 Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is VariableVector3 value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}
