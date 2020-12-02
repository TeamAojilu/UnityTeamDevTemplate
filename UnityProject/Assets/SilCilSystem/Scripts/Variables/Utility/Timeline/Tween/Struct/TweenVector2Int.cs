using SilCilSystem.Math;
using UnityEngine;

namespace SilCilSystem.Variables.Timeline
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
            return m_castType.Cast(Vector2.Lerp(start, end, t));
        }
    }

    public class TweenVector2IntBehaviour : TweenVariableBehaviour<Vector2Int> { }
}