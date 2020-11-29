using UnityEditor;
using SilCilSystem.Variables;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
    [CustomEditor(typeof(EventNoArgs))]
    internal class GameEventNoArgsEditor : GameEventEditor<GameEvent, EventNoArgsListener> { }

    [CustomEditor(typeof(EventInt))]
    internal class GameEventIntEditor : GameEventEditor<GameEventInt, EventIntListener> { }

    [CustomEditor(typeof(EventBool))]
    internal class GameEventBoolEditor : GameEventEditor<GameEventBool, EventBoolListener> { }

    [CustomEditor(typeof(EventFloat))]
    internal class GameEventFloatEditor : GameEventEditor<GameEventFloat, EventFloatListener> { }

    [CustomEditor(typeof(EventString))]
    internal class GameEventStringEditor : GameEventEditor<GameEventString, EventStringListener> { }
}
#endif