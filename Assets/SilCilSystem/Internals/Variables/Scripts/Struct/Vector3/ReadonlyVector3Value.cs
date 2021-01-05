using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [Variable("Readonly", Constants.ReadonlyMenuPath + "(Vector3)", typeof(VariableVector3))]
    internal class ReadonlyVector3Value : ReadonlyVector3
    {
        [SerializeField] private VariableVector3 m_variable = default;

        public override Vector3 Value => m_variable;

        [OnAttached]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableVector3>();
        }
    }
}
