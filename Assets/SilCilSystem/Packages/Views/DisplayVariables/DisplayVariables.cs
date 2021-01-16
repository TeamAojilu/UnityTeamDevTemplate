using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using SilCilSystem.Variables;
using SilCilSystem.Timers;

namespace SilCilSystem.Views
{
    [AddComponentMenu(menuName: Constants.AddComponentPath + "Views/" + nameof(DisplayVariables))]
    public class DisplayVariables : MonoBehaviour
    {
        [Header("Status")]
        [SerializeField] private PropertyBool m_isBusy = new PropertyBool(false);

        [Header("Text")]
        [TextArea, SerializeField] private string m_format = "Value: {key}"; // TextAreaやOnValidateが効かないのでPropertyStringは未使用.

        [Header("Variables")]
        [SerializeField] private List<DisplayVariableInt> m_intValues = new List<DisplayVariableInt>();
        [SerializeField] private List<DisplayVariableFloat> m_floatValues = new List<DisplayVariableFloat>();
        [SerializeField] private List<DisplayVariableString> m_stringValues = new List<DisplayVariableString>();
        
        private IDisplayText m_text = default;
        private List<IDisplayVariable> m_variables = default;
        private string[] m_currentStrings = default;

        public bool IsBusy
        {
            get => m_isBusy;
            private set
            {
                if (m_isBusy != value) m_isBusy.Value = value;
            }
        }

        public void SetIsBusyVariable(VariableBool variable)
        {
            m_isBusy.Variable = variable;
        }

        public string Format
        {
            get => m_format;
            set
            {
                if (m_format == value) return;
                m_format = value;
                UpdateText();
            }
        }

        public void AddDisplayedVariable(string key, ReadonlyFloat variable)
        {
            var item = new DisplayVariableFloat();
            item.m_key = key;
            item.m_variable = variable;
            item.m_format = new ReadonlyPropertyString("0.000");
            m_floatValues.Add(item);

            SetVariables();
            UpdateText();
        }

        public void AddDisplayedVariable(string key, ReadonlyInt variable)
        {
            var item = new DisplayVariableInt();
            item.m_key = key;
            item.m_variable = variable;
            m_intValues.Add(item);

            SetVariables();
            UpdateText();
        }

        public void AddDisplayedVariable(string key, ReadonlyString variable)
        {
            var item = new DisplayVariableString();
            item.m_key = key;
            item.m_variable = variable;
            m_stringValues.Add(item);

            SetVariables();
            UpdateText();
        }

        public void UpdateText()
        {
            if (m_variables == null) SetVariables();
            if (!UpdateStrings()) return;
            SetText();
        }

        private void OnValidate()
        {
            SetVariables();
            MicroUpdate(0f);
        }

        private void Start()
        {
            UpdateDispatcher.Register(MicroUpdate, gameObject);
        }

        private bool MicroUpdate(float deltaTime)
        {
            if (!enabled) return true;
            UpdateText();
            return true;
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
            if (m_intValues.Count > 0) m_variables.AddRange(m_intValues);
            if (m_floatValues.Count > 0) m_variables.AddRange(m_floatValues);
            if (m_stringValues.Count > 0) m_variables.AddRange(m_stringValues);
            m_variables.ForEach(x => x.Initialize());
            m_currentStrings = new string[m_variables.Count];
        }
    }
}