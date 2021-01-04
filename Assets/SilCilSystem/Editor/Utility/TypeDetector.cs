using System;
using System.Reflection;
using UnityEditor.Callbacks;

namespace SilCilSystem.Editors
{
    internal static class TypeDetector
    {
        public const int PreTypeDetectOrder = 0;
        private const int TypeDetectOrder = PreTypeDetectOrder + 1;

        public static event Action<Type> OnTypeDetected;

        private static bool IsExcepted(Assembly assembly)
        {
            string name = assembly?.GetName()?.Name;
            if (name == null) return true;
            if (name == "System") return true;
            if (name == "UnityEngine") return true;
            if (name == "UnityEditor") return true;
            if (name.StartsWith("System.")) return true;
            if (name.StartsWith("Unity.")) return true;
            if (name.StartsWith("UnityEditor.")) return true;
            if (name.StartsWith("UnityEngine.")) return true;
            return false;
        }
        
        [DidReloadScripts(TypeDetectOrder)]
        public static void OnLoad()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (IsExcepted(assembly)) continue;
                foreach (var type in assembly.GetTypes())
                {
                    OnTypeDetected?.Invoke(type);
                }
            }
        }
    }
}