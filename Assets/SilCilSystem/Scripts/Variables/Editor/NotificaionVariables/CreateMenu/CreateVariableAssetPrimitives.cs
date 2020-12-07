using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
	internal static class CreateVariableAssetPrimitives
	{
		private const string MenuPath = EditorConstants.CreateAssetMenuPath + "Variables/Primitives/";

		[MenuItem(MenuPath + "Int", false, 0)]
		private static void CreateIntAsset()
		{
			var asset = ScriptableObject.CreateInstance<NotificationInt>();
			ProjectWindowUtil.CreateAsset(asset, "New Variable.asset");
		}

		[MenuItem(MenuPath + "Bool", false, 0)]
		private static void CreateBoolAsset()
		{
			var asset = ScriptableObject.CreateInstance<NotificationBool>();
			ProjectWindowUtil.CreateAsset(asset, "New Variable.asset");
		}

		[MenuItem(MenuPath + "String", false, 0)]
		private static void CreateStringAsset()
		{
			var asset = ScriptableObject.CreateInstance<NotificationString>();
			ProjectWindowUtil.CreateAsset(asset, "New Variable.asset");
		}

		[MenuItem(MenuPath + "Float", false, 0)]
		private static void CreateFloatAsset()
		{
			var asset = ScriptableObject.CreateInstance<NotificationFloat>();
			ProjectWindowUtil.CreateAsset(asset, "New Variable.asset");
		}
	}
}
#endif