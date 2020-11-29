using UnityEngine;

namespace SilCilSystem.Variables.Timeline
{
    public class TweenVector2 : TweenVariableAsset<Vector2, TweenVector2Behaviour>
    {
        [SerializeField] private VariableVector2 m_variable = default;

        protected override Variable<Vector2> GetVariable()
        {
            return m_variable;
        }

        protected override Vector2 Lerp(Vector2 start, Vector2 end, float t)
        {
            return Vector2.Lerp(start, end, t);
        }
    }

    public class TweenVector2Behaviour : TweenVariableBehaviour<Vector2> { }
}