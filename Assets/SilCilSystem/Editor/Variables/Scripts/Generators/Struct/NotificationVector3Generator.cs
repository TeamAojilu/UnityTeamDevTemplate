using SilCilSystem.Internals;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal static class NotificationVector3Generator
    {
        private const string MenuPath = "Struct/Vector3";

        [MenuItem(EditorConstants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            CustomEditorUtil.CreateVariableAsset<NotificationVector3>("NewVariable.asset", typeof(ReadonlyVector3Value), typeof(EventVector3), typeof(EventVector3Listener));
        }

        [MenuItem(EditorConstants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            CustomEditorUtil.CreateVariableAsset<EventVector3>("NewEvent.asset", typeof(EventVector3Listener));
        }
    }
}
