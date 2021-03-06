﻿using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Editors
{
    internal static class VariableDragDropActionList
    {
        private static Dictionary<string, VariableDragDropAction> m_list = new Dictionary<string, VariableDragDropAction>();

        [DidReloadScripts(TypeDetector.PreTypeDetectOrder)]
        private static void OnLoad()
        {
            m_list.Clear();
            TypeDetector.OnTypeDetected += AddAction;
        }

        private static void AddAction(Type type)
        {
            if (!typeof(VariableDragDropAction).IsAssignableFrom(type)) return;
            if (type.IsAbstract) return;

            var attr = type.GetCustomAttribute<VariableDragDropAttribute>();
            if (attr == null) return;

            var constructor = type.GetConstructor(Type.EmptyTypes);
            if (constructor == null) return;
            
            m_list.Add(attr.m_path, constructor.Invoke(null) as VariableDragDropAction);
        }

        public static void DisplayMenuAtMousePosition(IEnumerable<VariableAsset> variables) => DisplayMenuAtMousePosition(variables?.ToArray());

        public static void DisplayMenuAtMousePosition(params VariableAsset[] variables)
        {
            if (variables == null) return;
            if (variables.Length == 0) return;

            List<string> paths = new List<string>();
            List<VariableDragDropAction> actions = new List<VariableDragDropAction>();

            var drops = variables.Select(x => x.GetAllVariables()).ToArray();

            foreach(var item in m_list)
            {
                if(drops.All(x => item.Value.IsAccepted(x)))
                {
                    paths.Add(item.Key);
                    actions.Add(item.Value);
                }
            }

            if (paths.Count == 0) return;

            EditorMenuUtil.DisplayMenuAtMousePosition(i => 
            {
                foreach(var variable in variables)
                {
                    actions[i].OnDropExited(variable);
                }
            }, paths);
        }
    }
}