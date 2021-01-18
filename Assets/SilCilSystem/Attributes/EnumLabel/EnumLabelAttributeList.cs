#if UNITY_EDITOR
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Callbacks;
using UnityEngine;

namespace SilCilSystem.Editors
{
    internal static class EnumLabelAttributeList
    {
        private static Dictionary<Type, EnumLabelInfo> m_list = new Dictionary<Type, EnumLabelInfo>();

        private class EnumLabelInfo
        {
            private readonly string[] m_displayNames;
            private readonly int[] m_values;
            public IReadOnlyList<string> DisplayNames => m_displayNames;
            public IReadOnlyList<int> Values => m_values;
            public EnumLabelInfo(string[] displayNames, int[] values)
            {
                m_displayNames = displayNames;
                m_values = values;
            }
        }

        [DidReloadScripts(TypeDetector.PreTypeDetectOrder)]
        private static void Load()
        {
            m_list.Clear();
            TypeDetector.OnTypeDetected += AddItem;
        }

        private static void AddItem(Type obj)
        {
            if (!IsEnum(obj)) return;

            var members = obj.GetFields().Skip(1).Select(x => x.GetCustomAttribute<EnumLabelAttribute>()).ToArray(); // 最初の要素はEnumそのものっぽい？
            if (members.All(x => x == null)) return;

            var names = obj.GetEnumNames();
            var values = obj.GetEnumValues();
            int[] enumValues = new int[values.Length];
            Array.Copy(values, enumValues, values.Length);

            if(names.Length != enumValues.Length || enumValues.Length != members.Length)
            {
                Debug.LogError($"EnumLabelError: {obj}");
                return;
            }

            for(int i = 0; i < names.Length; i++)
            {
                names[i] = (members[i] != null) ? members[i].DisplayName: names[i];
            }
            m_list[obj] = new EnumLabelInfo(names, enumValues);
        }

        public static IReadOnlyList<string> GetDisplayNames(Type enumType)
        {
            return (m_list.ContainsKey(enumType)) ? m_list[enumType].DisplayNames : null;
        }

        public static IReadOnlyList<int> GetValues(Type enumType)
        {
            return (m_list.ContainsKey(enumType)) ? m_list[enumType].Values : null;
        }

        private static bool IsEnum(Type enumType)
        {
            return typeof(Enum).IsAssignableFrom(enumType);
        }
    }
}
#endif