using UnityEngine;
using SilCilSystem.Variables;

namespace SilCilSystem.Components
{
    [AddComponentMenu(menuName: EditorConstants.AddComponentPath + "Views/" + nameof(DisplayFloat))]
    public class DisplayFloat : MonoBehaviour
    {
        [SerializeField] private ReadonlyFloat m_variable = default;
        [SerializeField] private string m_format = "Value: {0:0.00}";
        private IDisplayText m_text = default;

        private void Update() => SetText();

        private void SetText()
        {
            m_text = m_text ?? gameObject.GetTextComponent();
            m_text?.SetText(string.Format(m_format, m_variable?.Value ?? 0f));
        }

        private void OnValidate() => SetText();
    }
}