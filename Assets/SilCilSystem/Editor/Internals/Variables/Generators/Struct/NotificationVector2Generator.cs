using UnityEditor;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Editors
{
    internal static class NotificationVector2Generator
    {
        private const string MenuPath = "Struct/Vector2";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            VariableCreateAction.CreateAsset<NotificationVector2>("NewVariable.asset", typeof(ReadonlyVector2Value), typeof(EventVector2), typeof(EventVector2Listener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            VariableCreateAction.CreateAsset<EventVector2>("NewEvent.asset", typeof(EventVector2Listener));
        }
    }
}
