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
        [SerializeField] private VariableBool m_isBusy = default;

        [Header("Text")]
        [SerializeField, TextArea] private string m_format = "Value: {key}";

        [Header("Variables")]
        [SerializeField] private DisplayVariableInt[] m_intValues = default;
        [SerializeField] private DisplayVariableFloat[] m_floatValues = default;

        private IDisplayText m_text = default;
        private List<IDisplayVariable> m_variables = default;

        private bool IsBusy
        {
            set
            {
                if (m_isBusy != null && m_isBusy.Value != value) m_isBusy.Value = value;
            }
        }

        private void Start() => SetText();
        private void OnValidate() => SetText();
        
        private void Update()
        {
            if (!CheckIsBusy()) return;
            SetText();
        }

        private bool CheckIsBusy()
        {
            bool busy = m_variables.Any(x => x.IsBusy);
            IsBusy = busy;
            return busy;
        }

        private void SetText()
        {
            m_text = m_text ?? gameObject.GetTextComponent();
            if (m_text == null) return;

            if (m_variables == null)
            {
                m_variables = new List<IDisplayVariable>();
                if(m_intValues?.Length != 0) m_variables.AddRange(m_intValues);
                if(m_floatValues?.Length != 0) m_variables.AddRange(m_floatValues);
                m_variables.ForEach(x => x.Initialize());
            }

            string text = m_format;
            foreach (var variable in m_variables)
            {
                text = text.Replace($"{{{variable.Key}}}", variable.Update());
            }
            m_text.SetText(text);
        }
    }
}