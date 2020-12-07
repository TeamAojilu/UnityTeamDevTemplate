using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class Vector2IntValue : VariableVector2Int
    {
        [SerializeField] private Vector2Int m_value = default;
        public override Vector2Int Value { get => m_value; set => m_value = value; }
    }
}