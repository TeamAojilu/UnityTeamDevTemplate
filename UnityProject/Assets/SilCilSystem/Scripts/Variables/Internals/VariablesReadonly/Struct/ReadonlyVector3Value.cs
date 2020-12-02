using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class ReadonlyVector3Value : ReadonlyVector3, IVariableSetter<VariableVector3>
    {
        [SerializeField] private VariableVector3 m_variable = default;
        public override Vector3 Value => m_variable.Value;

        void IVariableSetter<VariableVector3>.SetVariable(VariableVector3 variable)
        {
            m_variable = variable;
        }
    }
}
