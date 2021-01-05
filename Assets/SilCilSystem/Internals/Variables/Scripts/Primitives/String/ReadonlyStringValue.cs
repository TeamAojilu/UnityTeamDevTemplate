using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [Variable("Readonly", Constants.ReadonlyMenuPath + "(String)", typeof(VariableString))]
    internal class ReadonlyStringValue : ReadonlyString
    {
        [SerializeField] private VariableString m_variable = default;

        public override string Value => m_variable;
        
        [OnAttached]
        private void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableString>();
        }
    }
}
