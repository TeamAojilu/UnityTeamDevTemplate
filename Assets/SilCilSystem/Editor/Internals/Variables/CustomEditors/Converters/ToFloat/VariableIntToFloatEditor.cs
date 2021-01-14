using UnityEditor;
using UnityEngine;
using SilCilSystem.Editors;
using SilCilSystem.Internals.Variables.Converters;
using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Internals.Editors
{
    [CustomEditor(typeof(VariableIntToFloat))]
    internal class VariableIntToFloatEditor : VariableWithDisplayedValueEditor
    {
        protected override void DrawValue(Rect rect)
        {
            var variable = target as Variable<float>;
            variable.Value = EditorGUI.DelayedFloatField(rect, "Value", variable.Value);
        }
    }
}