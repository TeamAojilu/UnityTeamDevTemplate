using System;
using System.Collections;
using UnityEngine;
using static SilCilSystem.Math.CurveTypeDefinition;
using static SilCilSystem.Math.FloatToInt;

namespace SilCilSystem.Variables 
{ 
    public static class TweenStructExtensions
    {
        #region Vector2
        public static IEnumerator Tween(this Variable<Vector2> variable, Vector2 start, Vector2 end, float time, Func<float, float> curve)
            => variable.Tween(start, end, time, Vector2.Lerp, curve);
        public static IEnumerator Tween(this Variable<Vector2> variable, Vector2 start, Vector2 end, float time, CurveType curveType = default)
            => variable.Tween(start, end, time, Vector2.Lerp, curveType.GetCurve());
        public static IEnumerator Tween(this Variable<Vector2> variable, Vector2 end, float time, Func<float, float> curve)
            => variable.Tween(variable.Value, end, time, Vector2.Lerp, curve);
        public static IEnumerator Tween(this Variable<Vector2> variable, Vector2 end, float time, CurveType curveType = default)
            => variable.Tween(variable.Value, end, time, Vector2.Lerp, curveType.GetCurve());
        #endregion

        #region Vecto2Int
        public static IEnumerator Tween(this Variable<Vector2Int> variable, Vector2Int start, Vector2Int end, float time, Func<float, float> curve, CastType castType = default)
            => variable.Tween(start, end, time, (s, e, t) => castType.Cast(Vector2.Lerp(s, e, t)), curve);
        public static IEnumerator Tween(this Variable<Vector2Int> variable, Vector2Int start, Vector2Int end, float time, CurveType curveType = default, CastType castType = default)
            => variable.Tween(start, end, time, (s, e, t) => castType.Cast(Vector2.Lerp(s, e, t)), curveType.GetCurve());
        public static IEnumerator Tween(this Variable<Vector2Int> variable, Vector2Int end, float time, Func<float, float> curve, CastType castType = default)
            => variable.Tween(variable.Value, end, time, (s, e, t) => castType.Cast(Vector2.Lerp(s, e, t)), curve);
        public static IEnumerator Tween(this Variable<Vector2Int> variable, Vector2Int end, float time, CurveType curveType = default, CastType castType = default)
            => variable.Tween(variable.Value, end, time, (s, e, t) => castType.Cast(Vector2.Lerp(s, e, t)), curveType.GetCurve());
        #endregion

        #region Vector3
        public static IEnumerator Tween(this Variable<Vector3> variable, Vector3 start, Vector3 end, float time, Func<float, float> curve)
            => variable.Tween(start, end, time, Vector3.Lerp, curve);
        public static IEnumerator Tween(this Variable<Vector3> variable, Vector3 start, Vector3 end, float time, CurveType curveType = default)
            => variable.Tween(start, end, time, Vector3.Lerp, curveType.GetCurve());
        public static IEnumerator Tween(this Variable<Vector3> variable, Vector3 end, float time, Func<float, float> curve)
            => variable.Tween(variable.Value, end, time, Vector3.Lerp, curve);
        public static IEnumerator Tween(this Variable<Vector3> variable, Vector3 end, float time, CurveType curveType = default)
            => variable.Tween(variable.Value, end, time, Vector3.Lerp, curveType.GetCurve());

        public static IEnumerator TweenSlerp(this Variable<Vector3> variable, Vector3 start, Vector3 end, float time, Func<float, float> curve)
            => variable.Tween(start, end, time, Vector3.Slerp, curve);
        public static IEnumerator TweenSlerp(this Variable<Vector3> variable, Vector3 start, Vector3 end, float time, CurveType curveType = default)
            => variable.Tween(start, end, time, Vector3.Slerp, curveType.GetCurve());
        public static IEnumerator TweenSlerp(this Variable<Vector3> variable, Vector3 end, float time, Func<float, float> curve)
            => variable.Tween(variable.Value, end, time, Vector3.Slerp, curve);
        public static IEnumerator TweenSlerp(this Variable<Vector3> variable, Vector3 end, float time, CurveType curveType = default)
            => variable.Tween(variable.Value, end, time, Vector3.Slerp, curveType.GetCurve());
        #endregion

