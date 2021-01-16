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
            EditorApplication.delayCall += DelayCall;
        }

        private static void DelayCall()
        {
            SetActive();
            EditorApplication.delayCall -= DelayCall;
        }

        [MenuItem(MenuPath)]
        private static void OnStateChanged()
        {
            EditorPrefs.SetBool(MenuPath, !EditorPrefs.GetBool(MenuPath, true));
            SetActive();
        }

        private static void SetActive()
        {
            bool active = EditorPrefs.GetBool(MenuPath, true);
            Menu.SetChecked(MenuPath, active);

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