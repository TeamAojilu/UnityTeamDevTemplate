using SilCilSystem.Variables.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SilCilSystem.Editors
{
    public static class CustomEditorUtil
    {
        internal static void AddObjectsToAsset(Object parent, params Object[] children)
        {
            foreach (var child in children)
            {
                AssetDatabase.AddObjectToAsset(child, parent);
            }
        }

        internal static void RenameVariableAssets(Object parentAsset)
        {
            if (AssetDatabase.IsSubAsset(parentAsset)) return;

            var path = AssetDatabase.GetAssetPath(parentAsset);
            var assets = AssetDatabase.LoadAllAssetsAtPath(path);
            if (!assets.All(x => x is VariableAsset)) return;

            bool import = false;
            foreach (var asset in assets)
            {
                if (asset == parentAsset) continue;
                string name = parentAsset.name;
                (asset as VariableAsset).GetAssetName(ref name);
                if (asset.name == name) continue;
                asset.name = name;
                import = true;
            }

            if(import) AssetDatabase.ImportAsset(path);
        }

        internal static void RenameVariableAssets(string parentName, params VariableAsset[] assets)
        {
            foreach (var asset in assets)
            {
                string name = parentName;
                asset.GetAssetName(ref name);
                asset.name = name;
            }
        }

        public static void CreateVariableAsset<TVariable>(string name, params Type[] children) where TVariable : VariableAsset
        {
            var variable = ScriptableObject.CreateInstance<TVariable>();
            var instanceID = variable.GetInstanceID();
            var icon = AssetPreview.GetMiniThumbnail(variable);
            var endNameEditAction = ScriptableObject.CreateInstance<VariableCreateAction>();
            endNameEditAction.m_types = children;
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(instanceID, endNameEditAction, name, icon, "");
        }
        
        public static void AttachVariableAssets(VariableAsset parent, IEnumerable<Type> attachTypes)
        {
            var attaches = attachTypes.Where(x => typeof(VariableAsset).IsAssignableFrom(x)).ToArray();

            var path = AssetDatabase.GetAssetPath(parent);
            var variables = AssetDatabase.LoadAllAssetsAtPath(path).Where(x => x is VariableAsset).OfType<VariableAsset>().ToList();
            var hideFlags = variables.FirstOrDefault(x => x != parent)?.hideFlags ?? HideFlags.HideInHierarchy;

            var children = attaches.Select(x => ScriptableObject.CreateInstance(x) as VariableAsset).ToArray();
            AddObjectsToAsset(parent, children);
            RenameVariableAssets(parent.name, children);

            variables.AddRange(children);
            foreach (var child in children)
            {
                child.hideFlags = hideFlags;
                child.OnAttached(variables);
            }
        }
    }
}
