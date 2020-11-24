using UnityEditor;
using SilCilSystem.Variables;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
    [CustomEditor(typeof(NotificationInt))]
    internal class NotificationIntEditor : NotificationVariableEditor<NotificationInt, ReadonlyIntValue, EventInt, VariableInt, GameEventInt> { }

    [CustomEditor(typeof(NotificationBool))]
    internal class NotificationBoolEditor : NotificationVariableEditor<NotificationBool, ReadonlyBoolValue, EventBool, VariableBool, GameEventBool> { }

    [CustomEditor(typeof(NotificationString))]
    internal class NotificationStringEditor : NotificationVariableEditor<NotificationString, ReadonlyStringValue, EventString, VariableString, GameEventString> { }

    [CustomEditor(typeof(NotificationFloat))]
    internal class NotificationFloatEditor : NotificationVariableEditor<NotificationFloat, ReadonlyFloatValue, EventFloat, VariableFloat, GameEventFloat> { }
}
#endif