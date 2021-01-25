using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Math;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables.Converters
{
    [Variable("ToFloat", Constants.ConvertMenuPath + "Vector3 (from Vector3Int)", typeof(VariableVector3Int))]
    internal class VariableVector3IntToVector3 : VariableVector3
    {
        [SerializeField, NonEditable] private VariableVector3Int m_variable = default;
        [SerializeField] private FloatToInt.CastType m_castType = default;

        public override Vector3 Value
        {
            get => m_variable.Value;
            set => m_variable.Value = m_castType.Cast(value);
        }

        [OnAttached, Conditional("UNITY_EDITOR")]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableVector3Int>();
        }
    }
}