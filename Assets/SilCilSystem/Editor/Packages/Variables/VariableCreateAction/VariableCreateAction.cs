using UnityEditor.ProjectWindowCallback;
using UnityEditor;
using SilCilSystem.Variables.Base;
using System;
using UnityEngine;

namespace SilCilSystem.Editors
{
    public static class VariableCreateAction
    {
        public static void CreateAsset<TVariable>(string name, params Type[] children) where TVariable : VariableAsset
        {
            var variable = ScriptableObject.CreateInstance<TVariable>();
            var instanceID = variable.GetInstanceID();
            var icon = AssetPreview.GetMiniThumbnail(variable);
            var endNameEditAction = ScriptableObject.CreateInstance<EndAction>();
            endNameEditAction.m_types = children;
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(instanceID, endNameEditAction, name, icon, "");
        }

        private class EndAction : EndNameEditAction
        {
            internal Type[] m_types = default;

            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var variable = EditorUtility.InstanceIDToObject(instanceId) as VariableAsset;
                if (variable == null) return;

                AssetDatabase.CreateAsset(variable, pathName);
                variable.AddSubVariables(false, m_types);
                VariableAttributeList.CallAttached(variable, variable);

                AssetDatabase.ImportAsset(pathName);
                ProjectWindowUtil.ShowCreatedAsset(variable);
            }
        }
    }
}