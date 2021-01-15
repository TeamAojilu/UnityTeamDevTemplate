using UnityEngine;
using UnityEditor;
using SilCilSystem.Editors;
using SilCilSystem.Internals.Variables.Converters;
using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Internals.Editors
{
    [CustomEditor(typeof(VariableFloatToInt))]
    internal class VariableFloatToIntEditor : VariableWithDisplayedValueEditor
    {
        protected override void DrawValue(Rect rect)
        {
            var variable = target as Variable<int>;
            var value = EditorGUI.DelayedIntField(rect, "Value", variable.Value);
            if(value != variable.Value)
            {
                variable.Value = value;
            }
        }
    }
}