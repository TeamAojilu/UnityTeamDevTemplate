using UnityEditor;
using UnityEngine;

namespace SilCilSystem.Editors
{
    [CustomPropertyDrawer(typeof(NotEditableAttribute))]
    internal class NotEditableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }
}