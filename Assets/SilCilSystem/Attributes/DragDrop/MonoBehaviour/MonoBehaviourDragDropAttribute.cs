using System;
using System.Diagnostics;

namespace SilCilSystem.Editors
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Method)]
    public class MonoBehaviourDragDropAttribute : Attribute
    {
        private readonly string m_path;
        public string Path => m_path;
        public MonoBehaviourDragDropAttribute(string path)
        {
            m_path = path;
        }
    }
}