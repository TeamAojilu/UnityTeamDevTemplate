using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class ReadonlyVector3IntValue : ReadonlyVector3Int, IVariableSetter<VariableVector3Int>
    {
        [SerializeField] private VariableVector3Int m_variable = default;
        public override Vector3Int Value => m_variable.Value;

        void IVariableSetter<VariableVector3Int>.SetVariable(VariableVector3Int variable)
        {
            m_variable = variable;
        }
    }
}
