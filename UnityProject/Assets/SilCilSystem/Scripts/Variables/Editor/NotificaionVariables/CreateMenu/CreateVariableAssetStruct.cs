using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
	internal static class CreateVariableAssetStruct
	{
		private const string MenuPath = EditorConstants.CreateAssetMenuPath + "Variables/Struct/";

		[MenuItem(MenuPath + "Vector2", false, 0)]
		private static void CreateVector2Asset()
		{
			var asset = ScriptableObject.CreateInstance<NotificationVector2>();
			ProjectWindowUtil.CreateAsset(asset, "New Variable.asset");
		}

		[MenuItem(MenuPath + "Vector2Int", false, 0)]
		private static void CreateVector2IntAsset()
		{
			var asset = ScriptableObject.CreateInstance<NotificationVector2Int>();
			ProjectWindowUtil.CreateAsset(asset, "New Variable.asset");
		}

		[MenuItem(MenuPath + "Vector3", false, 0)]
		private static void CreateVector3Asset()
		{
			var asset = ScriptableObject.CreateInstance<NotificationVector3>();
			ProjectWindowUtil.CreateAsset(asset, "New Variable.asset");
		}

		[MenuItem(MenuPath + "Vector3Int", false, 0)]
		private static void CreateVector3IntAsset()
		{
			var asset = ScriptableObject.CreateInstance<NotificationVector3Int>();
			ProjectWindowUtil.CreateAsset(asset, "New Variable.asset");
		}

		[MenuItem(MenuPath + "Color", false, 0)]
		private static void CreateColorAsset()
		{
			var asset = ScriptableObject.CreateInstance<NotificationColor>();
			ProjectWindowUtil.CreateAsset(asset, "New Variable.asset");
		}

		[MenuItem(MenuPath + "Quaternion", false, 0)]
		private static void CreateQuaternionAsset()
		{
			var asset = ScriptableObject.CreateInstance<NotificationQuaternion>();
			ProjectWindowUtil.CreateAsset(asset, "New Variable.asset");
		}
	}
}
#endif