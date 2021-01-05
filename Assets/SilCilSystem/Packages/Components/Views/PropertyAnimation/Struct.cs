using SilCilSystem.Math;
using SilCilSystem.Variables;
using UnityEngine;

namespace SilCilSystem.Components.Views
{
    internal class PropertyAnimationColor : PropertyAnimation<Color, ReadonlyPropertyColor, ReadonlyColor>
    {
        public PropertyAnimationColor(ReadonlyPropertyColor property, ReadonlyPropertyFloat duration, InterpolationCurve curve) : base(property, duration, curve) { }
        protected override Color Lerp(Color start, Color end, float t) => Color.LerpUnclamped(start, end, t);
    }
    internal class ReadonlyAnimationColor : ReadonlyVariableAnimation<Color, ReadonlyColor>
    {
        public ReadonlyAnimationColor(ReadonlyColor variable, ReadonlyPropertyFloat duration, InterpolationCurve curve) : base(variable, duration, curve) { }
        protected override Color Lerp(Color start, Color end, float t) => Color.LerpUnclamped(start, end, t);
    }
}