        #region Vecto3Int
        public static IEnumerator Tween(this Variable<Vector3Int> variable, Vector3Int start, Vector3Int end, float time, Func<float, float> curve, CastType castType = default)
            => variable.Tween(start, end, time, (s, e, t) => castType.Cast(Vector3.Lerp(s, e, t)), curve);
        public static IEnumerator Tween(this Variable<Vector3Int> variable, Vector3Int start, Vector3Int end, float time, CurveType curveType = default, CastType castType = default)
            => variable.Tween(start, end, time, (s, e, t) => castType.Cast(Vector3.Lerp(s, e, t)), curveType.GetCurve());
        public static IEnumerator Tween(this Variable<Vector3Int> variable, Vector3Int end, float time, Func<float, float> curve, CastType castType = default)
            => variable.Tween(variable.Value, end, time, (s, e, t) => castType.Cast(Vector3.Lerp(s, e, t)), curve);
        public static IEnumerator Tween(this Variable<Vector3Int> variable, Vector3Int end, float time, CurveType curveType = default, CastType castType = default)
            => variable.Tween(variable.Value, end, time, (s, e, t) => castType.Cast(Vector3.Lerp(s, e, t)), curveType.GetCurve());

        public static IEnumerator TweenSlerp(this Variable<Vector3Int> variable, Vector3Int start, Vector3Int end, float time, Func<float, float> curve, CastType castType = default)
            => variable.Tween(start, end, time, (s, e, t) => castType.Cast(Vector3.Slerp(s, e, t)), curve);
        public static IEnumerator TweenSlerp(this Variable<Vector3Int> variable, Vector3Int start, Vector3Int end, float time, CurveType curveType = default, CastType castType = default)
            => variable.Tween(start, end, time, (s, e, t) => castType.Cast(Vector3.Slerp(s, e, t)), curveType.GetCurve());
        public static IEnumerator TweenSlerp(this Variable<Vector3Int> variable, Vector3Int end, float time, Func<float, float> curve, CastType castType = default)
            => variable.Tween(variable.Value, end, time, (s, e, t) => castType.Cast(Vector3.Slerp(s, e, t)), curve);
        public static IEnumerator TweenSlerp(this Variable<Vector3Int> variable, Vector3Int end, float time, CurveType curveType = default, CastType castType = default)
            => variable.Tween(variable.Value, end, time, (s, e, t) => castType.Cast(Vector3.Slerp(s, e, t)), curveType.GetCurve());
        #endregion

        #region Quaternion
        public static IEnumerator Tween(this Variable<Quaternion> variable, Quaternion start, Quaternion end, float time, Func<float, float> curve)
            => variable.Tween(start, end, time, Quaternion.Lerp, curve);
        public static IEnumerator Tween(this Variable<Quaternion> variable, Quaternion start, Quaternion end, float time, CurveType curveType = default)
            => variable.Tween(start, end, time, Quaternion.Lerp, curveType.GetCurve());
        public static IEnumerator Tween(this Variable<Quaternion> variable, Quaternion end, float time, Func<float, float> curve)
            => variable.Tween(variable.Value, end, time, Quaternion.Lerp, curve);
        public static IEnumerator Tween(this Variable<Quaternion> variable, Quaternion end, float time, CurveType curveType = default)
            => variable.Tween(variable.Value, end, time, Quaternion.Lerp, curveType.GetCurve());

        public static IEnumerator TweenSlerp(this Variable<Quaternion> variable, Quaternion start, Quaternion end, float time, Func<float, float> curve)
            => variable.Tween(start, end, time, Quaternion.Slerp, curve);
        public static IEnumerator TweenSlerp(this Variable<Quaternion> variable, Quaternion start, Quaternion end, float time, CurveType curveType = default)
            => variable.Tween(start, end, time, Quaternion.Slerp, curveType.GetCurve());
        public static IEnumerator TweenSlerp(this Variable<Quaternion> variable, Quaternion end, float time, Func<float, float> curve)
            => variable.Tween(variable.Value, end, time, Quaternion.Slerp, curve);
        public static IEnumerator TweenSlerp(this Variable<Quaternion> variable, Quaternion end, float time, CurveType curveType = default)
            => variable.Tween(variable.Value, end, time, Quaternion.Slerp, curveType.GetCurve());
        #endregion

        #region Color
        public static IEnumerator Tween(this Variable<Color> variable, Color start, Color end, float time, Func<float, float> curve)
            => variable.Tween(start, end, time, Color.Lerp, curve);
        public static IEnumerator Tween(this Variable<Color> variable, Color start, Color end, float time, CurveType curveType = default)
            => variable.Tween(start, end, time, Color.Lerp, curveType.GetCurve());
        public static IEnumerator Tween(this Variable<Color> variable, Color end, float time, Func<float, float> curve)
            => variable.Tween(variable.Value, end, time, Color.Lerp, curve);
        public static IEnumerator Tween(this Variable<Color> variable, Color end, float time, CurveType curveType = default)
            => variable.Tween(variable.Value, end, time, Color.Lerp, curveType.GetCurve());
        #endregion
    }
}