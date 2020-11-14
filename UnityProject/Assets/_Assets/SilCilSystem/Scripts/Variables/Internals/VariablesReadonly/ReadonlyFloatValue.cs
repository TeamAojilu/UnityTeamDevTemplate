using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class ReadonlyFloatValue : ReadonlyFloat, IVariableSetter<VariableFloat>
    {
        [SerializeField] private VariableFloat m_variable = default;
        public override float Value => m_variable.Value;

        void IVariableSetter<VariableFloat>.SetVariable(VariableFloat variable)
        {
            m_variable = variable;
        }
    }
}
