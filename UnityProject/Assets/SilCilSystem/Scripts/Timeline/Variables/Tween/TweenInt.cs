using SilCilSystem.Variables;
using UnityEngine;

namespace SilCilSystem.Timeline
{
    public class TweenInt : TweenVariableAsset<int, TweenIntBehaviour>
    {
        private enum CastType
        {
            Simple,
            FloorToInt,
            CeilToInt,
            RoundToInt,
        }

        [SerializeField] private CastType m_castType = default;
        [SerializeField] private VariableInt m_variable = default;

        protected override Variable<int> GetVariable()
        {
            return m_variable;
        }

        protected override int Lerp(int start, int end, float t)
        {
            float value = Mathf.Lerp(start, end, t);
            return Cast(value);
        }

        private int Cast(float value)
        {
            switch (m_castType)
            {
                default:
                case CastType.Simple:
                    return (int)value;
                case CastType.FloorToInt:
                    return Mathf.FloorToInt(value);
                case CastType.CeilToInt:
                    return Mathf.CeilToInt(value);
                case CastType.RoundToInt:
                    return Mathf.RoundToInt(value);
            }
        }
    }

    public class TweenIntBehaviour : TweenVariableBehaviour<int> { }
}