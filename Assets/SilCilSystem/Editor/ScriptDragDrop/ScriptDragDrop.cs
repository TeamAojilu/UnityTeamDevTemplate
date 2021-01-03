using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

namespace SilCilSystem.Editors
{
    public static class ScriptDragDrop
    {
        private static HashSet<Rect> m_rects = new HashSet<Rect>();
        private static bool m_willCreate = false;

        [InitializeOnLoadMethod]
        private static void OnLoad()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= OnGUI;
            EditorApplication.hierarchyChanged -= OnHierarchyChanged;
            EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
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
                        foreach (var behaviour in behaviours)
                        {
                            GameObject obj = new GameObject();
                            obj.name = behaviour.Name;
                            obj.AddComponent(behaviour);
                        }
                        EditorApplication.RepaintHierarchyWindow();
                    }
                    break;
            }
        }
    }
}