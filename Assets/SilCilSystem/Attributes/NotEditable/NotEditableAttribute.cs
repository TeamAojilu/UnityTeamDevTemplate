using System;
using UnityEngine;

namespace SilCilSystem.Editors
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class NotEditableAttribute : PropertyAttribute { }
}