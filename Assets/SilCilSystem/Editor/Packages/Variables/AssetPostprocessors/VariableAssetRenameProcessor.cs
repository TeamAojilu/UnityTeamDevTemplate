using UnityEditor;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Editors
{
    internal class VariableAssetRenameProcessor : AssetPostprocessor
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
            if (changedAssets == null) return;
            if (changedAssets.Length == 0) return;

            foreach (var changed in changedAssets)
            {
                var parent = AssetDatabase.LoadAssetAtPath<VariableAsset>(changed);
                if (parent == null) continue;
                parent.RenameSubVariables();
            }
        }
    }
}