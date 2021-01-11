using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Editors
{
    [CustomEditor(typeof(ReadonlyVariable<Vector2>), true), CanEditMultipleObjects]
    internal class ReadonlyVariableVector2Editor : ReadonlyVariableEditor<Vector2> { }

    [CustomEditor(typeof(ReadonlyVariable<Vector2Int>), true), CanEditMultipleObjects]
    internal class ReadonlyVariableVector2IntEditor : ReadonlyVariableEditor<Vector2Int> { }

    [CustomEditor(typeof(ReadonlyVariable<Vector3>), true), CanEditMultipleObjects]
    internal class ReadonlyVariableVector3Editor : ReadonlyVariableEditor<Vector3> { }

    [CustomEditor(typeof(ReadonlyVariable<Vector3Int>), true), CanEditMultipleObjects]
    internal class ReadonlyVariableVector3IntEditor : ReadonlyVariableEditor<Vector3Int> { }

    [CustomEditor(typeof(ReadonlyVariable<Quaternion>), true), CanEditMultipleObjects]
    internal class ReadonlyVariableQuaternionEditor : ReadonlyVariableEditor<Quaternion> { }

    [CustomEditor(typeof(ReadonlyVariable<Color>), true), CanEditMultipleObjects]
    internal class ReadonlyVariableColorEditor : ReadonlyVariableEditor<Color> 
    {
        protected override void DrawReadonlyValue(Rect rect, Color value)
        {
            GUI.enabled = false;
            EditorGUI.ColorField(rect, "Value", value);
            GUI.enabled = true;
        }
    }
}