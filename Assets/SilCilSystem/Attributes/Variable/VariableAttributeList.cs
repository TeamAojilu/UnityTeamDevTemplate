using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Editors
{
    internal static class VariableAttributeList
    {
        private static Dictionary<Type, SubVariableAction> m_list = new Dictionary<Type, SubVariableAction>();
        
        private class SubVariableAction
        {
            public VariableAttribute Attribute { get; private set; }
            private List<MethodInfo> m_onAttached = new List<MethodInfo>();

            public SubVariableAction(VariableAttribute attribute)
            {
                Attribute = attribute;
            }

            public void TryAddMethod(MethodInfo method)
            {
                if (method.GetCustomAttribute<OnAttachedAttribute>() == null) return;
                if (method.ReturnParameter.ParameterType != typeof(void)) return;
                var parameters = method.GetParameters();
                if (parameters.Length != 1) return;
                if (parameters[0].ParameterType != typeof(VariableAsset)) return;
                
                m_onAttached.Add(method);
            }

            public void CallAttached(object subAsset, VariableAsset parent)
            {
                foreach(var onAttached in m_onAttached)
                {
                    onAttached?.Invoke(subAsset, new object[] { parent });
                }
            }
        }

#if UNITY_EDITOR
        [UnityEditor.Callbacks.DidReloadScripts(TypeDetector.PreTypeDetectOrder)]
        private static void OnLoad()
        {
            m_list.Clear();
            TypeDetector.OnTypeDetected += AddItem;
        }

        private static void AddItem(Type type)
        {
            if (!typeof(VariableAsset).IsAssignableFrom(type)) return;
            if (type.IsAbstract) return;

            var attr = type.GetCustomAttribute<VariableAttribute>();
            if (attr == null) return;

            var item = new SubVariableAction(attr);
            foreach(var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod))
            {
                item.TryAddMethod(method);
            }
            m_list[type] = item;
        }
#endif

        public static string GetName(VariableAsset subAsset, string name)
        {
            foreach(var item in m_list)
            {
                if (subAsset?.GetType() != item.Key) continue;
                return item.Value.Attribute.GetName(name);
            }
            return null;
        }

        [Conditional("UNITY_EDITOR")]
        public static void CallAttached(VariableAsset subAsset, VariableAsset parent)
        {
            foreach(var item in m_list)
            {
                if (subAsset?.GetType() != item.Key) continue;
                item.Value.CallAttached(subAsset, parent);
                return;
            }
        }

        public static void GetEnableTypes(VariableAsset parent, out List<string> menuPaths, out List<Type> types)
        {
            menuPaths = new List<string>();
            types = new List<Type>();
            foreach(var item in m_list)
            {
                if (item.Value.Attribute.CanBeChild(parent.GetAllVariables()))
                {
                    menuPaths.Add(item.Value.Attribute.MenuPath ?? item.Value.GetType().Name);
                    types.Add(item.Key);
                }
            }
        }
    }
}