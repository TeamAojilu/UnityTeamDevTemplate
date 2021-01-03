using UnityEngine;
using System.Collections.Generic;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;
using SilCilSystem.Math;

namespace SilCilSystem.Internals.Variables.Converters
{
    [AddSubAssetMenu(VariablePath.ConvertMenuPath + "Float (from Int)", typeof(VariableInt))]
    internal class VariableIntToFloat : VariableFloat
    {
        [SerializeField] private VariableInt m_variable = default;
        [SerializeField] private FloatToInt.CastType m_castType = default;

        public override float Value
        {
            get => m_variable.Value;
            set => m_variable?.SetValue(m_castType.Cast(value));
        }
        
        public override void GetAssetName(ref string name) => name = $"{name}_ToFloat";

        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is VariableInt value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}