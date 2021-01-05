using UnityEditor;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Editors
{
    internal static class NotificationIntGenerator
    {
        private const string MenuPath = "Primitives/Int";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            VariableCreateAction.CreateAsset<NotificationInt>("NewVariable.asset", typeof(ReadonlyIntValue), typeof(EventInt), typeof(EventIntListener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            VariableCreateAction.CreateAsset<EventInt>("NewEvent.asset", typeof(EventIntListener));
        }
    }
}
