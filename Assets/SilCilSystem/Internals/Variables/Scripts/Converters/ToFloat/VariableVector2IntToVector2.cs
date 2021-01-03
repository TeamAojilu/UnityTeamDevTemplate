﻿using UnityEngine;
using System.Collections.Generic;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;
using SilCilSystem.Math;

namespace SilCilSystem.Internals.Variables.Converters
{
    [AddSubAssetMenu(VariablePath.ConvertMenuPath + "Vector2 (from Vector2Int)", typeof(VariableVector2Int))]
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

        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is VariableVector2Int value)
                {
                    m_variable = value;
                    return;
                }
            }
        }
    }
}