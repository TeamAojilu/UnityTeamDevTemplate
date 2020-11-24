using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class ReadonlyVector2Value : ReadonlyVector2, IVariableSetter<VariableVector2>
    {
        [SerializeField] private VariableVector2 m_variable = default;
        public override Vector2 Value => m_variable.Value;

        void IVariableSetter<VariableVector2>.SetVariable(VariableVector2 variable)
        {
            m_variable = variable;
        }
    }
}
