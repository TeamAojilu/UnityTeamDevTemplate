using UnityEditor;
using SilCilSystem.Editors;
using SilCilSystem.Internals.Variables;

namespace SilCilSystem.Internals.Editors
{
    internal static class NotificationQuaternionGenerator
    {
        private const string MenuPath = "Struct/Quaternion";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            VariableCreateAction.CreateAsset<NotificationQuaternion>("NewVariable.asset", typeof(ReadonlyQuaternionValue), typeof(EventQuaternion), typeof(EventQuaternionListener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            VariableCreateAction.CreateAsset<EventQuaternion>("NewEvent.asset", typeof(EventQuaternionListener));
        }
    }
}
