using UnityEngine;
using UnityEditor;
using SilCilSystem.Math;

namespace SilCilSystem.Editors
{
    [CustomPropertyDrawer(typeof(InterpolationCurve))]
    internal class InterpolationCurveDrawer : PropertyDrawer
    {
        private const string CurveTypeName = "m_curveType";
        private const string UseCustomName = "m_useCustomCurve";
        private const string CustomCurveName = "m_customCurve";

        private const string ValueName = "m_value";
        private const string VariableName = "m_variable";
        private const float ToggleWidth = 20f;
        private const float Space = 10f;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 描画範囲の計算.
            Rect mainRect = position;
            mainRect.width -= ToggleWidth + Space;

            Rect toggleRect = position;
            toggleRect.width = ToggleWidth;
            toggleRect.x = position.xMax - toggleRect.width;

            // 設定項目の準備.
            var curveType = property.FindPropertyRelative(CurveTypeName);
            var customCurve = property.FindPropertyRelative(CustomCurveName);
            var useCustom = property.FindPropertyRelative(UseCustomName);
            
            // 描画.
            EditorGUI.BeginProperty(position, label, property);
            useCustom.boolValue = GUI.Toggle(toggleRect, useCustom.boolValue, "");
            EditorGUI.PropertyField(mainRect, (useCustom.boolValue) ? customCurve : curveType, label);
            EditorGUI.EndProperty();
        }
    }
}