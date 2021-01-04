using SilCilSystem.Internals;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal static class NotificationColorGenerator
    {
        private const string MenuPath = "Struct/Color";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            CustomEditorUtil.CreateVariableAsset<NotificationColor>("NewVariable.asset", typeof(ReadonlyColorValue), typeof(EventColor), typeof(EventColorListener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            CustomEditorUtil.CreateVariableAsset<EventColor>("NewEvent.asset", typeof(EventColorListener));
        }
    }
}
