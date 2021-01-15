using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SilCilSystem.Editors
{
    [CustomPropertyDrawer(typeof(Enum), true)]
    internal class EnumLabelDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            do
            {
                if (property.propertyType != SerializedPropertyType.Enum) break;
                var displayNames = EnumLabelAttributeList.GetDisplayNames(fieldInfo.FieldType)?.Select(x => new GUIContent(x)).ToArray();
                var options = EnumLabelAttributeList.GetValues(fieldInfo.FieldType)?.ToArray();
                if (displayNames == null || options == null) break;

                EditorGUI.BeginProperty(position, label, property);
                property.intValue = EditorGUI.IntPopup(position, label, property.intValue, displayNames, options);
                EditorGUI.EndProperty();
                return;
            } while (false);
            DrawDefault(position, property, label);
        }

        private static void DrawDefault(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property);
            EditorGUI.EndProperty();
        }
    }
}