using System.Collections;
using UnityEngine;
using System;
using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Variables
{
    public static class TweenVariablesExtensions
    {
        public static IEnumerator Tween<T>(this Variable<T> variable, T start, T end, float time, Func<T, T, float, T> lerp, Func<float, float> curve)
        {
            float timer = 0f;
            while (timer < time)
            {
                timer += Time.deltaTime;
                variable.Value = lerp(start, end, curve(Mathf.Clamp01(timer / time)));
                yield return null;
            }
            variable.Value = lerp(start, end, curve(1f));
        }
    }
}