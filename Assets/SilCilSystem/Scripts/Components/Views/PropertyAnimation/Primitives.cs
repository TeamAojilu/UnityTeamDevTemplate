using UnityEngine;
using SilCilSystem.Math;
using SilCilSystem.Variables;

namespace SilCilSystem.Components.Views
{
    internal class PropertyAnimationFloat : PropertyAnimation<float, ReadonlyPropertyFloat, ReadonlyFloat>
    {
        public PropertyAnimationFloat(ReadonlyPropertyFloat property, ReadonlyPropertyFloat duration, InterpolationCurve curve) : base(property, duration, curve) { }
        protected override float Lerp(float start, float end, float t) => Mathf.LerpUnclamped(start, end, t);
    }
    internal class ReadonlyAnimationFloat : ReadonlyVariableAnimation<float, ReadonlyFloat>
    {
        public ReadonlyAnimationFloat(ReadonlyFloat variable, ReadonlyPropertyFloat duration, InterpolationCurve curve) : base(variable, duration, curve) { }
        protected override float Lerp(float start, float end, float t) => Mathf.LerpUnclamped(start, end, t);
    }

    internal class PropertyAnimationInt : PropertyAnimation<int, ReadonlyPropertyInt, ReadonlyInt>
    {
        public FloatToInt.CastType CastType { get; set; }
        public PropertyAnimationInt(ReadonlyPropertyInt property, ReadonlyPropertyFloat duration, InterpolationCurve curve) : base(property, duration, curve) { }
        protected override int Lerp(int start, int end, float t) => CastType.Cast(Mathf.LerpUnclamped(start, end, t));
    }
    internal class ReadonlyAnimaitonInt : ReadonlyVariableAnimation<int, ReadonlyInt>
    {
        public FloatToInt.CastType CastType { get; set; }
        public ReadonlyAnimaitonInt(ReadonlyInt variable, ReadonlyPropertyFloat duration, InterpolationCurve curve) : base(variable, duration, curve) { }
        protected override int Lerp(int start, int end, float t) => CastType.Cast(Mathf.LerpUnclamped(start, end, t));
    }
}