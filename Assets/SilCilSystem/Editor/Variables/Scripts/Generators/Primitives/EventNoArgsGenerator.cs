using UnityEditor;
using SilCilSystem.Internals;

namespace SilCilSystem.Editors
{
    internal static class EventNoArgsGenerator
    {
        private const string MenuPath = "Primitives/Action";

        [MenuItem(EditorConstants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            CustomEditorUtil.CreateVariableAsset<EventNoArgs>("NewEvent.asset", typeof(EventNoArgsListener));
        }
    }
}
