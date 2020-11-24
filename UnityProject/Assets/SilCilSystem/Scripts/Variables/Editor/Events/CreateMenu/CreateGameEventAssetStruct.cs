using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
    internal static class CreateEventAssetStruct
    {
        private const string MenuPath = EditorConstants.CreateAssetMenuPath + "GameEvents/Struct/";

        [MenuItem(MenuPath + "Vector2", false, 0)]
        private static void CreateEventVector2Asset()
        {
            var asset = ScriptableObject.CreateInstance<EventVector2>();
            ProjectWindowUtil.CreateAsset(asset, "New Event.asset");
        }

        [MenuItem(MenuPath + "Vector2Int", false, 0)]
        private static void CreateEventVector2IntAsset()
        {
            var asset = ScriptableObject.CreateInstance<EventVector2Int>();
            ProjectWindowUtil.CreateAsset(asset, "New Event.asset");
        }

        [MenuItem(MenuPath + "Vector3", false, 0)]
        private static void CreateEventVector3Asset()
        {
            var asset = ScriptableObject.CreateInstance<EventVector3>();
            ProjectWindowUtil.CreateAsset(asset, "New Event.asset");
        }

        [MenuItem(MenuPath + "Vector3Int", false, 0)]
        private static void CreateEventVector3IntAsset()
        {
            var asset = ScriptableObject.CreateInstance<EventVector3Int>();
            ProjectWindowUtil.CreateAsset(asset, "New Event.asset");
        }

        [MenuItem(MenuPath + "Color", false, 0)]
        private static void CreateEventColorAsset()
        {
            var asset = ScriptableObject.CreateInstance<EventColor>();
            ProjectWindowUtil.CreateAsset(asset, "New Event.asset");
        }

        [MenuItem(MenuPath + "Quaternion", false, 0)]
        private static void CreateEventQuaternionAsset()
        {
            var asset = ScriptableObject.CreateInstance<EventQuaternion>();
            ProjectWindowUtil.CreateAsset(asset, "New Event.asset");
        }
    }
}
#endif