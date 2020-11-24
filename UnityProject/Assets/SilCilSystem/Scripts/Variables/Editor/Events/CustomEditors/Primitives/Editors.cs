using UnityEditor;
using SilCilSystem.Variables;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
    [CustomEditor(typeof(EventNoArgs))]
    internal class GameEventNoArgsEditor : GameEventEditor<EventNoArgs, EventNoArgsListener> { }

    [CustomEditor(typeof(EventInt))]
    internal class GameEventIntEditor : GameEventEditor<EventInt, EventIntListener> { }

    [CustomEditor(typeof(EventBool))]
    internal class GameEventBoolEditor : GameEventEditor<EventBool, EventBoolListener> { }

    [CustomEditor(typeof(EventFloat))]
    internal class GameEventFloatEditor : GameEventEditor<EventFloat, EventFloatListener> { }

    [CustomEditor(typeof(EventString))]
    internal class GameEventStringEditor : GameEventEditor<EventString, EventStringListener> { }
}
#endif