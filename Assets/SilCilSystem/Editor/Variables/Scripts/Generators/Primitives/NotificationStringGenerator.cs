using SilCilSystem.Internals;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal static class NotificationStringGenerator
    {
        private const string MenuPath = "Primitives/String";

        [MenuItem(EditorConstants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            CustomEditorUtil.CreateVariableAsset<NotificationString>("NewVariable.asset", typeof(ReadonlyStringValue), typeof(EventString), typeof(EventStringListener));
        }

        [MenuItem(EditorConstants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            CustomEditorUtil.CreateVariableAsset<EventString>("NewEvent.asset", typeof(EventStringListener));
        }
    }
}
