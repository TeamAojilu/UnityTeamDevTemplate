using System;
using UnityEngine;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal abstract class VariableWithDisplayedValueEditor : VariableAssetEditor
    {
        public override void OnInspectorGUI()
        {
            var rect = GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth, EditorGUIUtility.singleLineHeight + 10f);
            var labelRect = new Rect(rect.x, rect.y + 5f, rect.width, rect.height - 10f);

            DrawLineWithShadow(rect.position, rect.width);
            try
            {
                DrawValue(labelRect);
            }
            catch (NullReferenceException)
            {
                EditorGUI.LabelField(rect, "Value", "NULL");
            }
            DrawLineWithShadow(new Vector2(rect.x, rect.yMax), rect.width);
            EditorGUILayout.Space();

            base.OnInspectorGUI();
        }

        protected abstract void DrawValue(Rect rect);

        private static void DrawLineWithShadow(Vector2 topLeft, float width)
        {
            var lineColor = new Color(0f, 0f, 0f, 0.5f);
            var lineShadowColor = new Color(1f, 1f, 1f, 0.1f);
            var rect = new Rect(topLeft.x, topLeft.y, width, 1f);

            EditorGUI.DrawRect(rect, lineColor);
            rect.y += rect.height;
            EditorGUI.DrawRect(rect, lineShadowColor);
        }
    }
}