using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables.Generic;

[AddSubAssetMenu("ToFloat", typeof(Variable<int>))]
public class IntToFloat : ReadonlyFloat
{
    [SerializeField] private ReadonlyInt m_variable = default;

    public override float Value => m_variable;

    public override void GetAssetName(ref string name) => name = $"{name}_ToFloat";

    public override void OnAttached(IEnumerable<VariableAsset> variables)
    {
        foreach(var variable in variables)
        {
            if(variable is ReadonlyInt value)
            {
                m_variable = value;
                return;
            }
        }
    }
}
