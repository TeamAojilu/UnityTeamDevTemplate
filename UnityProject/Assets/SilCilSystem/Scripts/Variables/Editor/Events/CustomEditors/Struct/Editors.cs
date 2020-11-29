using UnityEditor;
using SilCilSystem.Variables;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
    [CustomEditor(typeof(EventVector2))]
    internal class GameEventVector2Editor : GameEventEditor<GameEventVector2, EventVector2Listener> { }

    [CustomEditor(typeof(EventVector2Int))]
    internal class GameEventVector2IntEditor : GameEventEditor<GameEventVector2Int, EventVector2IntListener> { }

    [CustomEditor(typeof(EventVector3))]
    internal class GameEventVector3Editor : GameEventEditor<GameEventVector3, EventVector3Listener> { }

    [CustomEditor(typeof(EventVector3Int))]
    internal class GameEventVector3IntEditor : GameEventEditor<GameEventVector3Int, EventVector3IntListener> { }

    [CustomEditor(typeof(EventQuaternion))]
    internal class GameEventQuaternionEditor : GameEventEditor<GameEventQuaternion, EventQuaternionListener> { }

    [CustomEditor(typeof(EventColor))]
    internal class GameEventColorEditor : GameEventEditor<GameEventColor, EventColorListener> { }
}
#endif