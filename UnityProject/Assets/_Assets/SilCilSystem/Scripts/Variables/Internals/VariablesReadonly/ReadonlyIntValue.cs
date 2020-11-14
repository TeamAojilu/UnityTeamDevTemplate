using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class ReadonlyIntValue : ReadonlyInt, IVariableSetter<VariableInt>
    {
        [SerializeField] internal VariableInt m_variable = default;
        public override int Value => m_variable.Value;

        void IVariableSetter<VariableInt>.SetVariable(VariableInt variable)
        {
            m_variable = variable;
        }
    }
}
