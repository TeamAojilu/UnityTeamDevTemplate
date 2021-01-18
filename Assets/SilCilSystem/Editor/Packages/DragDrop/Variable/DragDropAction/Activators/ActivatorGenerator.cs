using System.Linq;
using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Activators;

namespace SilCilSystem.Editors
{
    internal abstract class ActivatorGeneratorBase<T> : VariableDragDropAction where T : Activator
    {
        protected abstract string Prefix { get; }

        public override bool IsAccepted(VariableAsset[] assetIncludingChildren)
        {
            return assetIncludingChildren.Any(x => x is ReadonlyBool);
        }

        public override void OnDropExited(VariableAsset dropAsset)
        {
            var variable = dropAsset?.GetSubVariable<ReadonlyBool>();
            if (variable == null) return;

            var obj = new GameObject();
            obj.name = $"{Prefix} {dropAsset.name}";
            var activator = obj.AddComponent<T>();
            activator.m_isActive.Variable = variable;

            Undo.RegisterCreatedObjectUndo(obj, $"Create {obj.name}");
            Selection.activeObject = obj;
        }
    }

    [VariableDragDrop("Activators/GameObjects")]
    internal class GameObjectActivatorGenerator : ActivatorGeneratorBase<GameObjectActivator>
    {
        protected override string Prefix => nameof(GameObjectActivator);
    }

    [VariableDragDrop("Activators/Behaviours")]
    internal class BehaviourActivatorGenerator : ActivatorGeneratorBase<BehaviourActivator>
    {
        protected override string Prefix => nameof(BehaviourActivator);
    }
}