using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Views;
using SilCilSystem.Editors.Views;
using System.Linq;

namespace SilCilSystem.Editors
{
    internal abstract class TextGeneratorBase : VariableDragDropAction
    {
        public override bool IsAccepted(VariableAsset[] dropAsset)
        {
            return dropAsset.Any(x => x is ReadonlyInt || x is ReadonlyFloat || x is ReadonlyString);
        }

        public override void OnDropExited(VariableAsset dropAsset)
        {
            var text = CreateText();
            if (text == null) return;


            text.gameObject.name = $"{dropAsset.name} Text";

            string key = "key";
            var display = text.AddComponent<DisplayVariables>();
            display.Format = $"{dropAsset.name}: {{{key}}}";

            var intValue = dropAsset.GetSubVariable<ReadonlyInt>();
            if(intValue != null)
            {
                display.AddDisplayedVariable(key, intValue);
                display.UpdateText();
                return;
            }

            var floatValue = dropAsset.GetSubVariable<ReadonlyFloat>();
            if (floatValue != null)
            {
                display.AddDisplayedVariable(key, floatValue);
                display.UpdateText();
                return;
            }

            var stringValue = dropAsset.GetSubVariable<ReadonlyString>();
            if (stringValue != null)
            {
                display.AddDisplayedVariable(key, stringValue);
                display.UpdateText();
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