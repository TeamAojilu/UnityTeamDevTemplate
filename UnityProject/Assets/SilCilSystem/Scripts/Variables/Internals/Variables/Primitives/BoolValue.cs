using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class BoolValue : VariableBool
    {
        [SerializeField] private bool m_value = default;
        public override bool Value { get => m_value; set => m_value = value; }
    }
}
