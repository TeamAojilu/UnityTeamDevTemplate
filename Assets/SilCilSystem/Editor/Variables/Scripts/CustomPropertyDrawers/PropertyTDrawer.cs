using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables;
using System.Linq;
using SilCilSystem.Variables.Base;
using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Editors
{
    // [CustomPropertyDrawer(typeof(Property<>), true)]
    // [CustomPropertyDrawer(typeof(ReadonlyProperty<>), true)]
    [CustomPropertyDrawer(typeof(Property<,>), true)]
    [CustomPropertyDrawer(typeof(ReadonlyProperty<,>), true)]
    public class PropertyTDrawer : PropertyDrawer
    {
        private const string ValueName = "m_value";
        private const string VariableName = "m_variable";
        private const float ToggleWidth = 20f;
        private const float Space = 10f;

        private Dictionary<Rect, bool> m_useVariables = new Dictionary<Rect, bool>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 描画範囲の計算.
            Rect mainRect = position;
            mainRect.width -= ToggleWidth + Space;

            Rect toggleRect = position;
            toggleRect.width = ToggleWidth;
            toggleRect.x = position.xMax - toggleRect.width;

            // 設定項目の準備.
            var variable = property.FindPropertyRelative(VariableName);
            var value = property.FindPropertyRelative(ValueName);
            if (!m_useVariables.ContainsKey(position))
            {
                m_useVariables[position] = false;
            }

            EditorGUI.BeginProperty(position, label, property);

            // 描画.
            m_useVariables[position] = GUI.Toggle(toggleRect, variable.objectReferenceValue != null || m_useVariables[position], "");
            EditorGUI.PropertyField(mainRect, (m_useVariables[position]) ? variable : value, label);

            // ドラッグ処理.
            if (position.Contains(Event.current.mousePosition))
            {
                switch (Event.current.type)
                {
                    case EventType.DragUpdated:
                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                        break;
                    case EventType.DragPerform:
                        DragAndDrop.AcceptDrag();
                        var obj = DragAndDrop.objectReferences.FirstOrDefault();
                        if (!(obj is VariableAsset parent)) break;
                        
                        foreach(var asset in parent.GetAllVariables())
                        {
                            variable.objectReferenceValue = asset;
                            if (variable.objectReferenceValue != null) break;
                        }
                        break;
                }
            }

            EditorGUI.EndProperty();
        }
    }
}