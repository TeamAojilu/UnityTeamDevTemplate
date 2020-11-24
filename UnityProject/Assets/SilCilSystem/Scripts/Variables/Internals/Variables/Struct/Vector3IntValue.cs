using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class Vector3IntValue : VariableVector3Int
    {
        [SerializeField] private Vector3Int m_value = default;
        public override Vector3Int Value { get => m_value; set => m_value = value; }
    }
}