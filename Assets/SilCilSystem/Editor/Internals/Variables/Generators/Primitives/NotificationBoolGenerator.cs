using UnityEditor;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Editors
{
    internal static class NotificationBoolGenerator
    {
        private const string MenuPath = "Primitives/Bool";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            VariableCreateAction.CreateAsset<NotificationBool>("NewVariable.asset", typeof(ReadonlyBoolValue), typeof(EventBool), typeof(EventBoolListener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            VariableCreateAction.CreateAsset<EventBool>("NewEvent.asset", typeof(EventBoolListener));
        }
    }
}
