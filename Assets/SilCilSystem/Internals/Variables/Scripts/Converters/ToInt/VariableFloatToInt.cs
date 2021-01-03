using UnityEngine;
using System.Collections.Generic;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;
using SilCilSystem.Math;

namespace SilCilSystem.Internals.Variables.Converters
{
    [AddSubAssetMenu(VariablePath.ConvertMenuPath + "Int (from Float)", typeof(VariableFloat))]
    internal class VariableFloatToInt : VariableInt
    {
        [SerializeField] private VariableFloat m_variable = default;
        [SerializeField] private FloatToInt.CastType m_castType = default;

        public override int Value
        {
            get => m_castType.Cast(m_variable.Value);
            set => m_variable?.SetValue(value);
        }
        
        public override void GetAssetName(ref string name) => name = $"{name}_ToInt";

        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is VariableFloat value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}