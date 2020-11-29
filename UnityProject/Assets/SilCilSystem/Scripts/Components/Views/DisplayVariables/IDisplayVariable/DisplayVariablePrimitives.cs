using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Math;
using System.Collections.Generic;

namespace SilCilSystem.Components.Views
{
    [System.Serializable]
    internal class DisplayVariableInt : DisplayVariable<int>
    {
        [SerializeField] private FloatToInt.CastType m_castType = default;
        protected override int Lerp(int start, int end, float t) => m_castType.Cast(Mathf.Lerp(start, end, t));
        protected override string ToText(int value) => value.ToString(m_format);
    }

    [System.Serializable]
    internal class DisplayVariableFloat : DisplayVariable<float>
    {
        protected override float Lerp(float start, float end, float t) => Mathf.Lerp(start, end, t);
        protected override string ToText(float value) => value.ToString(m_format);
    }
}