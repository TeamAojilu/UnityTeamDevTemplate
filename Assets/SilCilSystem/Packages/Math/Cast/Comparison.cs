using System;
using System.Collections.Generic;
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
            => Compare(value1.CompareTo(value2), compareType);

        public static bool Compare<T>(this CompareType compareType, T value1, T value2, IComparer<T> comparer)
            => Compare(comparer.Compare(value1, value2), compareType);

        private static bool Compare(int compareValue, CompareType compareType)
        {
            switch (compareType)
            {
                case CompareType.EqualTo: return compareValue == 0;
                case CompareType.NotEqualTo: return compareValue != 0;
                case CompareType.LessThan: return compareValue < 0;
                case CompareType.LessThanOrEqualTo: return compareValue <= 0;
                case CompareType.GreaterThan: return compareValue > 0;
                case CompareType.GreaterThanOrEqualTo: return compareValue >= 0;
                default: return false;
            }
        }

        public static bool CompareTo<T>(this T value1, T value2, CompareType compareType) where T : IComparable<T>
            => compareType.Compare(value1, value2);

        public static bool CompareTo<T>(this T value1, T value2, CompareType compareType, IComparer<T> comparer)
            => compareType.Compare(value1, value2, comparer);
    }
}