using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Editors
{
    internal static class AddSubAssetMenu
    {
        private static List<AddSubAssetMenuAttribute> m_attributes = new List<AddSubAssetMenuAttribute>();
        
        [DidReloadScripts(TypeDetector.PreTypeDetectOrder)]
        public static void OnLoad()
        {
            m_attributes.Clear();
            TypeDetector.OnTypeDetected += AddAttribute;
        }

        private static void AddAttribute(Type type)
        {
            if (!typeof(VariableAsset).IsAssignableFrom(type)) return;
            if (type.IsAbstract) return;

            var attr = type.GetCustomAttribute<AddSubAssetMenuAttribute>();
            if (attr == null) return;

            attr.AttachedType = type;
            m_attributes.Add(attr);
        }
        
        public static void DisplayAddSubAssetMenu(Rect rect, Action onAttached = null)
        {
            if  (!(Selection.activeObject is VariableAsset target)) return;

            var menuItems = GetAddSubAssetMenu(Selection.objects);
            CustomEditorUtil.DisplayMenu(rect, i => 
            {
                foreach (var obj in Selection.objects)
                {
                    if (obj is VariableAsset parent)
                    {
                        parent.AddSubVariable(menuItems[i].AttachedType);
                    }
                }
                onAttached?.Invoke();
            }, menuItems.Select(x => x.m_menuPath));
        }

        private static AddSubAssetMenuAttribute[] GetAddSubAssetMenu(params UnityEngine.Object[] targets)
        {
            List<AddSubAssetMenuAttribute> lists = new List<AddSubAssetMenuAttribute>();
            var targetTypesArray = targets.Where(x => x is VariableAsset).Select(x => (x as VariableAsset).GetAllVariables().Select(z => z.GetType())).ToArray();
            foreach (var attr in m_attributes)
            {
                if(targetTypesArray.All(targetTypes => targetTypes.Any(targetType => attr.m_targets.Any(type => type.IsAssignableFrom(targetType)))))
                {
                    lists.Add(attr);
                }
            }
            return lists.ToArray();
        }
    }
}