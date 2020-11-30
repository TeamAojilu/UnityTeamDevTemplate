using System;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Math;

namespace SilCilSystem.Components.Views
{
    [Serializable]
    internal class DisplayVariableInt : DisplayVariable<int, ReadonlyInt>
    {
        [SerializeField] private FloatToInt.CastType m_castType = default;
        protected override int Lerp(int start, int end, float t) => m_castType.Cast(Mathf.Lerp(start, end, t));
        protected override string ToText(int value) => value.ToString(m_format);
    }

    [Serializable]
    internal class DisplayVariableFloat : DisplayVariable<float, ReadonlyFloat>
    {
        protected override float Lerp(float start, float end, float t) => Mathf.Lerp(start, end, t);
        protected override string ToText(float value) => value.ToString(m_format);
    }
}