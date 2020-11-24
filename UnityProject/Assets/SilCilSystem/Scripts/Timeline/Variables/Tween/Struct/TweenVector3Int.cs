using SilCilSystem.Variables;
using SilCilSystem.Math;
using UnityEngine;

namespace SilCilSystem.Timeline
{
    public class TweenVector3Int : TweenVariableAsset<Vector3Int, TweenVector3IntBehaviour>
    {
        private enum LerpType
        {
            Lerp,
            SLerp,
        }

        [SerializeField] private LerpType m_lerpType = default;
        [SerializeField] private FloatToInt.CastType m_castType = default;
        [SerializeField] private VariableVector3Int m_variable = default;

        protected override Variable<Vector3Int> GetVariable()
        {
            return m_variable;
        }

        protected override Vector3Int Lerp(Vector3Int start, Vector3Int end, float t)
        {
            switch (m_lerpType)
            {
                default:
                case LerpType.Lerp:
                    return Vector3ToInt(Vector3.Lerp(start, end, t));
                case LerpType.SLerp:
                    return Vector3ToInt(Vector3.Slerp(start, end, t));
            }
        }

        private Vector3Int Vector3ToInt(Vector3 vec)
        {
            return new Vector3Int(m_castType.Cast(vec.x), m_castType.Cast(vec.y), m_castType.Cast(vec.z));
        }
    }

    public class TweenVector3IntBehaviour : TweenVariableBehaviour<Vector3Int> { }
}