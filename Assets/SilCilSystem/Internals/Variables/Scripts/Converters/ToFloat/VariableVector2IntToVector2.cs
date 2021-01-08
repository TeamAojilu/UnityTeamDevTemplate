using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Math;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables.Converters
{
    [Variable("ToFloat", Constants.ConvertMenuPath + "Vector2 (from Vector2Int)", typeof(VariableVector2Int))]
    internal class VariableVector2IntToVector2 : VariableVector2
    {
        [SerializeField] private VariableVector2Int m_variable = default;
        [SerializeField] private FloatToInt.CastType m_castType = default;

        public override Vector2 Value
        {
            get => m_variable.Value;
            set => m_variable.Value = m_castType.Cast(value);
        }
        
        [OnAttached, Conditional("UNITY_EDITOR")]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableVector2Int>();
        }
    }
}