using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Math;
using SilCilSystem.Variables;

namespace SilCilSystem.Components.Views
{
    internal abstract class PropertyAnimation<T, TProperty, TVariable> : ValueAnimation<T>
            where TProperty : ReadonlyProperty<T, TVariable>
            where TVariable : ReadonlyVariable<T>
    {
        private readonly TProperty m_property = default;
        
        protected PropertyAnimation(TProperty property, ReadonlyPropertyFloat duration, InterpolationCurve curve) : base(duration, curve)
        {
            m_property = property;
        }

        protected override T TargetValue => m_property;
    }

    internal abstract class ReadonlyVariableAnimation<T, TVariable> : ValueAnimation<T>
            where TVariable : ReadonlyVariable<T>
    {
        private readonly TVariable m_variable = default;
        
        protected ReadonlyVariableAnimation(TVariable variable, ReadonlyPropertyFloat duration, InterpolationCurve curve) : base(duration, curve)
        {
            m_variable = variable;
        }

        protected override T TargetValue => m_variable;
    }

    internal abstract class ValueAnimation<T>
    {
        private readonly ReadonlyPropertyFloat m_duration = default;
        private readonly InterpolationCurve m_curve = default;

        private T m_startValue = default;
        private T m_currentValue = default;
        private T m_targetValue = default;
        private float m_timer = 0f;

        protected ValueAnimation(ReadonlyPropertyFloat duration, InterpolationCurve curve)
        {
            m_duration = duration;
            m_curve = curve;
        }

        public void Initialize(T startValue)
        {
            m_startValue = m_currentValue = startValue;
            m_targetValue = TargetValue;
            m_timer = 0f;
        }

        public bool IsBusy() => !EqualityComparer<T>.Default.Equals(m_currentValue, m_targetValue) || m_timer < m_duration;

        public T Update()
        {
            if (!EqualityComparer<T>.Default.Equals(TargetValue, m_targetValue))
            {
                Initialize(m_currentValue);
            }

            m_timer += Time.deltaTime;
            m_currentValue = (m_timer >= m_duration) ? TargetValue : Lerp(m_startValue, m_targetValue, m_curve.Evaluate(Mathf.Clamp01(m_timer / m_duration)));
            return m_currentValue;
        }

        protected abstract T TargetValue { get; }
        protected abstract T Lerp(T start, T end, float t);
    }
}