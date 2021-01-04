using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    internal class NotificationQuaternion : VariableQuaternion
    {
        [SerializeField] private Quaternion m_value = default;
        [SerializeField] private GameEventQuaternion m_onValueChanged = default;

        public override Quaternion Value
        {
            get => m_value;
            set
            {
                m_value = value;
                m_onValueChanged?.Publish(m_value);
            }
        }

        public override void GetAssetName(ref string name) => name = $"{name}_Variable";
        public override void OnAttached(VariableAsset parent)
        {
            m_onValueChanged = parent.GetSubVariable<GameEventQuaternion>();
        }
    }
}