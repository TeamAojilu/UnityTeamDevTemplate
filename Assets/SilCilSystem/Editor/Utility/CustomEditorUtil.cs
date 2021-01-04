using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Editors
{
    public static class CustomEditorUtil
    {
        internal static void DisplayMenu(Rect rect, Action<int> onSelected, params string[] contents)
        {
            var menuItems = contents.Select(x => new GUIContent(x)).ToArray();
            var callback = new EditorUtility.SelectMenuItemFunction((_, __, i) => onSelected?.Invoke(i));
            EditorUtility.DisplayCustomMenu(rect, menuItems, -1, callback, null);
        }

        internal static void DisplayMenu(Rect rect, Action<int> onSelected, IEnumerable<string> contents)
        {
            DisplayMenu(rect, onSelected, contents.ToArray());
        }

        internal static void DisplayMenuAtMousePosition(Action<int> onSelected, params string[] contents)
        {
            var rect = new Rect(Event.current.mousePosition, Vector2.zero);
            DisplayMenu(rect, onSelected, contents);
        }

        internal static void DisplayMenuAtMousePosition(Action<int> onSelected, IEnumerable<string> contents)
        {
            var rect = new Rect(Event.current.mousePosition, Vector2.zero);
            DisplayMenuAtMousePosition(onSelected, contents.ToArray());
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
    }
}
