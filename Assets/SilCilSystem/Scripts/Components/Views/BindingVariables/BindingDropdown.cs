using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SilCilSystem.Variables;

namespace SilCilSystem.Components.Views
{
    [AddComponentMenu(menuName: EditorConstants.AddComponentPath + "Views/" + nameof(BindingDropdown))]
    public class BindingDropdown : BindingComponent, IBindingParameters
    {
        private Dropdown m_dropdown = default;
        private TMP_Dropdown m_tmpDropdown = default;

        [SerializeField] private ReadonlyPropertyBool m_setValueWithoutNotify = new ReadonlyPropertyBool(false);

        [Header("Params")]
        [SerializeField] private ReadonlyPropertyInt m_value = new ReadonlyPropertyInt(0);
        [SerializeField] private ReadonlyPropertyFloat m_alphaFadeSpeed = new ReadonlyPropertyFloat(0.15f);

        protected override IBindingParameters GetBindingParameters()
        {
            if (m_setValueWithoutNotify == null) return null;
            m_dropdown = GetComponent<Dropdown>();
            m_tmpDropdown = GetComponent<TMP_Dropdown>();
            return this;
        }

        public void SetParameters()
        {
            SetParametersDropdown();
            SetParametersTMPDropdown();
        }

        private void SetParametersDropdown()
        {
            if (m_dropdown == null) return;
            if (m_dropdown.alphaFadeSpeed != m_alphaFadeSpeed) m_dropdown.alphaFadeSpeed = m_alphaFadeSpeed;
            if (m_dropdown.value == m_value) return;

            if (m_setValueWithoutNotify)
            {
                m_dropdown.SetValueWithoutNotify(m_value);
            }
            else
            {
                m_dropdown.value = m_value;
            }
        }

        private void SetParametersTMPDropdown()
        {
            if (m_tmpDropdown == null) return;
            if (m_tmpDropdown.alphaFadeSpeed != m_alphaFadeSpeed) m_tmpDropdown.alphaFadeSpeed = m_alphaFadeSpeed;
            if (m_tmpDropdown.value == m_value) return;

            if (m_setValueWithoutNotify)
            {
                m_tmpDropdown.SetValueWithoutNotify(m_value);
            }
            else
            {
                m_tmpDropdown.value = m_value;
            }
        }
    }
}