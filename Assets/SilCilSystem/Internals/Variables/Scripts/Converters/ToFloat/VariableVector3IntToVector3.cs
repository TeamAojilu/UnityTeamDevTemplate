using UnityEngine;
using System.Collections.Generic;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;
using SilCilSystem.Math;

namespace SilCilSystem.Internals.Variables.Converters
{
    [AddSubAssetMenu(VariablePath.ConvertMenuPath + "Vector3 (from Vector3Int)", typeof(VariableVector3Int))]
    internal class VariableVector3IntToVector3 : VariableVector3
    {
        [SerializeField] private VariableVector3Int m_variable = default;
        [SerializeField] private FloatToInt.CastType m_castType = default;

        public override Vector3 Value
        {
            get => m_variable.Value;
            set => m_variable?.SetValue(m_castType.Cast(value));
        }
        
        public override void GetAssetName(ref string name) => name = $"{name}_ToFloat";

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