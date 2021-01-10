using System.Linq;
using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Editors
{
    internal static class VariableDragDrop
    {
        private const string MenuPath = Constants.DragDropSettingMenuPath + nameof(VariableAsset);

        [InitializeOnLoadMethod]
        private static void OnLoad()
        {
            EditorApplication.delayCall += () => Menu.SetChecked(MenuPath, EditorPrefs.GetBool(MenuPath, true));
            SetActive();
        }

        [MenuItem(MenuPath)]
        private static void OnStateChanged()
        {
            bool isChecked = !EditorPrefs.GetBool(MenuPath, Menu.GetChecked(MenuPath));
            EditorPrefs.SetBool(MenuPath, isChecked);
            SetActive();
        }

        private static void SetActive()
        {
            bool active = EditorPrefs.GetBool(MenuPath, true);
            EditorApplication.hierarchyWindowItemOnGUI -= OnGUI;
            if (active)
            {
                EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
            }
        }
        
        private static void OnGUI(int instanceID, Rect rect)
        {
            if (DragAndDrop.objectReferences == null) return;
            if (DragAndDrop.objectReferences.Length == 0) return;
            
            var variables = DragAndDrop.objectReferences
                        .Where(x => typeof(VariableAsset).IsAssignableFrom(x.GetType()))
                        .ToArray();

            if (variables.Length == 0) return;
            
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

            if (Event.current.type != EventType.DragPerform) return;
            DragAndDrop.AcceptDrag();
            Event.current.Use();

            VariableDragDropActionList.DisplayMenuAtMousePosition(variables.Select(x => x as VariableAsset));
            EditorApplication.RepaintHierarchyWindow();
        }
    }
}