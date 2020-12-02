using UnityEditor;
using SilCilSystem.Variables;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
    [CustomEditor(typeof(NotificationVector2))]
    internal class NotificationVector2Editor : NotificationVariableEditor<NotificationVector2, ReadonlyVector2Value, EventVector2, VariableVector2, GameEventVector2> { }

    [CustomEditor(typeof(NotificationVector2Int))]
    internal class NotificationVector2IntEditor : NotificationVariableEditor<NotificationVector2Int, ReadonlyVector2IntValue, EventVector2Int, VariableVector2Int, GameEventVector2Int> { }

    [CustomEditor(typeof(NotificationVector3))]
    internal class NotificationVector3Editor : NotificationVariableEditor<NotificationVector3, ReadonlyVector3Value, EventVector3, VariableVector3, GameEventVector3> { }

    [CustomEditor(typeof(NotificationVector3Int))]
    internal class NotificationVector3IntEditor : NotificationVariableEditor<NotificationVector3Int, ReadonlyVector3IntValue, EventVector3Int, VariableVector3Int, GameEventVector3Int> { }

    [CustomEditor(typeof(NotificationQuaternion))]
    internal class NotificationQuaternionEditor : NotificationVariableEditor<NotificationQuaternion, ReadonlyQuaternionValue, EventQuaternion, VariableQuaternion, GameEventQuaternion> { }

    [CustomEditor(typeof(NotificationColor))]
    internal class NotificationColorEditor : NotificationVariableEditor<NotificationColor, ReadonlyColorValue, EventColor, VariableColor, GameEventColor> { }
}
#endif