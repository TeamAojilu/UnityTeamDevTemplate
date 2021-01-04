using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;
using SilCilSystem.Math;

namespace SilCilSystem.Internals.Variables.Converters
{
    [AddSubAssetMenu(Constants.ConvertMenuPath + "Vector3 (from Vector3Int)", typeof(VariableVector3Int))]
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
        public override void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableVector3Int>();
        }
    }
}