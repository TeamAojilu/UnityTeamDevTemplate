using UnityEditor;
using SilCilSystem.Editors;
using SilCilSystem.Internals.Variables;

namespace SilCilSystem.Internals.Editors
{
    internal static class NotificationColorGenerator
    {
        private const string MenuPath = "Struct/Color";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            VariableCreateAction.CreateAsset<NotificationColor>("NewVariable.asset", typeof(ReadonlyColorValue), typeof(EventColor), typeof(EventColorListener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            VariableCreateAction.CreateAsset<EventColor>("NewEvent.asset", typeof(EventColorListener));
        }
    }
}
