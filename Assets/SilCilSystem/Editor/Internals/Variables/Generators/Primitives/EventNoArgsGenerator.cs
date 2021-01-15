using UnityEditor;
using SilCilSystem.Editors;
using SilCilSystem.Internals.Variables;

namespace SilCilSystem.Internals.Editors
{
    internal static class EventNoArgsGenerator
    {
        private const string MenuPath = "Primitives/Action";

        [MenuItem(Constants.CreateGameEventMenuPath + MenuPath, false, 0)]
        private static void CreateEventAsset()
        {
            VariableCreateAction.CreateAsset<EventNoArgs>("NewEvent.asset", typeof(EventNoArgsListener));
        }
    }
}
