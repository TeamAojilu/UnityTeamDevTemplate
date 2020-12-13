using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SilCilSystem.Variables;

namespace SilCilSystem.Components.Views
{
    [AddComponentMenu(menuName: EditorConstants.AddComponentPath + "Views/" + nameof(BindingInputField))]
    public class BindingInputField : BindingComponent, IBindingParameters
    {
        private InputField m_inputField = default;
        private TMP_InputField m_tmpInputField = default;

        [SerializeField] private ReadonlyPropertyBool m_setValueWithoutNotify = new ReadonlyPropertyBool(false);

        [Header("Params")]
        [SerializeField] private ReadonlyPropertyString m_text = default;
        [SerializeField] private ReadonlyPropertyInt m_characterLimit = new ReadonlyPropertyInt(0);
        [SerializeField] private ReadonlyPropertyFloat m_caretBlinkRate = new ReadonlyPropertyFloat(0.85f);
        [SerializeField] private ReadonlyPropertyInt m_caretWidth = new ReadonlyPropertyInt(1);
        [SerializeField] private ReadonlyPropertyBool m_customCaretColor = new ReadonlyPropertyBool(false);
        [SerializeField] private ReadonlyPropertyColor m_caretColor = new ReadonlyPropertyColor(new Color(0.1960784f, 0.1960784f, 0.1960784f));
        [SerializeField] private ReadonlyPropertyColor m_selectionColor = new ReadonlyPropertyColor(new Color(0.6588235f, 0.8078431f, 1f, 0.7529412f));
        [SerializeField] private ReadonlyPropertyBool m_hideMobileInput = new ReadonlyPropertyBool(false);
        [SerializeField] private ReadonlyPropertyBool m_readOnly = new ReadonlyPropertyBool(false);

        protected override IBindingParameters GetBindingParameters()
        {
            if (m_text == null) return null;
            m_inputField = GetComponent<InputField>();
            m_tmpInputField = GetComponent<TMP_InputField>();
            return this;
        }

        public void SetParameters()
        {
            SetParametersInputField();
            SetParametersTMPInputField();
        }

        private void SetParametersInputField()
        {
            if (m_inputField == null) return;

            if (m_inputField.characterLimit != m_characterLimit) m_inputField.characterLimit = m_characterLimit;
            if (m_inputField.caretBlinkRate != m_caretBlinkRate) m_inputField.caretBlinkRate = m_caretBlinkRate;
            if (m_inputField.caretWidth != m_caretWidth) m_inputField.caretWidth = m_caretWidth;
            if (m_inputField.customCaretColor != m_customCaretColor) m_inputField.customCaretColor = m_customCaretColor;
            if (m_inputField.caretColor != m_caretColor) m_inputField.caretColor = m_caretColor;
            if (m_inputField.selectionColor != m_selectionColor) m_inputField.selectionColor = m_selectionColor;
            if (m_inputField.shouldHideMobileInput != m_hideMobileInput) m_inputField.shouldHideMobileInput = m_hideMobileInput;
            if (m_inputField.readOnly != m_readOnly) m_inputField.readOnly = m_readOnly;

            if (m_inputField.text == m_text) return;
            if (m_setValueWithoutNotify)
            {
                m_inputField.SetTextWithoutNotify(m_text);
            }
            else
            {
                m_inputField.text = m_text;
            }
        }

        private void SetParametersTMPInputField()
        {
            if (m_tmpInputField == null) return;

            if (m_tmpInputField.characterLimit != m_characterLimit) m_tmpInputField.characterLimit = m_characterLimit;
            if (m_tmpInputField.caretBlinkRate != m_caretBlinkRate) m_tmpInputField.caretBlinkRate = m_caretBlinkRate;
            if (m_tmpInputField.caretWidth != m_caretWidth) m_tmpInputField.caretWidth = m_caretWidth;
            if (m_tmpInputField.customCaretColor != m_customCaretColor) m_tmpInputField.customCaretColor = m_customCaretColor;
            if (m_tmpInputField.caretColor != m_caretColor) m_tmpInputField.caretColor = m_caretColor;
            if (m_tmpInputField.selectionColor != m_selectionColor) m_tmpInputField.selectionColor = m_selectionColor;
            if (m_tmpInputField.shouldHideMobileInput != m_hideMobileInput) m_tmpInputField.shouldHideMobileInput = m_hideMobileInput;
            if (m_tmpInputField.readOnly != m_readOnly) m_tmpInputField.readOnly = m_readOnly;

            if (m_tmpInputField.text == m_text) return;
            if (m_setValueWithoutNotify)
            {
                m_tmpInputField.SetTextWithoutNotify(m_text);
            }
            else
            {
                m_tmpInputField.text = m_text;
            }
        }
    }
}