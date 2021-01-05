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
        private static bool m_willCreate = false;

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
            if (!m_rects.Contains(rect))
            {
                m_rects.Add(rect);
                return;
            }

            // 通常のスクリプトアタッチを邪魔しないようにする.
            if (m_rects.Any(x => x.Contains(Event.current.mousePosition))) return;

            if (DragAndDrop.objectReferences == null) return;
            if (DragAndDrop.objectReferences.Length == 0) return;
            
            var instance = EditorUtility.InstanceIDToObject(instanceID);
            
            var behaviours = DragAndDrop.objectReferences
                        .Where(x => x.GetType() == typeof(MonoScript))
                        .OfType<MonoScript>()
                        .Select(x => x.GetClass())
                        .Where(x => typeof(MonoBehaviour).IsAssignableFrom(x))
                        .ToArray();

            if (behaviours.Length == 0) return;
            
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

            switch (Event.current.type)
            {
                case EventType.DragPerform:
                    m_willCreate = true;
                    break;
                case EventType.DragExited:
                    if (m_willCreate)
                    {
                        m_willCreate = false;

                        if (behaviours.Length == 1)
                        {
                            CreateEachGameObject(behaviours);
                        }
                        else
                        {
                            EditorMenuUtil.DisplayMenuAtMousePosition(i => 
                            {
                                if (i == 0) CreateSingleGameObject(behaviours);
                                if (i == 1) CreateEachGameObject(behaviours);
                            }, "Single object", "Each object");
                        }

                        EditorApplication.RepaintHierarchyWindow();
                    }
                    break;
            }
        }

        private static void CreateSingleGameObject(Type[] behaviours)
        {
            GameObject obj = new GameObject();
            obj.name = null;
            foreach (var behaviour in behaviours)
            {
                obj.name = (string.IsNullOrWhiteSpace(obj.name)) ? behaviour.Name : obj.name;
                obj.AddComponent(behaviour);
            }
        }

        private static void CreateEachGameObject(System.Type[] behaviours)
        {
            foreach (var behaviour in behaviours)
            {
                GameObject obj = new GameObject();
                obj.name = behaviour.Name;
                obj.AddComponent(behaviour);
            }
        }
    }
}