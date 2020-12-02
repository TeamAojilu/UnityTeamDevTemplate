using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class StringValue : VariableString
    {
        [SerializeField] private string m_value;
        public override string Value { get => m_value; set => m_value = value; }
    }
}
