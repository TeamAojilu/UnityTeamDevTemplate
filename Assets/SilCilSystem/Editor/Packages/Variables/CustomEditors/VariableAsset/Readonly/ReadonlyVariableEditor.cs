using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Editors
{
    internal class ReadonlyVariableEditor<T> : VariableWithDisplayedValueEditor
    {
        protected override void DrawValue(Rect rect)
        {
            var variable = target as ReadonlyVariable<T>;
            EditorGUI.LabelField(rect, "Value", variable.Value.ToString());
        }
    }
}