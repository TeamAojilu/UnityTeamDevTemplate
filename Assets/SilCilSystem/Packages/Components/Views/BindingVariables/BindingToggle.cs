using UnityEngine;
using UnityEngine.UI;
using SilCilSystem.Variables;

namespace SilCilSystem.Components.Views
{
    [AddComponentMenu(menuName: Constants.AddComponentPath + "Views/" + nameof(BindingToggle))]
    [RequireComponent(typeof(Toggle))]
    public class BindingToggle : BindingComponent, IBindingParameters
    {
        private Toggle m_toggle = default;

        public ReadonlyPropertyBool m_setValueWithoutNotify = new ReadonlyPropertyBool(false);
        
        [Header("Params")]
        public ReadonlyPropertyBool m_isOn = new ReadonlyPropertyBool(false);

        public void SetParameters()
        {
            if (m_toggle.isOn == m_isOn) return;

            if (m_setValueWithoutNotify)
            {
                m_toggle.SetIsOnWithoutNotify(m_isOn);
            }
            else
            {
                m_toggle.isOn = m_isOn;
            }
        }

        protected override IBindingParameters GetBindingParameters()
        {
            if (m_setValueWithoutNotify == null) return null;
            m_toggle = GetComponent<Toggle>();
            return this;
        }
    }
}