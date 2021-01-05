using UnityEditor;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Editors
{
    internal static class NotificationVector3Generator
    {
        private const string MenuPath = "Struct/Vector3";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            VariableCreateAction.CreateAsset<NotificationVector3>("NewVariable.asset", typeof(ReadonlyVector3Value), typeof(EventVector3), typeof(EventVector3Listener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            VariableCreateAction.CreateAsset<EventVector3>("NewEvent.asset", typeof(EventVector3Listener));
        }
    }
}
