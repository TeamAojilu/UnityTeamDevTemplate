using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;
using SilCilSystem.Math;

namespace SilCilSystem.Internals.Variables.Converters
{
    [AddSubAssetMenu(Constants.ConvertMenuPath + "Vector2 (from Vector2Int)", typeof(VariableVector2Int))]
    internal class VariableVector2IntToVector2 : VariableVector2
    {
        [SerializeField] private VariableVector2Int m_variable = default;
        [SerializeField] private FloatToInt.CastType m_castType = default;

        public override Vector2 Value
        {
            get => m_variable.Value;
            set => m_variable?.SetValue(m_castType.Cast(value));
        }
        
        public override void GetAssetName(ref string name) => name = $"{name}_ToFloat";
        public override void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableVector2Int>();
        }
    }
}