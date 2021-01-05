using UnityEngine.UI;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Components.Views;
using SilCilSystem.Editors.Views;
using UnityEditor.Events;

namespace SilCilSystem.Editors
{
    internal abstract class SliderGeneratorBase : VariableDragDropAction
    {
        public override bool IsAccepted(VariableAsset dropAsset)
        {
            return dropAsset?.GetSubVariable<ReadonlyFloat>() != null;
        }

        public override void OnDropExited(VariableAsset dropAsset)
        {
            var ro = dropAsset?.GetSubVariable<ReadonlyFloat>();

            if (ro == null) return;
            var slider = CreateSlider();
            slider.name = $"{dropAsset.name}_{slider.name}";

            var bind = slider.gameObject.AddComponent<BindingSlider>();
            bind.m_value = new ReadonlyPropertyFloat(ro);
            bind.m_value.Variable = ro;

            PostProcess(dropAsset, slider);
        }

        protected abstract Slider CreateSlider();

        protected abstract void PostProcess(VariableAsset dropAsset, Slider slider);
    }

    [AddVariableDragDrop("Readonly/Bar (Slider)")]
    internal class BarGenerator : SliderGeneratorBase
    {
        protected override Slider CreateSlider()
        {
            return UICreator.CreateBar();
        }

        protected override void PostProcess(VariableAsset dropAsset, Slider slider) { }
    }

    [AddVariableDragDrop("Interactives/Slider")]
    internal class SliderGenerator : SliderGeneratorBase
    {
        public override bool IsAccepted(VariableAsset dropAsset)
        {
            return base.IsAccepted(dropAsset) && dropAsset?.GetSubVariable<ReadonlyFloat>() != null;
        }

        protected override Slider CreateSlider()
        {
            return UICreator.CreateSlider();
        }

        protected override void PostProcess(VariableAsset dropAsset, Slider slider)
        {
            var value = dropAsset?.GetSubVariable<VariableFloat>();
            if (value == null) return;
            UnityEventTools.AddPersistentListener(slider.onValueChanged, value.SetValue);
        }
    }    
}