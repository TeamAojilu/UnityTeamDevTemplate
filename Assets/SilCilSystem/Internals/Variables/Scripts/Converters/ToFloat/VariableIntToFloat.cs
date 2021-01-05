using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Editors;
using SilCilSystem.Math;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals.Variables.Converters
{
    [Variable("ToFloat", Constants.ConvertMenuPath + "Float (from Int)", typeof(VariableInt))]
    internal class VariableIntToFloat : VariableFloat
    {
        [SerializeField] private VariableInt m_variable = default;
        [SerializeField] private FloatToInt.CastType m_castType = default;

        public override float Value
        {
            get => m_variable.Value;
            set => m_variable?.SetValue(m_castType.Cast(value));
        }

        [OnAttached]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableInt>();
        }
    }
}