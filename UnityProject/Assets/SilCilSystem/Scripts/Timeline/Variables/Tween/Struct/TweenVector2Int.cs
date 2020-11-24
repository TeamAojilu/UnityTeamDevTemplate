using SilCilSystem.Variables;
using SilCilSystem.Math;
using UnityEngine;

namespace SilCilSystem.Timeline
{
    public class TweenVector2Int : TweenVariableAsset<Vector2Int, TweenVector2IntBehaviour>
    {
        [SerializeField] private FloatToInt.CastType m_castType = default;
        [SerializeField] private VariableVector2Int m_variable = default;

        protected override Variable<Vector2Int> GetVariable()
        {
            return m_variable;
        }

        protected override Vector2Int Lerp(Vector2Int start, Vector2Int end, float t)
        {
            var vec = Vector2.Lerp(start, end, t);
            return new Vector2Int(m_castType.Cast(vec.x), m_castType.Cast(vec.y));
        }
    }

    public class TweenVector2IntBehaviour : TweenVariableBehaviour<Vector2Int> { }
}