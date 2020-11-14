using UnityEngine;
using SilCilSystem.Variables;

public class ReadonlyVector3 : ReadonlyVariable<Vector3>, IVariableSetter<VariableVector3>
{
    [SerializeField] private VariableVector3 m_variable = default;
    public override Vector3 Value => m_variable.Value;

    void IVariableSetter<VariableVector3>.SetVariable(VariableVector3 variable)
    {
        m_variable = variable;
    }
}
