using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class FloatValue : VariableFloat
    {
        [SerializeField] private float m_value;
        public override float Value { get => m_value; set => m_value = value; }
    }
}
