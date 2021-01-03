using System;
using System.Diagnostics;

namespace SilCilSystem.Editors
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Class)]
    public class AddSubAssetMenuAttribute : Attribute
    {
        public readonly Type[] m_targets;
        public readonly string m_menuPath;

        internal Type AttachedType { get; set; }

        public AddSubAssetMenuAttribute(string menuPath, params Type[] targets)
        {
            m_targets = targets;
            m_menuPath = menuPath;
        }
    }
}