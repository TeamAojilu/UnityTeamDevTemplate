using System;
using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Editors
{
    internal class ReadonlyVariableEditor<T> : VariableAssetEditor
    {
        public override void OnInspectorGUI()
        {
            var rect = GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth, EditorGUIUtility.singleLineHeight + 10f);
            var labelRect = new Rect(rect.x, rect.y + 5f, rect.width, rect.height - 10f);

            DrawLine(rect.position, rect.width);
            try
            {
                var variable = target as ReadonlyVariable<T>;
                DrawReadonlyValue(labelRect, variable.Value);
            }
            catch (NullReferenceException)
            {
                EditorGUI.LabelField(rect, "Value", "NULL");
            }
            DrawLine(new Vector2(rect.x, rect.yMax), rect.width);
            EditorGUILayout.Space();

            base.OnInspectorGUI();
        }

        private static void DrawLine(Vector2 topLeft, float width)
        {
            var lineColor = new Color(0f, 0f, 0f, 0.5f);
            var lineShadowColor = new Color(1f, 1f, 1f, 0.1f);
            var rect = new Rect(topLeft.x, topLeft.y, width, 1f);

            EditorGUI.DrawRect(rect, lineColor);
            rect.y += rect.height;
            EditorGUI.DrawRect(rect, lineShadowColor);
        }

        protected virtual void DrawReadonlyValue(Rect rect, T value)
        {
            EditorGUI.LabelField(rect, "Value", value.ToString());
        }
    }
}