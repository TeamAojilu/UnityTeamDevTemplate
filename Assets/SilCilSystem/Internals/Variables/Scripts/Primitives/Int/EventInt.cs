using System;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    internal class EventInt : GameEventInt
    {
        private event Action<int> m_event = default;

        public override void Publish(int value)
        {
            m_event?.Invoke(value);
        }

        public override IDisposable Subscribe(Action<int> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
        
        public override void GetAssetName(ref string name) => name = $"{name}_OnChanged";
        public override void OnAttached(VariableAsset parent) { }
    }
}