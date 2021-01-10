using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Readonly", Constants.ReadonlyMenuPath + "(Vector3Int)", typeof(VariableVector3Int))]
    internal class ReadonlyVector3IntValue : ReadonlyVector3Int
    {
        [SerializeField, NotEditable] private VariableVector3Int m_variable = default;

        public override Vector3Int Value => m_variable;

        [OnAttached, Conditional("UNITY_EDITOR")]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableVector3Int>();
        }
    }
}
