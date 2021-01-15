using UnityEditor;
using UnityEngine;
using SilCilSystem.Editors;
using SilCilSystem.Internals.Variables.Converters;
using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Internals.Editors
{
    [CustomEditor(typeof(VariableVector2IntToVector2))]
    internal class VariableVector2IntToVector2Editor : VariableWithDisplayedValueEditor
    {
        private const float LabelWidth = 20f;
        private const float Margin = 10f;

        protected override void DrawValue(Rect rect)
        {
            float fieldWidth = (rect.width - Margin) / 2f - LabelWidth;

            var rectXLabel = new Rect(rect.x, rect.y, LabelWidth, rect.height);
            var rectX = new Rect(rectXLabel.xMax, rect.y, fieldWidth, rect.height);
            var rectYLabel = new Rect(rectX.xMax + Margin, rect.y, LabelWidth, rect.height);
            var rectY = new Rect(rectYLabel.xMax, rect.y, fieldWidth, rect.height);

            var variable = target as Variable<Vector2>;
            EditorGUI.LabelField(rectXLabel, "x");
            var x = EditorGUI.DelayedFloatField(rectX, variable.Value.x);
            EditorGUI.LabelField(rectYLabel, "y");
            var y = EditorGUI.DelayedFloatField(rectY, variable.Value.y);
            variable.Value = new Vector2(x, y);
        }
    }
}