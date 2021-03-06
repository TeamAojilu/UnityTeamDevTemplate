﻿using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Math;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables.Converters
{
    [Variable("ToInt", Constants.ConvertMenuPath + "Vector2Int (from Vector2)", typeof(VariableVector2))]
    internal class VariableVector2ToVector2Int : VariableVector2Int
    {
        [SerializeField, NonEditable] private VariableVector2 m_variable = default;
        [SerializeField] private FloatToInt.CastType m_castType = default;

        public override Vector2Int Value
        {
            get => m_castType.Cast(m_variable.Value);
            set => m_variable.Value = value;
        }
        
        [OnAttached, Conditional("UNITY_EDITOR")]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableVector2>();
        }
    }
}