using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(Constants.ReadonlyMenuPath + "(Quaternion)", typeof(VariableQuaternion))]
    internal class ReadonlyQuaternionValue : ReadonlyQuaternion
    {
        [SerializeField] private VariableQuaternion m_variable = default;

        public override Quaternion Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<VariableQuaternion>();
        }
    }
}
