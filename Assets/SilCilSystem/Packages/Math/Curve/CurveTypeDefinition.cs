using SilCilSystem.Editors;
using System;
using UnityEngine;

namespace SilCilSystem.Math
{
    public static class CurveTypeDefinition
    {
        /// <summary>
        /// 補間曲線の種類
        /// 【注意】intValueは指定で決めている（enumはSerializeの際にintで保存される. 変更してEnumが変わってしまうのを回避したい）
        /// </summary>
        public enum CurveType
        {
            /// <summary>線形補間</summary>
            Linear = 0,

            /// <summary>2次関数によるEaseIn</summary>
            [EnumLabel("EaseIn/Quad")] EaseInQuad = 100,
            /// <summary>2次関数によるEaseOut</summary>
            [EnumLabel("EaseOut/Quad")] EaseOutQuad,
            /// <summary>2次関数によるEaseInOut</summary>
            [EnumLabel("EaseInOut/Quad")] EaseInOutQuad,

            /// <summary>4次関数によるEaseIn</summary>
            [EnumLabel("EaseIn/Quart")] EaseInQuart = 200,
            /// <summary>4次関数によるEaseOut</summary>
            [EnumLabel("EaseOut/Quart")] EaseOutQuart,
            /// <summary>4次関数によるEaseInOut</summary>
            [EnumLabel("EaseInOut/Quart")] EaseInOutQuart,

            /// <summary>三角関数によるEaseIn</summary>
            [EnumLabel("EaseIn/Sin")] EaseInSin = 300,
            /// <summary>三角関数によるEaseOut</summary>
            [EnumLabel("EaseOut/Sin")] EaseOutSin,
            /// <summary>三角関数によるEaseInOut</summary>
            [EnumLabel("EaseInOut/Sin")] EaseInOutSin,

            /// <summary>反動ありEaseIn</summary>
            [EnumLabel("EaseIn/Back")] EaseInBack = 400,
            /// <summary>反動ありEaseOut</summary>
            [EnumLabel("EaseOut/Back")] EaseOutBack,
            /// <summary>反動ありEaseInOut</summary>
            [EnumLabel("EaseInOut/Back")] EaseInOutBack,

            /// <summary>跳ね返りEaseIn</summary>
            [EnumLabel("EaseIn/Bounce")] EaseInBounce = 500,
            /// <summary>跳ね返りEaseOut</summary>
            [EnumLabel("EaseOut/Bounce")] EaseOutBounce,
            /// <summary>跳ね返りEaseInOut</summary>
            [EnumLabel("EaseInOut/Bounce")] EaseInOutBounce,

            /// <summary>直線によるループ</summary>
            [EnumLabel("Loop/Linear")] LoopLinear = 1000,
            /// <summary>放物線（2次関数）によるループ</summary>
            [EnumLabel("Loop/Jump")] LoopJump,
            /// <summary>三角関数によるループ</summary>
            [EnumLabel("Loop/Sin")] LoopSin,

            /// <summary>Unity.Random</summary>
            Random = 10000,
        }

        public static Func<float, float> GetCurve(this CurveType type)
        {
            switch (type)
            {
                default:
                // 1次
                case CurveType.Linear: return x => x;
                case CurveType.LoopLinear: return x => (x < 0.5f) ? 2f * x : 2f * (1f - x);
                // 2次
                case CurveType.EaseInQuad: return x => x * x;
                case CurveType.EaseOutQuad: return x => 1f - (1f - x) * (1f - x);
                case CurveType.EaseInOutQuad: return x => (x < 0.5f) ? 2f * x * x : 1f - Mathf.Pow(-2f * (1f - x), 2f) / 2f;
                case CurveType.LoopJump: return x => -4f * (x - 0.5f) * (x - 0.5f) + 1f;
                // 4次
                case CurveType.EaseInQuart: return x => x * x * x * x;
                case CurveType.EaseOutQuart: return x => 1f - Mathf.Pow(1f - x, 4);
                case CurveType.EaseInOutQuart: return x => (x < 0.5f) ? 8f * x * x * x * x : 1 - Mathf.Pow(2f * (1f - x), 4) / 2f;
                // Sin
                case CurveType.EaseInSin: return x => 1f - Mathf.Cos(x * Mathf.PI / 2f);
                case CurveType.EaseOutSin: return x => Mathf.Sin(x * Mathf.PI / 2f);
                case CurveType.EaseInOutSin: return x => -(Mathf.Cos(Mathf.PI * x) - 1f) / 2f;
                case CurveType.LoopSin: return x => 0.5f * (Mathf.Sin(2f * Mathf.PI * (x + 0.75f)) + 1f);
                // Back
                case CurveType.EaseInBack: return x => (1f + 1.70158f) * x * x * x - (1.70158f) * x * x;
                case CurveType.EaseOutBack: return x => 1f + (1f + 1.70158f) * Mathf.Pow(x - 1f, 3) + (1.70158f) * Mathf.Pow(x - 1f, 2);
                case CurveType.EaseInOutBack:
                    float c1 = 1.70158f;
                    float c2 = c1 * 1.525f;
                    return x => (x < 0.5f) ? Mathf.Pow(2f * x, 2) * ((c2 + 1f) * 2f * x - c2) / 2f
                      : (Mathf.Pow(2f * (1f - x), 2) * ((c2 + 1) * 2f * (x - 1f) + c2) + 2f) / 2f;
                // Bounce
                case CurveType.EaseOutBounce:
                    float n1 = 7.5625f;
                    float d1 = 2.75f;
                    return x =>
                    {
                        if (x < 1f / d1) return n1 * x * x;
                        else if (x < 2f / d1) return n1 * (x -= 1.5f / d1) * x + 0.75f;
                        else if (x < 2.5f / d1) return n1 * (x -= 2.25f / d1) * x + 0.9375f;
                        else return n1 * (x -= 2.625f / d1) * x + 0.984375f;
                    };
                case CurveType.EaseInBounce: return x => 1f - GetCurve(CurveType.EaseOutBounce).Invoke(1f - x);
                case CurveType.EaseInOutBounce:
                    return x => (x < 0.5f) ? (1f - GetCurve(CurveType.EaseOutBounce).Invoke(1f - 2 * x)) / 2f
                    : (1f + GetCurve(CurveType.EaseOutBounce).Invoke(2f * x - 1)) / 2f;
                // Random
                case CurveType.Random:
                    return x => UnityEngine.Random.Range(0f, 1f);
            }
        }
    }
}