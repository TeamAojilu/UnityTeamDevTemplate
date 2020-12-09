using UnityEngine;
using UnityEngine.UI;
using SilCilSystem.Variables;
using SilCilSystem.Math;

namespace SilCilSystem.Components.Views
{
    [AddComponentMenu(menuName: EditorConstants.AddComponentPath + "Views/" + nameof(BindingSlider))]
    [RequireComponent(typeof(Slider))]
    public class BindingSlider : BindingComponent, IBindingParameters
    {
        [SerializeField] private ReadonlyPropertyBool m_setValueWithoutNotify = new ReadonlyPropertyBool(false);

        [Header("Params")]
        [SerializeField] private ReadonlyPropertyFloat m_minValue = new ReadonlyPropertyFloat(0f);
        [SerializeField] private ReadonlyPropertyFloat m_maxValue = new ReadonlyPropertyFloat(1f);
        [SerializeField] private ReadonlyPropertyBool m_wholeNumbers = new ReadonlyPropertyBool(false);
        [SerializeField] private ReadonlyPropertyFloat m_value = new ReadonlyPropertyFloat(0f);

        [Header("Animation")]
        [SerializeField] private PropertyBool m_isBusy = default;
        [SerializeField] private ReadonlyPropertyFloat m_duration = new ReadonlyPropertyFloat(-1f);
        [SerializeField] private InterpolationCurve m_curve = default;

        private Slider m_slider = default;
        private PropertyAnimation<float, ReadonlyPropertyFloat, ReadonlyFloat> m_animation = default;

        protected override void OnValidate()
        {
            base.OnValidate();
            m_setOnUpdate = (m_duration > 0f) ? true : m_setOnUpdate;
        }

        protected override IBindingParameters GetBindingParameters()
        {
            if (m_setValueWithoutNotify == null) return null;

            m_slider = GetComponent<Slider>();
            m_animation = new PropertyAnimationFloat(m_value, m_duration, m_curve);
            m_animation.Initialize(m_slider.value);
            return this;
        }

        public void SetParameters()
        {
            if (m_slider.minValue != m_minValue) m_slider.minValue = m_minValue;
            if (m_slider.maxValue != m_maxValue) m_slider.maxValue = m_maxValue;
            if (m_slider.wholeNumbers != m_wholeNumbers) m_slider.wholeNumbers = m_wholeNumbers;

            SetIsBusy(m_animation.IsBusy());
            SetSliderValue(m_animation.Update());
        }

        private void SetIsBusy(bool value)
        {
            if (value == m_isBusy) return;
            m_isBusy.Value = value;
        }

        private void SetSliderValue(float value)
        {
            if (m_slider.value == value) return;
            if (m_setValueWithoutNotify)
            {
                m_slider.SetValueWithoutNotify(value);
            }
            else
            {
                m_slider.value = value;
            }
        }
    }
}