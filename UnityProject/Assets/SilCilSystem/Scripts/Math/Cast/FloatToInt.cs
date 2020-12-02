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

        // switch判定を複数回呼ぶのが気持ち悪い気もするが、コードの短さ重視で.
        public static Vector2Int Cast(this CastType castType, Vector2 value)
            => new Vector2Int(castType.Cast(value.x), castType.Cast(value.y));

        // switch判定を複数回呼ぶのが気持ち悪い気もするが、コードの短さ重視で.
        public static Vector3Int Cast(this CastType castType, Vector3 value)
            => new Vector3Int(castType.Cast(value.x), castType.Cast(value.y), castType.Cast(value.z));
    }
}