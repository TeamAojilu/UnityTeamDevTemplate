using System;
using System.Diagnostics;

namespace SilCilSystem.Editors
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Class)]
    public class VariableDragDropAttribute : Attribute
    {
        public readonly string m_path;

        public VariableDragDropAttribute(string pathName)
        {
            m_path = pathName;
        }
    }
}