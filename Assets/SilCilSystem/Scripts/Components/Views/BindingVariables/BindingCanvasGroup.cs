using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Math;

namespace SilCilSystem.Components.Views
{
    [AddComponentMenu(menuName: EditorConstants.AddComponentPath + "Views/" + nameof(BindingCanvasGroup))]
    [RequireComponent(typeof(CanvasGroup))]
    public class BindingCanvasGroup : BindingComponent, IBindingParameters
    {
        [Header("Params")]
        [SerializeField] private ReadonlyPropertyFloat m_alpha = new ReadonlyPropertyFloat(1f);
        [SerializeField] private ReadonlyPropertyBool m_interactable = new ReadonlyPropertyBool(true);
        [SerializeField] private ReadonlyPropertyBool m_blocksRayCasts = new ReadonlyPropertyBool(true);
        [SerializeField] private ReadonlyPropertyBool m_ignoreParentGroups = new ReadonlyPropertyBool(false);

        [Header("Animation")]
        [SerializeField] private PropertyBool m_isBusy = default;
        [SerializeField] private ReadonlyPropertyFloat m_duration = new ReadonlyPropertyFloat(-1f);
        [SerializeField] private InterpolationCurve m_curve = default;

        [Header("Alpha")]
        [SerializeField] private bool m_useInitialValue = false;
        [SerializeField] private float m_initialAlpha = 0f;

        private CanvasGroup m_canvasGroup = default;
        private PropertyAnimation<float, ReadonlyPropertyFloat, ReadonlyFloat> m_animation = default;

        protected override void OnValidate()
        {
            base.OnValidate();
            m_setOnUpdate = (m_duration > 0f) ? true : m_setOnUpdate;
        }

        protected override IBindingParameters GetBindingParameters()
        {
            if (m_alpha == null) return null;

            m_canvasGroup = GetComponent<CanvasGroup>();
            m_animation = new PropertyAnimationFloat(m_alpha, m_duration, m_curve);
            m_animation.Initialize((m_useInitialValue) ? m_initialAlpha : m_alpha);
            return this;
        }

        public void SetParameters()
        {
            if (m_canvasGroup.interactable != m_interactable) m_canvasGroup.interactable = m_interactable;
            if (m_canvasGroup.blocksRaycasts != m_blocksRayCasts) m_canvasGroup.blocksRaycasts = m_blocksRayCasts;
            if (m_canvasGroup.ignoreParentGroups != m_ignoreParentGroups) m_canvasGroup.ignoreParentGroups = m_ignoreParentGroups;

            SetIsBusy(m_animation.IsBusy());
            SetAlphaValue(m_animation.Update());
        }

        private void SetIsBusy(bool value)
        {
            if (value == m_isBusy) return;
            m_isBusy.Value = value;
        }

        private void SetAlphaValue(float value)
        {
            if (m_canvasGroup.alpha == value) return;
            m_canvasGroup.alpha = value;
        }
    }
}