using UnityEditor;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Editors
{
    internal static class NotificationStringGenerator
    {
        private const string MenuPath = "Primitives/String";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            VariableCreateAction.CreateAsset<NotificationString>("NewVariable.asset", typeof(ReadonlyStringValue), typeof(EventString), typeof(EventStringListener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            VariableCreateAction.CreateAsset<EventString>("NewEvent.asset", typeof(EventStringListener));
        }
    }
}
