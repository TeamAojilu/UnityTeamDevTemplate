using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using SilCilSystem.Variables;

namespace SilCilSystem.Components.Views
{
    [AddComponentMenu(menuName: EditorConstants.AddComponentPath + "Views/" + nameof(DisplayVariables))]
    public class DisplayVariables : MonoBehaviour
    {
        [Header("Status")]
        [SerializeField] private PropertyBool m_isBusy = default;

        [Header("Text")]
        [SerializeField, TextArea] private string m_format = "Value: {key}"; // TextAreaやOnValidateが効かないのでPropertyStringは未使用.

        [Header("Variables")]
        [SerializeField] private DisplayVariableInt[] m_intValues = default;
        [SerializeField] private DisplayVariableFloat[] m_floatValues = default;
        [SerializeField] private DisplayVariableString[] m_stringValues = default;

        private IDisplayText m_text = default;
        private List<IDisplayVariable> m_variables = default;
        private string[] m_currentStrings = default;

        private bool IsBusy
        {
            set
            {
                if (m_isBusy != value) m_isBusy.Value = value;
            }
        }

        private void Start() 
        {
            SetVariables();
            UpdateStrings();
            SetText();
        }

        private void OnValidate() => Start();
        
        private void Update()
        {
            if (!UpdateStrings()) return;
            SetText();
        }

        private bool UpdateStrings()
        {
            bool changed = false;

            int i = 0;
            foreach (var variable in m_variables)
            {
                var newValue = variable.Update();
                changed |= m_currentStrings[i] != newValue;
                m_currentStrings[i] = newValue;
                i++;
            }

            IsBusy = changed;
            return changed;
        }

        private void SetText()
        {
            m_text = m_text ?? gameObject.GetTextComponent();
            if (m_text == null) return;

            string text = m_format;
            foreach ((var key, var value) in m_variables.Zip(m_currentStrings, (v, c) => (v.Key, c)))
            {
                text = text.Replace($"{{{key}}}", value);
            }

            m_text.SetText(text);
        }

        private void SetVariables()
        {
            m_variables = new List<IDisplayVariable>();
            if (m_intValues?.Length > 0) m_variables.AddRange(m_intValues);
            if (m_floatValues?.Length > 0) m_variables.AddRange(m_floatValues);
            if (m_stringValues?.Length > 0) m_variables.AddRange(m_stringValues);
            m_variables.ForEach(x => x.Initialize());
            m_currentStrings = new string[m_variables.Count];
        }
    }
}