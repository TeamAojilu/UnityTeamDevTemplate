using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class IntValue : VariableInt
    {
        [SerializeField] private int m_value;
        public override int Value { get => m_value; set => m_value = value; }
    }
}
