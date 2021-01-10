using SilCilSystem.Components.Timers;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal abstract class TimerGenerator : VariableDragDropAction
    {
        public override bool IsAccepted(VariableAsset[] dropAsset)
        {
            return dropAsset.Any(x => x is VariableFloat);
        }

        public override void OnDropExited(VariableAsset dropAsset)
        {
            var time = dropAsset?.GetSubVariable<VariableFloat>();
            if (time == null) return;

            GameObject obj = new GameObject();
            obj.name = $"Timer_{dropAsset.name}";
            var timer = obj.AddComponent<Timer>();

            SetOptions(timer, time);
            Undo.RegisterCreatedObjectUndo(obj, $"Create {obj.name}");
            Selection.activeObject = obj;
        }

        protected abstract void SetOptions(Timer timer, VariableFloat variable);
    }

    [AddVariableDragDrop("Timers/Count up")]
    internal class CountUpTimerGenerator : TimerGenerator
    {
        protected override void SetOptions(Timer timer, VariableFloat variable)
        {
            timer.m_time.Variable = variable;
        }
    }

    [AddVariableDragDrop("Timers/Count down")]
    internal class CountDownTimerGenerator : TimerGenerator
    {
        protected override void SetOptions(Timer timer, VariableFloat variable)
        {
            timer.m_time.Variable = variable;
            timer.m_initialTime = new ReadonlyPropertyFloat(variable);
            timer.m_max = new ReadonlyPropertyFloat(variable);
            timer.m_timeScale = new ReadonlyPropertyFloat(-1f);
        }
    }

    [AddVariableDragDrop("Timers/Repeat")]
    internal class RepeatTimerGenerator : TimerGenerator
    {
        protected override void SetOptions(Timer timer, VariableFloat variable)
        {
            timer.m_time.Variable = variable;
            timer.m_initialTime = new ReadonlyPropertyFloat(variable);
            timer.m_max = new ReadonlyPropertyFloat(variable);
            timer.m_repeating = new ReadonlyPropertyBool(true);
        }
    }
}