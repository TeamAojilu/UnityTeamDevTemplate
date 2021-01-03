using SilCilSystem.Internals;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal static class NotificationVector2IntGenerator
    {
        private const string MenuPath = "Struct/Vector2Int";

        [MenuItem(EditorConstants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            CustomEditorUtil.CreateVariableAsset<NotificationVector2Int>("NewVariable.asset", typeof(ReadonlyVector2IntValue), typeof(EventVector2Int), typeof(EventVector2IntListener));
        }

        [MenuItem(EditorConstants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            CustomEditorUtil.CreateVariableAsset<EventVector2Int>("NewEvent.asset", typeof(EventVector2IntListener));
        }
    }
}
