using UnityEngine;
using SilCilSystem.Components.Views;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors.Views;

namespace SilCilSystem.Editors
{
    internal abstract class TextGeneratorBase : VariableDragDropAction
    {
        public override bool IsAccepted(VariableAsset dropAsset)
        {
            if (dropAsset?.GetSubVariable<ReadonlyInt>() != null) return true;
            if (dropAsset?.GetSubVariable<ReadonlyFloat>() != null) return true;
            if (dropAsset?.GetSubVariable<ReadonlyString>() != null) return true;
            return false;
        }

        public override void OnDropExited(VariableAsset dropAsset)
        {
            var text = CreateText();
            if (text == null) return;

            text.gameObject.name = $"{dropAsset.name} Text";
            var display = text.AddComponent<DisplayVariables>();
            display.m_format = $"{dropAsset.name}: {{key}}";

            var intValue = dropAsset.GetSubVariable<ReadonlyInt>();
            if(intValue != null)
            {
                display.m_intValues = new DisplayVariableInt[]
                {
                    new DisplayVariableInt()
                    { 
                        m_key = "key",
                        m_variable = intValue,
                    }
                };
                return;
            }

            var floatValue = dropAsset.GetSubVariable<ReadonlyFloat>();
            if (floatValue != null)
            {
                display.m_floatValues = new DisplayVariableFloat[]
                {
                    new DisplayVariableFloat()
                    {
                        m_key = "key",
                        m_variable = floatValue,
                    }
                };
                return;
            }

            var stringValue = dropAsset.GetSubVariable<ReadonlyString>();
            if (stringValue != null)
            {
                display.m_stringValues = new DisplayVariableString[]
                {
                    new DisplayVariableString()
                    {
                        m_key = "key",
                        m_variable = stringValue,
                    }
                };
                return;
            }
        }

        protected abstract GameObject CreateText();
    }

    [AddVariableDragDrop("Readonly/Text")]
    internal class TextGenerator : TextGeneratorBase
    {
        protected override GameObject CreateText()
        {
            return UICreator.CreateText()?.gameObject;
        }
    }

    [AddVariableDragDrop("Readonly/Text - Text Mesh Pro")]
    internal class TextTMPGenerator : TextGeneratorBase
    {
        protected override GameObject CreateText()
        {
            return UICreator.CreateTextTMP()?.gameObject;
        }
    }
}