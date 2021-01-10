using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;
using SilCilSystem.Math;
using SilCilSystem.Variables.Generic;
using System;

namespace SilCilSystem.Internals.Variables.Converters
{
    internal abstract class VariableToBoolBase<TType, TVariable, TProperty> : ReadonlyBool
        where TType : IComparable<TType>
        where TVariable : ReadonlyVariable<TType>
        where TProperty : ReadonlyProperty<TType, TVariable>
    {
        [SerializeField] private TVariable m_variable = default;
        [SerializeField] private Comparison.CompareType m_compareType = default;
        [SerializeField] private TProperty m_compareTo = default;

        public override bool Value
        {
            get => m_variable.Value.CompareTo(m_compareTo, m_compareType);
        }

        [OnAttached, Conditional("UNITY_EDITOR")]
        protected void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<TVariable>();
        }
    }
}