using UnityEditor;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Editors
{
    public class VariableAssetRenameProcessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            RenameChildren(importedAssets);
            RenameChildren(movedAssets);
            if(deletedAssets != null && deletedAssets.Length != 0)
            {
                VariableInspectorOrders.RemoveNull();
            }
        }

        private static void RenameChildren(string[] changedAssets)
        {
            foreach (var changed in changedAssets)
            {
                var parent = AssetDatabase.LoadAssetAtPath<VariableAsset>(changed);
                if (parent == null) continue;
                var assets = AssetDatabase.LoadAllAssetsAtPath(changed);
                CustomEditorUtil.RenameVariableAssets(parent);
            }
        }
    }
}