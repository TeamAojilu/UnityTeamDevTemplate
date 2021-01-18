using System;
using System.Diagnostics;
using UnityEngine;

namespace SilCilSystem.Editors
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class NonEditableAttribute : PropertyAttribute { }
}