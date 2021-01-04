using UnityEditor.ProjectWindowCallback;
using UnityEditor;
using SilCilSystem.Variables.Base;
using System;

namespace SilCilSystem.Editors
{
    internal class VariableCreateAction : EndNameEditAction
    {
        internal Type[] m_types = default;
        
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            var variable = EditorUtility.InstanceIDToObject(instanceId) as VariableAsset;
            if (variable == null) return;

            AssetDatabase.CreateAsset(variable, pathName);
            variable.AddSubVariables(m_types);
            variable.OnAttached(variable);

            AssetDatabase.ImportAsset(pathName);
            ProjectWindowUtil.ShowCreatedAsset(variable);
        }
    }
}