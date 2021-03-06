﻿using SilCilSystem.Math;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Generic;
using UnityEngine;

namespace SilCilSystem.Timeline
{
    public class TweenInt : TweenVariableAsset<int, TweenIntBehaviour>
    {
        [SerializeField] private FloatToInt.CastType m_castType = default;
        [SerializeField] private VariableInt m_variable = default;

        protected override Variable<int> GetVariable()
        {
            return m_variable;
        }

        protected override int Lerp(int start, int end, float t)
        {
            float value = Mathf.LerpUnclamped(start, end, t);
            return m_castType.Cast(value);
        }
    }

    public class TweenIntBehaviour : TweenVariableBehaviour<int> { }
}