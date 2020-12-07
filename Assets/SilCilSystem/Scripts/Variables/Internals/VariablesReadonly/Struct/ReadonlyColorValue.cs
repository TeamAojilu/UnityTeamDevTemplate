using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class ReadonlyColorValue : ReadonlyColor, IVariableSetter<VariableColor>
    {
        [SerializeField] private VariableColor m_variable = default;
        public override Color Value => m_variable.Value;

        void IVariableSetter<VariableColor>.SetVariable(VariableColor variable)
        {
            m_variable = variable;
        }
    }
}
