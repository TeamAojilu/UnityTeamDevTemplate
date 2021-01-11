using UnityEditor;
using UnityEngine;
using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Editors
{
    internal class PropertyDrawerWithTooltipBase<T> : PropertyTDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var variable = property.FindPropertyRelative(VariableName);

            if(variable.objectReferenceValue is Variable<T> variableT)
            {
                label.tooltip = $"{label.tooltip}\nValue: {variableT.Value}";
            }
            else if(variable.objectReferenceValue is ReadonlyVariable<T> readonlyT)
            {
                try
                {
                    label.tooltip = $"{label.tooltip}\nValue: {readonlyT.Value}";
                }
                catch(System.NullReferenceException)
                {
                    label.tooltip = $"{label.tooltip}\nValue: NULL";
                }
            }

            Debug.Log(label.tooltip);
            base.OnGUI(position, property, label);
        }
    }
}