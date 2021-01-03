using UnityEngine;
using System.Collections.Generic;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;
using SilCilSystem.Math;

namespace SilCilSystem.Internals.Variables.Converters
{
    [AddSubAssetMenu(VariablePath.ConvertMenuPath + "Vector3Int (from Vector3)", typeof(VariableVector3))]
    internal class VariableVector3ToVector3Int : VariableVector3Int
    {
        [SerializeField] private VariableVector3 m_variable = default;
        [SerializeField] private FloatToInt.CastType m_castType = default;

        public override Vector3Int Value
        {
            get => m_castType.Cast(m_variable.Value);
            set => m_variable?.SetValue(value);
        }
        
        public override void GetAssetName(ref string name) => name = $"{name}_ToInt";

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