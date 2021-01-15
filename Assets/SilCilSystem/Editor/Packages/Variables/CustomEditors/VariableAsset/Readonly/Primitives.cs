using UnityEditor;
using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Editors
{
    [CustomEditor(typeof(ReadonlyVariable<bool>), true), CanEditMultipleObjects]
    internal class ReadonlyVariableBoolEditor : ReadonlyVariableEditor<bool> { }

    [CustomEditor(typeof(ReadonlyVariable<int>), true), CanEditMultipleObjects]
    internal class ReadonlyVariableIntEditor : ReadonlyVariableEditor<int> { }

    [CustomEditor(typeof(ReadonlyVariable<float>), true), CanEditMultipleObjects]
    internal class ReadonlyVariableFloatEditor : ReadonlyVariableEditor<float> { }

    [CustomEditor(typeof(ReadonlyVariable<string>), true), CanEditMultipleObjects]
    internal class ReadonlyVariableStringEditor : ReadonlyVariableEditor<string> { }
}