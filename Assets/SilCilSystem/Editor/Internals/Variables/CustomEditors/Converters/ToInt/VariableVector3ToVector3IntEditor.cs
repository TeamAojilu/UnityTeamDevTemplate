using UnityEngine;
using UnityEditor;
using SilCilSystem.Editors;
using SilCilSystem.Internals.Variables.Converters;
using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Internals.Editors
{
    [CustomEditor(typeof(VariableVector3ToVector3Int))]
    internal class VariableVector3ToVector3IntEditor : VariableWithDisplayedValueEditor
    {
        private const float LabelWidth = 20f;
        private const float Margin = 10f;

        protected override void DrawValue(Rect rect)
        {
            float fieldWidth = (rect.width - 2 * Margin) / 3f - LabelWidth;

            var rectXLabel = new Rect(rect.x, rect.y, LabelWidth, rect.height);
            var rectX = new Rect(rectXLabel.xMax, rect.y, fieldWidth, rect.height);
            var rectYLabel = new Rect(rectX.xMax + Margin, rect.y, LabelWidth, rect.height);
            var rectY = new Rect(rectYLabel.xMax, rect.y, fieldWidth, rect.height);
            var rectZLabel = new Rect(rectY.xMax + Margin, rect.y, LabelWidth, rect.height);
            var rectZ = new Rect(rectZLabel.xMax, rect.y, fieldWidth, rect.height);

            var variable = target as Variable<Vector3Int>;
            var value = variable.Value;
            EditorGUI.LabelField(rectXLabel, "x");
            value.x = EditorGUI.DelayedIntField(rectX, variable.Value.x);
            EditorGUI.LabelField(rectYLabel, "y");
            value.y = EditorGUI.DelayedIntField(rectY, variable.Value.y);
            EditorGUI.LabelField(rectZLabel, "z");
            value.z = EditorGUI.DelayedIntField(rectZ, variable.Value.z);

            if(value != variable.Value)
            {
                variable.Value = value;
            }
        }
    }
}