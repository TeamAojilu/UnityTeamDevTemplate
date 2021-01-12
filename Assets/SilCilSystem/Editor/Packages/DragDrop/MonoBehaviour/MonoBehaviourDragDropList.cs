using UnityEngine;
using UnityEditor.Callbacks;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SilCilSystem.Editors
{
    internal static class MonoBehaviourDragDropList
    {
        private static Dictionary<string, Func<GameObject>> m_list = new Dictionary<string, Func<GameObject>>();

        [DidReloadScripts(TypeDetector.PreTypeDetectOrder)]
        private static void OnLoad()
        {
            m_list.Clear();
            TypeDetector.OnTypeDetected -= AddItem;
            TypeDetector.OnTypeDetected += AddItem;
        }

        public static IReadOnlyCollection<string> Paths => m_list.Keys;

        public static GameObject GetGameObject(string path) => m_list[path].Invoke();

        private static void AddItem(Type obj)
        {
            foreach (var method in obj.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (!typeof(GameObject).IsAssignableFrom(method.ReturnType)) continue;
                if (method.GetParameters().Length != 0) continue;
                var attr = method.GetCustomAttribute<MonoBehaviourDragDropAttribute>();
                if (attr == null) return;
                m_list[attr.Path] = () => method.Invoke(null, null) as GameObject;
            }
        }
    }
}