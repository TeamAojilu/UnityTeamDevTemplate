using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;
using SilCilSystem.Math;

namespace SilCilSystem.Internals.Variables.Converters
{
    [AddSubAssetMenu(Constants.ConvertMenuPath + "Vector2Int (from Vector2)", typeof(VariableVector2))]
    internal class VariableVector2ToVector2Int : VariableVector2Int
    {
        [SerializeField] private VariableVector2 m_variable = default;
        [SerializeField] private FloatToInt.CastType m_castType = default;

        public override Vector2Int Value
        {
            get => m_castType.Cast(m_variable.Value);
            set => m_variable?.SetValue(value);
        }
        
        public override void GetAssetName(ref string name) => name = $"{name}_ToInt";
        public override void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableVector2>();
        }
    }
}