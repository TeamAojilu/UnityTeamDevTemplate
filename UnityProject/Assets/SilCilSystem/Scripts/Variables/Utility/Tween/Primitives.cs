using System;
using System.Collections;
using UnityEngine;
using static SilCilSystem.Math.CurveTypeDefinition;
using static SilCilSystem.Math.FloatToInt;

namespace SilCilSystem.Variables
{
    public static class TweenPrimitivesExtensions
    {
        #region float
        public static IEnumerator Tween(this Variable<float> variable, float start, float end, float time, Func<float, float> curve)
            => variable.Tween(start, end, time, Mathf.Lerp, curve);
        public static IEnumerator Tween(this Variable<float> variable, float start, float end, float time, CurveType curveType = default)
            => variable.Tween(start, end, time, Mathf.Lerp, curveType.GetCurve());
        public static IEnumerator Tween(this Variable<float> variable, float end, float time, Func<float, float> curve)
            => variable.Tween(variable.Value, end, time, Mathf.Lerp, curve);
        public static IEnumerator Tween(this Variable<float> variable, float end, float time, CurveType curveType = default)
            => variable.Tween(variable.Value, end, time, Mathf.Lerp, curveType.GetCurve());
        #endregion

        #region int
        public static IEnumerator Tween(this Variable<int> variable, int start, int end, float time, Func<float, float> curve, CastType castType = default)
            => variable.Tween(start, end, time, (s, e, t) => castType.Cast(Mathf.Lerp(s, e, t)), curve);
        public static IEnumerator Tween(this Variable<int> variable, int start, int end, float time, CurveType curveType = default, CastType castType = default)
            => variable.Tween(start, end, time, (s, e, t) => castType.Cast(Mathf.Lerp(s, e, t)), curveType.GetCurve());
        public static IEnumerator Tween(this Variable<int> variable, int end, float time, Func<float, float> curve, CastType castType = default)
            => variable.Tween(variable.Value, end, time, (s, e, t) => castType.Cast(Mathf.Lerp(s, e, t)), curve);
        public static IEnumerator Tween(this Variable<int> variable, int end, float time, CurveType curveType = default, CastType castType = default)
            => variable.Tween(variable.Value, end, time, (s, e, t) => castType.Cast(Mathf.Lerp(s, e, t)), curveType.GetCurve());
        #endregion
    }
}