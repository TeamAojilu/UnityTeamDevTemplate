using System;
using SilCilSystem.Editors;

namespace SilCilSystem.Math
{
    public static class Comparison
    {
        public enum CompareType
        {
            [EnumLabel("==")] EqualTo,
            [EnumLabel("!=")] NotEqualTo,
            [EnumLabel("<")] LessThan,
            [EnumLabel("<=")] LessThanOrEqualTo,
            [EnumLabel(">")] GreaterThan,
            [EnumLabel(">=")] GreaterThanOrEqualTo,
        }

        public static bool Compare<T>(this CompareType compareType, T value1, T value2) where T : IComparable<T>
        {
            switch (compareType)
            {
                case CompareType.EqualTo: return value1.CompareTo(value2) == 0;
                case CompareType.NotEqualTo: return value1.CompareTo(value2) != 0;
                case CompareType.LessThan: return value1.CompareTo(value2) < 0;
                case CompareType.LessThanOrEqualTo: return value1.CompareTo(value2) <= 0;
                case CompareType.GreaterThan: return value1.CompareTo(value2) > 0;
                case CompareType.GreaterThanOrEqualTo: return value1.CompareTo(value2) >= 0;
                default: return false;
            }
        }

        public static bool CompareTo<T>(this T value1, T value2, CompareType compareType) where T : IComparable<T>
            => compareType.Compare(value1, value2);
    }
}