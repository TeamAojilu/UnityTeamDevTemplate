using SilCilSystem.Variables;
using SilCilSystem.Variables.Generic;
using UnityEngine;

namespace SilCilSystem.Timeline
{
    public class TweenFloat : TweenVariableAsset<float, TweenFloatBehaviour>
    {
        [SerializeField] private VariableFloat m_variable = default;

        protected override Variable<float> GetVariable()
        {
            return m_variable;
        }

        protected override float Lerp(float start, float end, float t)
        {
            return Mathf.LerpUnclamped(start, end, t);
        }
    }

    public class TweenFloatBehaviour : TweenVariableBehaviour<float> { }
}