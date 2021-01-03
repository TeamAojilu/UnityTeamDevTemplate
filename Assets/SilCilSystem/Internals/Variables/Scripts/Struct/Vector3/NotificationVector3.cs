using UnityEngine;
using System.Collections.Generic;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{ 
    internal class NotificationVector3 : VariableVector3
    {
        [SerializeField] private Vector3 m_value = default;
        [SerializeField] private GameEventVector3 m_onValueChanged = default;

        public override void GetAssetName(ref string name) => name = $"{name}_Variable";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is GameEventVector3 onChanged)
                {
                    m_onValueChanged = onChanged;
                    return;
                }
            }
        }

        public override Vector3 Value
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