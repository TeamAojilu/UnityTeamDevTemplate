using System;
using System.Diagnostics;

namespace SilCilSystem.Editors
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumLabelAttribute : Attribute
    {
        private readonly string m_displayName = default;
        public string DisplayName => m_displayName;
        public EnumLabelAttribute(string displayName)
        {
            m_displayName = displayName;
        }
    }
}