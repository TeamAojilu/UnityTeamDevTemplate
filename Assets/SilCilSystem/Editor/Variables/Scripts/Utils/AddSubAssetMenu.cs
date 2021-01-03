using System.Reflection;
using System.Collections;
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

        public static AddSubAssetMenuAttribute[] GetAddSubAssetMenu(Type target)
        {
            return m_attributes.Where(x => x.m_targets.Any(t => t.IsAssignableFrom(target))).ToArray();
        }

        public static void DisplayAddSubAssetMenu(Rect rect, Action onAttached = null)
        {
            if  (!(Selection.activeObject is VariableAsset target)) return;

            var menuItems = AddSubAssetMenu.GetAddSubAssetMenu(target.GetType());
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