using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Math;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables.Converters
{
    [Variable("ToInt", Constants.ConvertMenuPath + "Int (from Float)", typeof(VariableFloat))]
    internal class VariableFloatToInt : VariableInt
    {
        [SerializeField, NotEditable] private VariableFloat m_variable = default;
        [SerializeField] private FloatToInt.CastType m_castType = default;

        public override int Value
        {
            get => m_castType.Cast(m_variable.Value);
            set => m_variable.Value = value;
        }
        
        [OnAttached, Conditional("UNITY_EDITOR")]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableFloat>();
        }
    }
}