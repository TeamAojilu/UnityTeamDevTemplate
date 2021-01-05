using UnityEditor;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Editors
{
    internal static class NotificationVector2IntGenerator
    {
        private const string MenuPath = "Struct/Vector2Int";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            VariableCreateAction.CreateAsset<NotificationVector2Int>("NewVariable.asset", typeof(ReadonlyVector2IntValue), typeof(EventVector2Int), typeof(EventVector2IntListener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            VariableCreateAction.CreateAsset<EventVector2Int>("NewEvent.asset", typeof(EventVector2IntListener));
        }
    }
}
