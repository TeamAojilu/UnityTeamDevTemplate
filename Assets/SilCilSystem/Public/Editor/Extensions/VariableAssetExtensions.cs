using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Editors
{
#if UNITY_EDITOR
    public static class VariableAssetExtensions
    {
        public static T GetSubVariable<T>(this VariableAsset parent) where T : VariableAsset
        {
            return GetSubVariableCollection<T>(parent).FirstOrDefault();
        }

        public static VariableAsset GetSubVariable(this VariableAsset parent, Type type)
        {
            return GetSubVariableCollection(parent, type).FirstOrDefault();
        }

        public static T[] GetSubVariables<T>(this VariableAsset parent) where T : VariableAsset
        {
            return GetSubVariableCollection<T>(parent).ToArray();
        }

        public static VariableAsset[] GetSubVariables(this VariableAsset parent, Type type)
        {
            return GetSubVariableCollection(parent, type).ToArray();
        }

        public static VariableAsset[] GetAllVariables(this VariableAsset parent)
        {
            return GetSubVariableCollection<VariableAsset>(parent).ToArray();
        }

        public static void AddSubVariable<T>(this VariableAsset parent) where T : VariableAsset
        {
            var asset = ScriptableObject.CreateInstance<T>();
            SetSubVariable(parent, asset);
        }
        
        public static void AddSubVariable(this VariableAsset parent, Type type)
        {
            var asset = ScriptableObject.CreateInstance(type) as VariableAsset;
            SetSubVariable(parent, asset);
        }

        public static void AddSubVariables(this VariableAsset parent, params Type[] types)
        {
            List<VariableAsset> assets = new List<VariableAsset>();
            foreach(var type in types)
            {
                var asset = ScriptableObject.CreateInstance(type) as VariableAsset;
                SetSubVariable(parent, asset, false);
                assets.Add(asset);
            }

            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(parent));
            foreach(var asset in assets)
            {
                asset.OnAttached(parent);
            }
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(parent));
        }

        internal static void RenameSubVariables(this VariableAsset parent)
        {
            if (AssetDatabase.IsSubAsset(parent)) return;
            
            // 名前が変わった場合のみImportする.
            bool import = false;
            foreach (var asset in parent.GetAllVariables())
            {
                if (asset == parent) continue;

                string name = asset.name;
                SetName(asset, parent.name);

                if (asset.name == name) continue;
                import = true;
            }

            if (import) AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(parent));
        }

        private static void SetSubVariable(VariableAsset parent, VariableAsset asset, bool importAndOnAttached = true)
        {
            SetName(asset, parent.name);
            asset.hideFlags = HideFlags.HideInHierarchy;

            AssetDatabase.AddObjectToAsset(asset, parent);

            if (importAndOnAttached)
            {
                asset.OnAttached(parent);
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(parent));
            }
        }

        private static bool SetName(VariableAsset asset, string parentName)
        {
            string name = parentName;
            asset.GetAssetName(ref name);
            if (name == parentName) name += "_"; // 親と同じ名前だとアセットのパスが重複してしまうので、変える.

            if (name != asset.name)
            {
                asset.name = name;
                return true;
            }
            return false;
        }

        private static IEnumerable<T> GetSubVariableCollection<T>(VariableAsset parent)
        {
            string path = AssetDatabase.GetAssetPath(parent);
            foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(path))
            {
                if (asset is T value)
                {
                    yield return value;
                }
            }
        }

        private static IEnumerable<VariableAsset> GetSubVariableCollection(VariableAsset parent, Type type)
        {
            string path = AssetDatabase.GetAssetPath(parent);
            foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(path))
            {
                if (asset is VariableAsset value && type.IsAssignableFrom(asset.GetType()))
                {
                    yield return value;
                }
            }
        }
    }
#endif
}