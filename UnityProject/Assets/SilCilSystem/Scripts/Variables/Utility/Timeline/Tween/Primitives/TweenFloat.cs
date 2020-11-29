using UnityEngine;

namespace SilCilSystem.Variables.Timeline
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
            return Mathf.Lerp(start, end, t);
        }
    }

    public class TweenFloatBehaviour : TweenVariableBehaviour<float> { }
}