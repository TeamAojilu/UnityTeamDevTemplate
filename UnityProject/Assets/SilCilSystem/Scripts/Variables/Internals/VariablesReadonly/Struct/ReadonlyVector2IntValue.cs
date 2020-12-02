using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class ReadonlyVector2IntValue : ReadonlyVector2Int, IVariableSetter<VariableVector2Int>
    {
        [SerializeField] private VariableVector2Int m_variable = default;
        public override Vector2Int Value => m_variable.Value;

        void IVariableSetter<VariableVector2Int>.SetVariable(VariableVector2Int variable)
        {
            m_variable = variable;
        }
    }
}
