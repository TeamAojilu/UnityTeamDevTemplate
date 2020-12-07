using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class Vector2Value : VariableVector2
    {
        [SerializeField] private Vector2 m_value = default;
        public override Vector2 Value { get => m_value; set => m_value = value; }
    }
}