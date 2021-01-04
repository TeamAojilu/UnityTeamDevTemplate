using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Components.Views;
using SilCilSystem.Editors.Views;
using UnityEditor.Events;

namespace SilCilSystem.Editors
{
    [AddVariableDragDrop("Slider")]
    public class SliderGenerator : VariableDragDropAction
    {
        public override bool IsAccepted(VariableAsset dropAsset)
        {
            return dropAsset?.GetSubVariable<ReadonlyFloat>() != null;
        }

        public override void OnDropExited(VariableAsset dropAsset)
        {
            var ro = dropAsset?.GetSubVariable<ReadonlyFloat>();

            if (ro == null) return;
            var slider = UICreator.CreateSlider();

            var bind = slider.gameObject.AddComponent<BindingSlider>();
            bind.m_value = new ReadonlyPropertyFloat(ro);
            bind.m_value.Variable = ro;

            var value = dropAsset?.GetSubVariable<VariableFloat>();
            if (value == null) return;

            UnityEventTools.AddPersistentListener(slider.onValueChanged, value.SetValue);
        }
    }
}