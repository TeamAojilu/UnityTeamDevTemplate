using SilCilSystem.Internals;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal static class NotificationQuaternionGenerator
    {
        private const string MenuPath = "Struct/Quaternion";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            CustomEditorUtil.CreateVariableAsset<NotificationQuaternion>("NewVariable.asset", typeof(ReadonlyQuaternionValue), typeof(EventQuaternion), typeof(EventQuaternionListener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            CustomEditorUtil.CreateVariableAsset<EventQuaternion>("NewEvent.asset", typeof(EventQuaternionListener));
        }
    }
}
