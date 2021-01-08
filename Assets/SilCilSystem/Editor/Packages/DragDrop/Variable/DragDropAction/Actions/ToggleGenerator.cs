using UnityEngine.UI;
using UnityEditor.Events;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Views;
using SilCilSystem.Editors.Views;
using System.Linq;

namespace SilCilSystem.Editors
{
    internal abstract class ToggleGeneratorBase : VariableDragDropAction
    {
        public override bool IsAccepted(VariableAsset[] dropAsset)
        {
            return dropAsset.Any(x => x is ReadonlyBool);
        }

        public override void OnDropExited(VariableAsset dropAsset)
        {
            var ro = dropAsset?.GetSubVariable<ReadonlyBool>();

            if (ro == null) return;
            var toggle = CreateToggle();
            toggle.name = $"{dropAsset.name}_{toggle.name}";
            toggle.gameObject.GetTextComponentInChildren()?.SetText(dropAsset.name);

            var bind = toggle.gameObject.AddComponent<BindingToggle>();
            bind.m_isOn = new ReadonlyPropertyBool(ro);
            bind.m_isOn.Variable = ro;

            PostProcess(dropAsset, toggle);
        }

        protected virtual Toggle CreateToggle()
        {
            return UICreator.CreateToggle();
        }

        protected abstract void PostProcess(VariableAsset dropAsset, Toggle toggle);
    }

    [AddVariableDragDrop("Readonly/Toggle")]
    internal class ReadonlyToggleGenerator : ToggleGeneratorBase
    {
        protected override void PostProcess(VariableAsset dropAsset, Toggle toggle)
        {
            toggle.interactable = false;
            toggle.transition = Selectable.Transition.None;
            var nav = toggle.navigation;
            nav.mode = Navigation.Mode.None;
            toggle.navigation = nav;
        }
    }

    [AddVariableDragDrop("Interactive/Toggle")]
    internal class ToggleGenerator : ToggleGeneratorBase
    {
        public override bool IsAccepted(VariableAsset[] dropAsset)
        {
            return base.IsAccepted(dropAsset) && dropAsset.Any(x => x is VariableBool);
        }

        protected override void PostProcess(VariableAsset dropAsset, Toggle toggle)
        {
            var value = dropAsset?.GetSubVariable<VariableBool>();
            if (value == null) return;
            UnityEventTools.AddPersistentListener(toggle.onValueChanged, value.SetValue);
        }
    }
}