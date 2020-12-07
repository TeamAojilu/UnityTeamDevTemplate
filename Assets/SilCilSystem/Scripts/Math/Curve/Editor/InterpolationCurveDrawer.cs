using UnityEngine;
using UnityEditor;
using SilCilSystem.Math;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
    [CustomPropertyDrawer(typeof(InterpolationCurve))]
    public class InterpolationCurveDrawer : PropertyDrawer
    {
        private const string CurveTypeName = "m_curveType";
        private const string UseCustomName = "m_useCustomCurve";
        private const string CustomCurveName = "m_customCurve";
        private const float ToggleWidth = 40f;
        private const float Space = 5f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {   
            Rect mainRect = new Rect(position.x, position.y, position.width - ToggleWidth - Space, position.height);
            Rect toggleRect = new Rect(mainRect.x + mainRect.width + Space, position.y, ToggleWidth, position.height);

            var curveType = property.FindPropertyRelative(CurveTypeName);
            var useCustom = property.FindPropertyRelative(UseCustomName);
            var customCurve = property.FindPropertyRelative(CustomCurveName);

            EditorGUI.BeginProperty(position, label, property);
            useCustom.boolValue = EditorGUI.Toggle(toggleRect, useCustom.boolValue);
            EditorGUI.PropertyField(mainRect, (useCustom.boolValue) ? customCurve : curveType, label);
            EditorGUI.EndProperty();
        }
    }
}
#endif