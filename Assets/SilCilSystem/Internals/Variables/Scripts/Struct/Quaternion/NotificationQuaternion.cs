using UnityEngine;
using System.Collections.Generic;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    internal class NotificationQuaternion : VariableQuaternion
    {
        [SerializeField] private Quaternion m_value = default;
        [SerializeField] private GameEventQuaternion m_onValueChanged = default;

        public override void GetAssetName(ref string name) => name = $"{name}_Variable";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is GameEventQuaternion onChanged)
                {
                    m_onValueChanged = onChanged;
                    return;
                }
            }
        }

        public override Quaternion Value
        {
            get => m_value;
            set
            {
                m_value = value;
                m_onValueChanged?.Publish(m_value);
            }
        }
    }
}