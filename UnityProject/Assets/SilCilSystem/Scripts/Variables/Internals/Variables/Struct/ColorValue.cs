using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class ColorValue : VariableColor
    {
        [SerializeField] private Color m_value = default;
        public override Color Value { get => m_value; set => m_value = value; }
    }
}