using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class ReadonlyQuaternionValue : ReadonlyQuaternion, IVariableSetter<VariableQuaternion>
    {
        [SerializeField] private VariableQuaternion m_variable = default;
        public override Quaternion Value => m_variable.Value;

        void IVariableSetter<VariableQuaternion>.SetVariable(VariableQuaternion variable)
        {
            m_variable = variable;
        }
    }
}
