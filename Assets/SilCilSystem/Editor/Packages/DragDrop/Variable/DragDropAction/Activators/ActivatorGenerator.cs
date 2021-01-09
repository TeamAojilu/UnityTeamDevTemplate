using System.Linq;
using UnityEngine;
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
        }
    }

    [AddVariableDragDrop("Activators/GameObjects")]
    internal class GameObjectActivatorGenerator : ActivatorGeneratorBase<GameObjectActivator>
    {
        protected override string Prefix => nameof(GameObjectActivator);
    }

    [AddVariableDragDrop("Activators/Behaviours")]
    internal class BehaviourActivatorGenerator : ActivatorGeneratorBase<BehaviourActivator>
    {
        protected override string Prefix => nameof(BehaviourActivator);
    }
}