using UnityEditor;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Editors
{
    internal static class NotificationVector3IntGenerator
    {
        private const string MenuPath = "Struct/Vector3Int";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            VariableCreateAction.CreateAsset<NotificationVector3Int>("NewVariable.asset", typeof(ReadonlyVector3IntValue), typeof(EventVector3Int), typeof(EventVector3IntListener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            VariableCreateAction.CreateAsset<EventVector3Int>("NewEvent.asset", typeof(EventVector3IntListener));
        }
    }
}
