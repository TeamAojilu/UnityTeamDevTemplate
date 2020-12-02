using SilCilSystem.Variables;
using UnityEngine;

namespace SilCilSystem.Variables.Timeline
{
    public class TweenQuaternion : TweenVariableAsset<Quaternion, TweenQuaternionBehaviour>
    {
        private enum LerpType
        {
            Lerp,
            SLerp,
        }

        [SerializeField] private LerpType m_lerpType = default;
        [SerializeField] private VariableQuaternion m_variable = default;

        protected override Variable<Quaternion> GetVariable()
        {
            return m_variable;
        }

        protected override Quaternion Lerp(Quaternion start, Quaternion end, float t)
        {
            switch (m_lerpType)
            {
                default:
                case LerpType.Lerp:
                    return Quaternion.Lerp(start, end, t);
                case LerpType.SLerp:
                    return Quaternion.Slerp(start, end, t);
            }
        }
    }

    public class TweenQuaternionBehaviour : TweenVariableBehaviour<Quaternion> { }
}