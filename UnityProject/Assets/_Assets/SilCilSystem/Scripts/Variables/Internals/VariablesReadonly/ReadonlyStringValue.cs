using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class ReadonlyStringValue : ReadonlyString, IVariableSetter<VariableString>
    {
        [SerializeField] internal VariableString m_variable = default;
        public override string Value => m_variable.Value;

        void IVariableSetter<VariableString>.SetVariable(VariableString variable)
        {
            m_variable = variable;
        }
    }
}
