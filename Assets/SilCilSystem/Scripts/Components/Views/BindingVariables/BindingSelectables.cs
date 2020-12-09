using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SilCilSystem.Variables;

namespace SilCilSystem.Components.Views
{
    [AddComponentMenu(menuName: EditorConstants.AddComponentPath + "Views/" + nameof(BindingSelectables))]
    public class BindingSelectables : BindingComponent, IBindingParameters
    {
        private const float NORMAL_COLOR = 1f;
        private const float HIGHLIGHT_COLOR = 0.9607843f;
        private const float PRESSED_COLOR = 0.7843137f;
        private const float SELECTED_COLOR = HIGHLIGHT_COLOR;
        private const float DISABLE_COLOR = PRESSED_COLOR;
        private const float DISABLE_ALPHA = 0.5019608f;

        [Header("Params")]
        [SerializeField] private ReadonlyPropertyBool m_interactable = new ReadonlyPropertyBool(true);

        [Header("Color Tint")]
        [SerializeField] private ReadonlyPropertyColor m_normalColor = new ReadonlyPropertyColor(new Color(NORMAL_COLOR, NORMAL_COLOR, NORMAL_COLOR));
        [SerializeField] private ReadonlyPropertyColor m_highlightedColor = new ReadonlyPropertyColor(new Color(HIGHLIGHT_COLOR, HIGHLIGHT_COLOR, HIGHLIGHT_COLOR));
        [SerializeField] private ReadonlyPropertyColor m_pressedColor = new ReadonlyPropertyColor(new Color(PRESSED_COLOR, PRESSED_COLOR, PRESSED_COLOR));
        [SerializeField] private ReadonlyPropertyColor m_selectedColor = new ReadonlyPropertyColor(new Color(SELECTED_COLOR, SELECTED_COLOR, SELECTED_COLOR));
        [SerializeField] private ReadonlyPropertyColor m_disabledColor = new ReadonlyPropertyColor(new Color(DISABLE_COLOR, DISABLE_COLOR, DISABLE_COLOR, DISABLE_ALPHA));
        [SerializeField] private ReadonlyPropertyFloat m_fadeDuration = new ReadonlyPropertyFloat(0.2f);
        [SerializeField] private ReadonlyPropertyFloat m_colorMultiplier = new ReadonlyPropertyFloat(1f);

        [Header("Animation")]
        [SerializeField] private ReadonlyPropertyString m_normalTrigger = new ReadonlyPropertyString("Normal");
        [SerializeField] private ReadonlyPropertyString m_highlightedTrigger = new ReadonlyPropertyString("Highlighted");
        [SerializeField] private ReadonlyPropertyString m_pressedTrigger = new ReadonlyPropertyString("Pressed");
        [SerializeField] private ReadonlyPropertyString m_selectedTrigger = new ReadonlyPropertyString("Selected");
        [SerializeField] private ReadonlyPropertyString m_disabledTrigger = new ReadonlyPropertyString("Disabled");

        [Header("Targets")]
        [SerializeField] private Selectable[] m_selectables = default;
        [SerializeField] private bool m_containsChildren = false;

        private List<Selectable> m_targets = default;

        public void SetParameters()
        {
            SetParametersSelectables();
        }

        protected override IBindingParameters GetBindingParameters()
        {
            if (m_selectables == null) return null;

            m_targets = new List<Selectable>(m_selectables);
            if (m_containsChildren)
            {
                foreach(var selectable in GetComponentsInChildren<Selectable>())
                {
                    if (m_targets.Contains(selectable)) continue;
                    m_targets.Add(selectable);
                }
            }

            return this;
        }

        private void SetParametersSelectables()
        {
            var colorBlock = GetColorBlock();
            var animationTriggers = GetTriggers();

            foreach (var selectable in m_targets)
            {
                if (selectable == null) continue;
                if (selectable.interactable != m_interactable) selectable.interactable = m_interactable;
                if (selectable.colors != colorBlock) selectable.colors = colorBlock;
                if (selectable.animationTriggers != animationTriggers) selectable.animationTriggers = animationTriggers;
            }
        }

        private ColorBlock GetColorBlock()
        {
            return new ColorBlock()
            {
                normalColor = m_normalColor,
                highlightedColor = m_highlightedColor,
                pressedColor = m_pressedColor,
                selectedColor = m_selectedColor,
                disabledColor = m_disabledColor,
                colorMultiplier = m_colorMultiplier,
                fadeDuration = m_fadeDuration,
            };
        }

        private AnimationTriggers GetTriggers()
        {
            return new AnimationTriggers()
            {
                normalTrigger = m_normalTrigger,
                highlightedTrigger = m_highlightedTrigger,
                pressedTrigger = m_pressedTrigger,
                selectedTrigger = m_selectedTrigger,
                disabledTrigger = m_disabledTrigger,
            };
        }
    }
}