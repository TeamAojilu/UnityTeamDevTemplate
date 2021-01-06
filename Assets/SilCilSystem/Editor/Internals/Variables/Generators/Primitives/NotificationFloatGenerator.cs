using UnityEditor;
using SilCilSystem.Editors;
using SilCilSystem.Internals.Variables;

namespace SilCilSystem.Internals.Editors
{
    internal static class NotificationFloatGenerator
    {
        private const string MenuPath = "Primitives/Float";

        [MenuItem(Constants.CreateVariableMenuPath + MenuPath, false, 0)]
        private static void CreateVariableAsset()
        {
            VariableCreateAction.CreateAsset<NotificationFloat>("NewVariable.asset", typeof(ReadonlyFloatValue), typeof(EventFloat), typeof(EventFloatListener));
        }

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            VariableCreateAction.CreateAsset<EventFloat>("NewEvent.asset", typeof(EventFloatListener));
        }
    }
}
