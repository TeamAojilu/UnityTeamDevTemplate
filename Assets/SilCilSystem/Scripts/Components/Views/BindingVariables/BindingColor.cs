using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;
using SilCilSystem.Variables;
using SilCilSystem.Math;

namespace SilCilSystem.Components.Views
{
    [AddComponentMenu(menuName: EditorConstants.AddComponentPath + "Views/" + nameof(BindingColor))]
    public class BindingColor : BindingComponent, IBindingParameters
    {
        [SerializeField] private PropertyBool m_isBusy = default;
        [SerializeField] private ReadonlyPropertyColor m_color = default;

        [Header("Animation")]
        [SerializeField] private ReadonlyPropertyFloat m_duration = new ReadonlyPropertyFloat(-1f);
        [SerializeField] private InterpolationCurve m_curve = default;
        [SerializeField] private ReadonlyPropertyBool m_useInitial = new ReadonlyPropertyBool(false);
        [SerializeField] private Color m_initialColor = default;

        [Header("Targets")]
        [SerializeField] private GameObject[] m_components = default;
        [SerializeField] private MaterialInfo[] m_materials = default;

        [Header("Children")]
        [SerializeField] private bool m_containsImage = false;
        [SerializeField] private bool m_containsRawImage = false;
        [SerializeField] private bool m_containsText = false;
        [SerializeField] private bool m_containsTextMeshProUGUI = false;
        [SerializeField] private bool m_containsSprite = false;
        [SerializeField] private bool m_containsTileMap = false;

        private IColorSetter m_colorSetter = default;
        private PropertyAnimation<Color, ReadonlyPropertyColor, ReadonlyColor> m_animation = default;
        private Color m_currentValue = default;

        protected override IBindingParameters GetBindingParameters()
        {
            if (m_isBusy == null) return null;

            SetColorSetter();
            m_animation = new PropertyAnimationColor(m_color, m_duration, m_curve);
            m_animation.Initialize((m_useInitial) ? m_initialColor : m_color);
            return this;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            m_setOnUpdate = (m_duration > 0f) ? true : m_setOnUpdate;
        }

        public void SetParameters()
        {
            if (m_colorSetter == null) return;

            var color = m_animation.Update();
            IsBusy = m_animation.IsBusy();
            if (m_currentValue == color) return; // 変化がない場合は更新しない.

            m_currentValue = color;
            m_colorSetter?.SetColor(m_currentValue);
        }
        
        private bool IsBusy
        {
            set
            {
                if (m_isBusy != value) m_isBusy.Value = value;
            }
        }

        private void SetColorSetter()
        {
            CompositeColorSetter colorSetter = new CompositeColorSetter();
            foreach (var gameObject in m_components) colorSetter.Add(gameObject.GetColorComponent());
            foreach (var materialInfo in m_materials) colorSetter.Add(materialInfo.GetColorSetter());
            
            if (m_containsImage)
            {
                foreach(var color in GetComponentsInChildren<Image>()) colorSetter.Add(new ColorSetterUIGraphic(color));
            }

            if (m_containsRawImage)
            {
                foreach (var color in GetComponentsInChildren<RawImage>()) colorSetter.Add(new ColorSetterUIGraphic(color));
            }

            if (m_containsText)
            {
                foreach (var color in GetComponentsInChildren<Text>()) colorSetter.Add(new ColorSetterUIGraphic(color));
            }

            if (m_containsTextMeshProUGUI)
            {
                foreach (var color in GetComponentsInChildren<TextMeshProUGUI>()) colorSetter.Add(new ColorSetterUIGraphic(color));
            }

            if (m_containsSprite)
            {
                foreach (var color in GetComponentsInChildren<SpriteRenderer>()) colorSetter.Add(new ColorSetterSpriteRenderer(color));
            }

            if (m_containsTileMap)
            {
                foreach (var color in GetComponentsInChildren<Tilemap>()) colorSetter.Add(new ColorSetterTileMap(color));
            }

            m_colorSetter = colorSetter;
        }
    }
}
