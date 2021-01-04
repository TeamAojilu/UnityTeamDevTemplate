using SilCilSystem.Internals;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal static class NotificationVector3IntGenerator
    {
        private const string MenuPath = "Struct/Vector3Int";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            CustomEditorUtil.CreateVariableAsset<NotificationVector3Int>("NewVariable.asset", typeof(ReadonlyVector3IntValue), typeof(EventVector3Int), typeof(EventVector3IntListener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            CustomEditorUtil.CreateVariableAsset<EventVector3Int>("NewEvent.asset", typeof(EventVector3IntListener));
        }
    }
}
