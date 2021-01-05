using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;
using SilCilSystem.Math;

namespace SilCilSystem.Internals.Variables.Converters
{
    [Variable("ToInt", Constants.ConvertMenuPath + "Int (from Float)", typeof(VariableFloat))]
    internal class VariableFloatToInt : VariableInt
    {
        [SerializeField] private VariableFloat m_variable = default;
        [SerializeField] private FloatToInt.CastType m_castType = default;

        public override int Value
        {
            get => m_castType.Cast(m_variable.Value);
            set => m_variable?.SetValue(value);
        }
        
        [OnAttached]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableFloat>();
        }
    }
}