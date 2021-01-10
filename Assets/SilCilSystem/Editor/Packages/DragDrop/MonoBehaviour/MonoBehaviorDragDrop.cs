using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal static class MonoBehaviorDragDrop
    {
        private const string MenuPath = Constants.DragDropSettingMenuPath + nameof(MonoBehaviour);
        private static HashSet<Rect> m_rects = new HashSet<Rect>();

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
            EditorApplication.hierarchyChanged -= OnHierarchyChanged;
            if (active)
            {
                EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
                EditorApplication.hierarchyChanged += OnHierarchyChanged;
            }
        }
        
        private static void OnHierarchyChanged()
        {
            m_rects.Clear();
        }

        private static void OnGUI(int instanceID, Rect rect)
        {
            // 通常のスクリプトアタッチを邪魔しないようにする.
            if (m_rects.Add(rect)) return;
            if (m_rects.Any(x => x.Contains(Event.current.mousePosition))) return;

            if (DragAndDrop.objectReferences == null) return;
            if (DragAndDrop.objectReferences.Length == 0) return;
            
            var behaviours = DragAndDrop.objectReferences
                        .Where(x => x.GetType() == typeof(MonoScript))
                        .OfType<MonoScript>()
                        .Select(x => x.GetClass())
                        .Where(x => typeof(MonoBehaviour).IsAssignableFrom(x))
                        .ToArray();

            if (behaviours.Length == 0) return;
            
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            if (Event.current.type != EventType.DragPerform) return;

            DragAndDrop.AcceptDrag();
            Event.current.Use();

            if (behaviours.Length == 1)
            {
                Selection.activeObject = CreateGameObject(behaviours);
            }
            else
            {
                EditorMenuUtil.DisplayMenuAtMousePosition(i =>
                {
                    if (i == 0) Selection.activeObject = CreateGameObject(behaviours);
                    if (i == 1) Selection.objects = behaviours.Select(x => CreateGameObject(x)).ToArray();
                }, "Single object", "Each object");
            }
        }

        private static GameObject CreateGameObject(params Type[] behaviours)
        {
            GameObject obj = new GameObject();
            obj.name = null;
            foreach (var behaviour in behaviours)
            {
                obj.name = (string.IsNullOrWhiteSpace(obj.name)) ? behaviour.Name : obj.name;
                obj.AddComponent(behaviour);
            }
            Undo.RegisterCreatedObjectUndo(obj, $"Create {obj.name}");
            return obj;
        }
    }
}