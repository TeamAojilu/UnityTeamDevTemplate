#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Callbacks;

namespace SilCilSystem.Editors
{
    internal static class TypeDetector
    {
        public const int PreTypeDetectOrder = 0;
        private const int TypeDetectOrder = PreTypeDetectOrder + 1;

        private readonly static HashSet<string> m_exceptions = new HashSet<string>()
        {
            "System",
            "UnityEngine",
            "UnityEditor",
            "Cinemachine",
            "com.unity.cinemachine.editor",
            "SyntaxTree.VisualStudio.Unity.Bridge",
            "SyntaxTree.VisualStudio.Unity.Messaging",
            "ExCSS.Unity",
            "nunit.framework",
            "ICSharpCode.NRefactory",
            "mscorlib",
        };

        public static event Action<Type> OnTypeDetected;

        private static bool IsExcepted(Assembly assembly)
        {
            string name = assembly?.GetName()?.Name;
            if (name == null) return true;
            if (m_exceptions.Contains(name)) return true;
            if (name.StartsWith("System.")) return true;
            if (name.StartsWith("Unity.")) return true;
            if (name.StartsWith("UnityEditor.")) return true;
            if (name.StartsWith("UnityEngine.")) return true;
            return false;
        }

        [DidReloadScripts(TypeDetectOrder)]
        private static void LoadAssemblies()
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
#endif