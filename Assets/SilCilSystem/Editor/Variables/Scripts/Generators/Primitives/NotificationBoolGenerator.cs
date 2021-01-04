using SilCilSystem.Internals;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal static class NotificationBoolGenerator
    {
        private const string MenuPath = "Primitives/Bool";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            CustomEditorUtil.CreateVariableAsset<NotificationBool>("NewVariable.asset", typeof(ReadonlyBoolValue), typeof(EventBool), typeof(EventBoolListener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            CustomEditorUtil.CreateVariableAsset<EventBool>("NewEvent.asset", typeof(EventBoolListener));
        }
    }
}
