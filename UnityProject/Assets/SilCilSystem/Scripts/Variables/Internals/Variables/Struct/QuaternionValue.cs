using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class QuaternionValue : VariableQuaternion
    {
        [SerializeField] private Quaternion m_value = default;
        public override Quaternion Value { get => m_value; set => m_value = value; }
    }
}