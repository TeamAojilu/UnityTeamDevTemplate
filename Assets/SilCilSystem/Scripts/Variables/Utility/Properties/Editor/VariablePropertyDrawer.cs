using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
    [CustomPropertyDrawer(typeof(PropertyInt))]
    [CustomPropertyDrawer(typeof(PropertyFloat))]
    [CustomPropertyDrawer(typeof(PropertyBool))]
    [CustomPropertyDrawer(typeof(PropertyString))]
    [CustomPropertyDrawer(typeof(PropertyVector2))]
    [CustomPropertyDrawer(typeof(PropertyVector2Int))]
    [CustomPropertyDrawer(typeof(PropertyVector3))]
    [CustomPropertyDrawer(typeof(PropertyVector3Int))]
    [CustomPropertyDrawer(typeof(PropertyColor))]
    [CustomPropertyDrawer(typeof(PropertyQuaternion))]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyInt))]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyFloat))]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyBool))]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyString))]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyVector2))]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyVector2Int))]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyVector3))]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyVector3Int))]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyColor))]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyQuaternion))]
    public class VariablePropertyDrawer : PropertyDrawer
    {
        private const string ValueName = "m_value";
        private const string VariableName = "m_variable";
        private const float ToggleWidth = 40f;
        private const float Space = 5f;

        private bool m_useVariable = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect mainRect = new Rect(position.x, position.y, position.width - ToggleWidth - Space, position.height);
            Rect toggleRect = new Rect(mainRect.x + mainRect.width + Space, position.y, ToggleWidth, position.height);

            var variable = property.FindPropertyRelative(VariableName);
            var value = property.FindPropertyRelative(ValueName);

            EditorGUI.BeginProperty(position, label, property);
            m_useVariable = EditorGUI.Toggle(toggleRect, variable.objectReferenceValue != null || m_useVariable);
            EditorGUI.PropertyField(mainRect, (m_useVariable) ? variable : value, label);
            EditorGUI.EndProperty();
        }
    }
}
#endif
