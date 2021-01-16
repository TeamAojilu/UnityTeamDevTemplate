using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal static class MonoBehaviourDragDrop
    {
        private const string MenuPath = Constants.DragDropSettingMenuPath + nameof(MonoBehaviour);
        private static HashSet<Rect> m_rects = new HashSet<Rect>();

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
                var options = MonoBehaviourDragDropList.Paths.ToArray();
                EditorMenuUtil.DisplayMenuAtMousePosition(i =>
                {
                    Selection.activeObject = AddComponentToGameObject(options[i], behaviours);
                }, options);
            }
            else
            {
                var menus = MonoBehaviourDragDropList.Paths.ToArray();
                var options = new string[] { "Single object", "Each Object" }.SelectMany(x => menus.Select(m => $"{x}/{m}")).ToArray();
                EditorMenuUtil.DisplayMenuAtMousePosition(i => 
                {
                    int mode = i / menus.Length;
                    int index = i % menus.Length;
                    switch (mode)
                    {
                        case 0:
                            Selection.activeObject = AddComponentToGameObject(menus[index], behaviours);
                            break;
                        case 1:
                            Selection.objects = behaviours.Select(x => AddComponentToGameObject(menus[index], x)).ToArray();
                            break;
                    }
                }, options);
            }
        }

        private static GameObject AddComponentToGameObject(string path, params Type[] behaviours)
        {
            string name = null;
            var obj = MonoBehaviourDragDropList.GetGameObject(path);
            foreach (var behaviour in behaviours)
            {
                name = name ?? $"{behaviour.Name}";
                obj.AddComponent(behaviour);
            }
            obj.name = (string.IsNullOrWhiteSpace(obj.name)) ? name : obj.name;
            return obj;
        }
    }
}