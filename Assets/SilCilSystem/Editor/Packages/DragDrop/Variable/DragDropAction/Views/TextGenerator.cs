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
            return dropAsset.Any(x => IsTarget(x));
        }

        private bool IsTarget(VariableAsset variable)
        {
            return variable is ReadonlyInt || variable is ReadonlyFloat || variable is ReadonlyString;
        }

        public override void OnDropExited(VariableAsset dropAsset)
        {
            var text = CreateText();
            if (text == null) return;

            text.gameObject.name = $"{dropAsset.name} Text";

            string key = "key";
            var display = text.AddComponent<DisplayVariables>();
            display.Format = $"{dropAsset.name}: {{{key}}}";

            foreach(var subVariable in dropAsset.GetAllVariables())
            {
                if (!IsTarget(subVariable)) continue;
                switch (subVariable)
                {
                    case ReadonlyInt intValue:
                        display.AddDisplayedVariable(key, intValue);
                        break;
                    case ReadonlyFloat floatValue:
                        display.AddDisplayedVariable(key, floatValue);
                        break;
                    case ReadonlyString stringValue:
                        display.AddDisplayedVariable(key, stringValue);
                        break;
                }
                display.UpdateText();
                break;
            }
        }

        protected abstract GameObject CreateText();
    }

    [VariableDragDrop("Readonly/Text")]
    internal class TextGenerator : TextGeneratorBase
    {
        protected override GameObject CreateText()
        {
            return UICreator.CreateText()?.gameObject;
        }
    }

    [VariableDragDrop("Readonly/Text - Text Mesh Pro")]
    internal class TextTMPGenerator : TextGeneratorBase
    {
        protected override GameObject CreateText()
        {
            return UICreator.CreateTextTMP()?.gameObject;
        }
    }
}