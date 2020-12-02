using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class Vector3Value : VariableVector3
    {
        [SerializeField] private Vector3 m_value = default;
        public override Vector3 Value { get => m_value; set => m_value = value; }
    }
}