using UnityEngine;

namespace SilCilSystem.Math
{
    public static class FloatToInt
    {
        public enum CastType
        {
            Simple,
            FloorToInt,
            CeilToInt,
            RoundToInt,
        }

        public static int Cast(this CastType castType, float value)
        {
            switch (castType)
            {
                default:
                case CastType.Simple:
                    return (int)value;
                case CastType.FloorToInt:
                    return Mathf.FloorToInt(value);
                case CastType.CeilToInt:
                    return Mathf.CeilToInt(value);
                case CastType.RoundToInt:
                    return Mathf.RoundToInt(value);
            }
        }
    }
}