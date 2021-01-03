using SilCilSystem.Internals;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal static class NotificationFloatGenerator
    {
        private const string MenuPath = "Primitives/Float";

        [MenuItem(EditorConstants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            CustomEditorUtil.CreateVariableAsset<NotificationFloat>("NewVariable.asset", typeof(ReadonlyFloatValue), typeof(EventFloat), typeof(EventFloatListener));
        }

        [MenuItem(EditorConstants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            CustomEditorUtil.CreateVariableAsset<EventFloat>("NewEvent.asset", typeof(EventFloatListener));
        }
    }
}
