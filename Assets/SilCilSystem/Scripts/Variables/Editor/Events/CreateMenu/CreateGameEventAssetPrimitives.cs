using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
    internal static class CreateEventAssetPrimitives
    {
        private const string MenuPath = EditorConstants.CreateAssetMenuPath + "GameEvents/Primitives/";

        [MenuItem(MenuPath + "Action", false, 0)]
        private static void CreateEventNoArgsAsset()
        {
            var asset = ScriptableObject.CreateInstance<EventNoArgs>();
            ProjectWindowUtil.CreateAsset(asset, "New Event.asset");
        }

        [MenuItem(MenuPath + "Int", false, 0)]
        private static void CreateEventIntAsset()
        {
            var asset = ScriptableObject.CreateInstance<EventInt>();
            ProjectWindowUtil.CreateAsset(asset, "New Event.asset");
        }

        [MenuItem(MenuPath + "Bool", false, 0)]
        private static void CreateEventBoolAsset()
        {
            var asset = ScriptableObject.CreateInstance<EventBool>();
            ProjectWindowUtil.CreateAsset(asset, "New Event.asset");
        }

        [MenuItem(MenuPath + "String", false, 0)]
        private static void CreateEventStringAsset()
        {
            var asset = ScriptableObject.CreateInstance<EventString>();
            ProjectWindowUtil.CreateAsset(asset, "New Event.asset");
        }

        [MenuItem(MenuPath + "Float", false, 0)]
        private static void CreateEventFloatAsset()
        {
            var asset = ScriptableObject.CreateInstance<EventFloat>();
            ProjectWindowUtil.CreateAsset(asset, "New Event.asset");
        }
    }
}
#endif