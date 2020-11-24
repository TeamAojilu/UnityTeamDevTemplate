using UnityEditor;
using SilCilSystem.Variables;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
    [CustomEditor(typeof(EventVector2))]
    internal class GameEventVector2Editor : GameEventEditor<EventVector2, EventVector2Listener> { }

    [CustomEditor(typeof(EventVector2Int))]
    internal class GameEventVector2IntEditor : GameEventEditor<EventVector2Int, EventVector2IntListener> { }

    [CustomEditor(typeof(EventVector3))]
    internal class GameEventVector3Editor : GameEventEditor<EventVector3, EventVector3Listener> { }

    [CustomEditor(typeof(EventVector3Int))]
    internal class GameEventVector3IntEditor : GameEventEditor<EventVector3Int, EventVector3IntListener> { }

    [CustomEditor(typeof(EventQuaternion))]
    internal class GameEventQuaternionEditor : GameEventEditor<EventQuaternion, EventQuaternionListener> { }

    [CustomEditor(typeof(EventColor))]
    internal class GameEventColorEditor : GameEventEditor<EventColor, EventColorListener> { }
}
#endif