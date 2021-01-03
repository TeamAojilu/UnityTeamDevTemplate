using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables.Base;
using System;
using System.Linq;

namespace SilCilSystem.Editors
{
    internal static class AddSubAssetMenu
    {
        private static List<AddSubAssetMenuAttribute> m_attributes = new List<AddSubAssetMenuAttribute>();
        
        [InitializeOnLoadMethod]
        public static void OnLoad()
        {
            m_attributes.Clear();
            
            foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (!typeof(VariableAsset).IsAssignableFrom(type)) continue;
                    if (type.IsAbstract) continue;
                    var attr = type.GetCustomAttribute<AddSubAssetMenuAttribute>();
                    if (attr == null) continue;
                    attr.AttachedType = type;
                    m_attributes.Add(attr);
                }
            }
        }

        private static AddSubAssetMenuAttribute[] GetAddSubAssetMenu(params UnityEngine.Object[] targets)
        {
            List<AddSubAssetMenuAttribute> lists = new List<AddSubAssetMenuAttribute>();
            var targetTypesArray = targets.Select(x => AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(x)).Where(y => y is VariableAsset).Select(z => z.GetType())).ToArray();
            foreach (var attr in m_attributes)
            {
                if(targetTypesArray.All(targetTypes => targetTypes.Any(targetType => attr.m_targets.Any(type => type.IsAssignableFrom(targetType)))))
                {
                    lists.Add(attr);
                }
            }
            return lists.ToArray();
        }

        public static void DisplayAddSubAssetMenu(Rect rect, Action onAttached = null)
        {
            if  (!(Selection.activeObject is VariableAsset target)) return;

            var menuItems = GetAddSubAssetMenu(Selection.objects);
            GUIContent[] options = menuItems.Select(x => new GUIContent(x.m_menuPath)).ToArray();
            var callback = new EditorUtility.SelectMenuItemFunction((_, __, i) =>
            {
                foreach (var obj in Selection.objects)
                {
                    if (obj is VariableAsset parent)
                    {
                        CustomEditorUtil.AttachVariableAssets(parent, menuItems[i].AttachedType);
                        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(obj));
                    }
                }
                onAttached?.Invoke();
            });
            EditorUtility.DisplayCustomMenu(rect, options, -1, callback, null);
        }
    }
}