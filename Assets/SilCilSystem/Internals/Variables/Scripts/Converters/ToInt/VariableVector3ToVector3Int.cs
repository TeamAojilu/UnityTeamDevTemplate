using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;
using SilCilSystem.Math;

namespace SilCilSystem.Internals.Variables.Converters
{
    [AddSubAssetMenu(Constants.ConvertMenuPath + "Vector3Int (from Vector3)", typeof(VariableVector3))]
    internal class VariableVector3ToVector3Int : VariableVector3Int
    {
        [SerializeField] private VariableVector3 m_variable = default;
        [SerializeField] private FloatToInt.CastType m_castType = default;

        public override Vector3Int Value
        {
            get => m_castType.Cast(m_variable.Value);
            set => m_variable?.SetValue(value);
        }
        
        public override void GetAssetName(ref string name) => name = $"{name}_ToInt";
        public override void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableVector3>();
        }
    }
}