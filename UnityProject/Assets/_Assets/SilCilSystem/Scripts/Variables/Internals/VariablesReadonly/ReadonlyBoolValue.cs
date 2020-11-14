using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class ReadonlyBoolValue : ReadonlyBool, IVariableSetter<VariableBool>
    {
        [SerializeField] private VariableBool m_variable = default;
        public override bool Value => m_variable.Value;

        void IVariableSetter<VariableBool>.SetVariable(VariableBool variable)
        {
            m_variable = variable;
        }
    }
}
