using UnityEngine;
using System.Collections.Generic;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    internal class NotificationInt : VariableInt
    {
        [SerializeField] private int m_value = default;
        [SerializeField] private GameEventInt m_onValueChanged = default;

        public override void GetAssetName(ref string name) => name = $"{name}_Variable";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is GameEventInt onChanged)
                {
                    m_onValueChanged = onChanged;
                    return;
                }
            }
        }

        public override int Value
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