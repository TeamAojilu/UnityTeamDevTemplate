using SilCilSystem.Internals;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal static class NotificationIntGenerator
    {
        private const string MenuPath = "Primitives/Int";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            CustomEditorUtil.CreateVariableAsset<NotificationInt>("NewVariable.asset", typeof(ReadonlyIntValue), typeof(EventInt), typeof(EventIntListener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            CustomEditorUtil.CreateVariableAsset<EventInt>("NewEvent.asset", typeof(EventIntListener));
        }
    }
}