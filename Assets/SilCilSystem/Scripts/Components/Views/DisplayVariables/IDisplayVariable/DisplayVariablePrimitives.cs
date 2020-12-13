using System;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Math;

namespace SilCilSystem.Components.Views
{
    [Serializable]
    internal class DisplayVariableInt : DisplayVariableWithAnimation<int, ReadonlyInt>
    {
        [SerializeField] private FloatToInt.CastType m_castType = default;

        protected override ReadonlyVariableAnimation<int, ReadonlyInt> CreateAnimation(ReadonlyInt variable, ReadonlyPropertyFloat duration, InterpolationCurve curve)
        {
            return new ReadonlyAnimaitonInt(variable, duration, curve) { CastType = m_castType };
        }

        protected override string ToText(int value) => value.ToString(m_format);
    }

    [Serializable]
    internal class DisplayVariableFloat : DisplayVariableWithAnimation<float, ReadonlyFloat>
    {
        protected override ReadonlyVariableAnimation<float, ReadonlyFloat> CreateAnimation(ReadonlyFloat variable, ReadonlyPropertyFloat duration, InterpolationCurve curve)
        {
            return new ReadonlyAnimationFloat(variable, duration, curve);
        }

        protected override string ToText(float value) => value.ToString(m_format);
    }

    [Serializable]
    internal class DisplayVariableString : IDisplayVariable
    {
        [Header("Basic")]
        [SerializeField] private string m_key = "key"; // インスペクタのヘッダーがm_keyの内容になるようにstring.
        [SerializeField] protected ReadonlyString m_variable = default;
        public string Key => m_key;
        public bool IsBusy => false;
        public void Initialize() { }
        public string Update() => m_variable;
    }
}