using UnityEditor.ProjectWindowCallback;
using UnityEditor;
using SilCilSystem.Variables.Base;
using System;
using System.Linq;

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
            CustomEditorUtil.AttachVariableAssets(variable, m_types);
            var variables = AssetDatabase.LoadAllAssetsAtPath(pathName);
            variable.OnAttached(variables.Where(x => x is VariableAsset).Select(x => x as VariableAsset));
            AssetDatabase.ImportAsset(pathName);
            ProjectWindowUtil.ShowCreatedAsset(variable);
        }
    }
}