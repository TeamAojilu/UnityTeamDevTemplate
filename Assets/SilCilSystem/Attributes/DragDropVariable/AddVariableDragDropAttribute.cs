using System;
using System.Diagnostics;

namespace SilCilSystem.Editors
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Class)]
    public class AddVariableDragDropAttribute : Attribute
    {
        public readonly string m_path;

        public AddVariableDragDropAttribute(string pathName)
        {
            m_path = pathName;
        }
    }
}