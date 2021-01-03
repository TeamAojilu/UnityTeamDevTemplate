using UnityEngine;
using System.Collections.Generic;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;
using SilCilSystem.Math;

namespace SilCilSystem.Internals.Variables.Converters
{
    [AddSubAssetMenu(VariablePath.ConvertMenuPath + "Vector2Int (from Vector2)", typeof(VariableVector2))]
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

        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is VariableVector2 value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}