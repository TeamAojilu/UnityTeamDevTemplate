using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    internal class ReadonlyQuaternionValue : ReadonlyQuaternion
    {
        [SerializeField, HideInInspector] private VariableQuaternion m_variable = default;

        public override Quaternion Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variables is VariableQuaternion value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}
