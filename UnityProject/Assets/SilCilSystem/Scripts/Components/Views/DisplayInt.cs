using UnityEngine;
using SilCilSystem.Variables;

namespace SilCilSystem.Components
{
    [AddComponentMenu(menuName: EditorConstants.AddComponentPath + "Views/" + nameof(DisplayInt))]
    public class DisplayInt : MonoBehaviour
    {
        [SerializeField] private ReadonlyInt m_variable = default;
        [SerializeField] private string m_format = "Count: {0:000}";
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