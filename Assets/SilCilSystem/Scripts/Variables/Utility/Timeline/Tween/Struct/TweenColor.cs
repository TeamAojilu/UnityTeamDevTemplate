using UnityEngine;

namespace SilCilSystem.Variables.Timeline
{
    public class TweenColor : TweenVariableAsset<Color, TweenColorBehaviour>
    {
        [SerializeField] private VariableColor m_variable = default;

        protected override Variable<Color> GetVariable()
        {
            return m_variable;
        }

        protected override Color Lerp(Color start, Color end, float t)
        {
            return Color.Lerp(start, end, t);
        }
    }

    public class TweenColorBehaviour : TweenVariableBehaviour<Color> { }
}